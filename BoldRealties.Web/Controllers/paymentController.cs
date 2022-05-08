using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.BillingPortal;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using Session = Stripe.Checkout.Session;
using SessionService = Stripe.Checkout.SessionService;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Models.ViewModels;
using BoldRealties.BLL;

namespace BoldRealties.Areas.Customer
{

    [Authorize]
    public class paymentController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly IEmailSender _emailSender;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public paymentController(IUnitOfWork unit, IEmailSender emailSender)
        {
            _unit = unit;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            //DS: we create a variable 'claimsIdentity' and we initialize it with the name identifier
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            //DS: we then create another variable 'claim' to extract the claim from the ClaimsIdentity
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _unit.ShoppingCart.GetAll(u => u.UserId == claim.Value,
                    includeProperties: "tenancies"),
                OrderHeader = new OrderHeader()
            };
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.tenancies.rentPrice
                   );
                ShoppingCartVM.OrderHeader.PaymentTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            //DS: we create a variable 'claimsIdentity' and we initialize it with the name identifier
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            //DS: we then create another variable 'claim' to extract the claim from the ClaimsIdentity
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //DS: create new shopping cart
            ShoppingCartVM = new ShoppingCartVM()
            {
                //DS: retrieve data from shopping cart for the logged in user 
                ListCart = _unit.ShoppingCart.GetAll(u => u.UserId == claim.Value,
                    includeProperties: "tenancies"),
                OrderHeader = new OrderHeader()
            };
            //get user id from db that is equal to claim value(logged in user) && assign the value to FK of User from OrderHeader
            ShoppingCartVM.OrderHeader.Users = _unit.Users.GetFirstOrDefault(u => u.Id == claim.Value);

            ShoppingCartVM.OrderHeader.FirstName = ShoppingCartVM.OrderHeader.Users.firstName;
            ShoppingCartVM.OrderHeader.LastName = ShoppingCartVM.OrderHeader.Users.lastName;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.Users.PhoneNumber;
            ShoppingCartVM.OrderHeader.Email = ShoppingCartVM.OrderHeader.Users.Email;
            ShoppingCartVM.OrderHeader.Address = ShoppingCartVM.OrderHeader.Users.Address;
            //DS: calculate the total price based on price and number of products(rent price) in the cart
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.tenancies.rentPrice
                   );
                ShoppingCartVM.OrderHeader.PaymentTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPOST()
        {
            //DS: claim identity of the logged in user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //DS: Ger shopping cart items for the logged in user && include tenancies
            ShoppingCartVM.ListCart = _unit.ShoppingCart.GetAll(u => u.UserId == claim.Value,
                includeProperties: "tenancies");
            //DS: set the value of payment date
            ShoppingCartVM.OrderHeader.UserId = claim.Value;
            //DS: for each item in the shopping cart calculate the total price based on item price and number of items
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.tenancies.rentPrice
                   );
                ShoppingCartVM.OrderHeader.PaymentTotal += (cart.Price * cart.Count);
            }
            //set the value of the application user id with the value of logged in user
            Users Users = _unit.Users.GetFirstOrDefault(u => u.Id == claim.Value);

            //add record to db and save
            _unit.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unit.Save();
            //for each item in shopping cart list for the logged in user add record for order details in db and save
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                OrderDetails OrderDetails = new OrderDetails()
                {
                    TenancyId = cart.TenancyId,
                    OrderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _unit.OrderDetails.Add(OrderDetails);
                _unit.Save();
            }
            //DS: stripe settings from here
            var domain = "https://localhost:7208/";
            var options = new SessionCreateOptions //we create a session create option
            {
                PaymentMethodTypes = new List<string> //
                {
                  "card", // this is the option for payment method
                },
                LineItems = new List<SessionLineItemOptions>(), //DS: we create a new list of session LineItems,representing a list of the items from the shopping cart
                Mode = "payment",

                SuccessUrl = domain + $"payment/PaymentConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                CancelUrl = domain + $"payment/Index",
            };

            foreach (var item in ShoppingCartVM.ListCart)
            {
                //DS: the code below represents the 'LineItems' options 
                // so foreach statement will apply the options for each product in the ListCart

                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),//20.00 -> 2000
                        Currency = "usd", // currency for shopping cart
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.tenancies.Id.ToString()
                        },

                    },
                    Quantity = item.Count,
                };
                //we basically add the options for each product in the LineItems created above
                options.LineItems.Add(sessionLineItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);
            _unit.OrderHeader.UpdateStripePaymentID(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unit.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult PaymentConfirmation(int id)
        {
            //DS: retrieve order header from db based on the id
            OrderHeader orderHeader = _unit.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "Users");
            //DS: 
            if (orderHeader.PaymentStatus != StaticDetails.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                //DS: check the stripe status
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unit.OrderHeader.UpdateStatus(id, StaticDetails.StatusApproved, StaticDetails.PaymentStatusApproved);
                    _unit.Save();
                }
            }

            _emailSender.SendEmailAsync(orderHeader.Users.Email, "Rent paid - Bold Realties", "<p>Rent paid </p>");
            List<ShoppingCart> shoppingCarts = _unit.ShoppingCart.GetAll(u => u.UserId ==
            orderHeader.UserId).ToList();
            _unit.ShoppingCart.RemoveRange(shoppingCarts);
            _unit.Save();
            return View(id);
        }
       
        //DS: function to remove item from shopping cart
        public IActionResult Remove(int cartId)
        {
            var cart = _unit.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unit.ShoppingCart.Remove(cart);
            _unit.Save();
            var count = _unit.ShoppingCart.GetAll(u => u.UserId == cart.UserId).ToList().Count;
            return RedirectToAction(nameof(Index));
        }
        //function which is used for total price calculation using quantity
        private double GetPriceBasedOnQuantity(double quantity, double price)
        {
            if (quantity <= 50)
            {
                return price;
            }
            else
                return price;

        }
    }
}

