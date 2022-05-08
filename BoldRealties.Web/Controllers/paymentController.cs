using Microsoft.AspNetCore.Mvc;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;
using Stripe.Checkout;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;
using BoldRealties.Models.ViewModels;
using BoldRealties.BLL;

namespace BoldRealties.Web.Controllers
{
    public class paymentController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly IEmailSender _emailSender;
        public paymentVM paymentVM { get; set; }
        public paymentController(IUnitOfWork unit, IEmailSender emailSender) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
            _emailSender = emailSender;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
             public IActionResult Index()
            {
            //DS: we create a variable 'claimsIdentity' and we initialize it with the name identifier
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            //DS: we then create another variable 'claim' to extract the claim from the ClaimsIdentity
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            paymentVM = new paymentVM()
            {
                PaymentList = _unit.payment.GetAll(u => u.UserID == claim.Value,
                    includeProperties: "tenancies"),
               
                    RentPaymentHeader = new RentPaymentHeader()
                };
                foreach (var paymentCart in paymentVM.PaymentList)
                {
                    paymentCart.amount = GetPriceBasedOnQuantity(paymentCart.Count, paymentCart.tenancies.rentPrice
                       );
                    paymentVM.RentPaymentHeader.PaymentAmount += (paymentCart.amount * paymentCart.Count);
                }
                return View(paymentVM);
            }
        public IActionResult paymentSummary()
        {
            //DS: we create a variable 'claimsIdentity' and we initialize it with the name identifier
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            //DS: we then create another variable 'claim' to extract the claim from the ClaimsIdentity
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            paymentVM = new paymentVM()
            {
               PaymentList = _unit.payment.GetAll(u => u.UserID == claim.Value,
                    includeProperties: "tenancies"),
                RentPaymentHeader = new RentPaymentHeader()
            };
            paymentVM.RentPaymentHeader.Users = _unit.Users.GetFirstOrDefault(u => u.Id == claim.Value);

            paymentVM.RentPaymentHeader.FirstName = paymentVM.RentPaymentHeader.Users.firstName;
            paymentVM.RentPaymentHeader.LastName = paymentVM.RentPaymentHeader.Users.lastName;
            paymentVM.RentPaymentHeader.PhoneNumber = paymentVM.RentPaymentHeader.Users.PhoneNumber;
            paymentVM.RentPaymentHeader.Email = paymentVM.RentPaymentHeader.Users.Email;
            paymentVM.RentPaymentHeader.Address = paymentVM.RentPaymentHeader.Users.Address;



            foreach (var paymentCart in paymentVM.PaymentList)
            {
                paymentCart.amount = GetPriceBasedOnQuantity(paymentCart.Count, paymentCart.tenancies.rentPrice
                   );
                paymentVM.RentPaymentHeader.PaymentAmount += (paymentCart.amount * paymentCart.Count);
            }
            return View(paymentVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult paymentSummaryPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            paymentVM.PaymentList = _unit.payment.GetAll(u => u.UserID == claim.Value,
                includeProperties: "tenancies");


            paymentVM.RentPaymentHeader.PaymentDate = System.DateTime.Now;
            paymentVM.RentPaymentHeader.UserId = claim.Value;


            foreach (var paymentCart in paymentVM.PaymentList)
            {
                paymentCart.amount = GetPriceBasedOnQuantity(paymentCart.Count, paymentCart.tenancies.rentPrice
                   );
                paymentVM.RentPaymentHeader.PaymentAmount += (paymentCart.amount * paymentCart.Count);
            }
            Users Users = _unit.Users.GetFirstOrDefault(u => u.Id == claim.Value);

            //some things might need to be added here

            _unit.RentPaymentHeader.Add(paymentVM.RentPaymentHeader);
            _unit.Save();
            foreach (var paymentCart in paymentVM.PaymentList)
            {
                RentPaymentDetails rentPayment = new RentPaymentDetails()
                {
                    TenancyId = paymentCart.TenancyID,
                    RentPaymentId = paymentVM.RentPaymentHeader.Id,
                    Price = paymentCart.amount,
                    Count = paymentCart.Count
                };
                _unit.RentPaymentDetails.Add(rentPayment);
                _unit.Save();
            }


            //some things might be added here at a later stage

            //DS: stripe settings from here
           
            var domain = "https://localhost:44318/";
            var options = new SessionCreateOptions //we create a session create option
            {
                PaymentMethodTypes = new List<string> //
                {
                  "card", // this is the option for payment method
                },
                LineItems = new List<SessionLineItemOptions>(), //DS: we create a new list of session LineItems,representing a list of the items from the shopping cart
                Mode = "payment",

                SuccessUrl = domain + $"Customer/ShoppingCart/OrderConfirmation?id={paymentVM.RentPaymentHeader.Id}",
                CancelUrl = domain + $"Customer/ShoppingCart/Index",
            };

            foreach (var item in paymentVM.PaymentList)
            {
                //DS: the code below represents the 'LineItems' options 
                //Diana Sandu(DS): so foreach statement will apply the options for each product in the ListCart

                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.amount * 100),//20.00 -> 2000
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                           Name = item.tenancies.PropertiesRS.propertyAddress
                        },

                    },
                    Quantity = item.Count,
                };
                //we basically add the options for each product in the LineItems created above
                options.LineItems.Add(sessionLineItem);

            }

            var service = new SessionService();
            Session session = service.Create(options);
            _unit.RentPaymentHeader.UpdateStripePaymentID(paymentVM.RentPaymentHeader.Id, session.Id, session.PaymentIntentId);
            _unit.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);


        }

        public IActionResult paymentConfirmation(int id)
        {
            //DS: retrieve order header from db based on the id
           RentPaymentHeader pFD = _unit.RentPaymentHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "Users");
            //DS: 
            if (pFD.PaymentStatus != StaticDetails.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(pFD.SessionId);
                //check the stripe status
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unit.RentPaymentHeader.UpdateStatus(id, StaticDetails.StatusApproved, StaticDetails.PaymentStatusApproved);
                    _unit.Save();
                }
            }
            
            List<payment> payments = _unit.payment.GetAll(u => u.UserID ==
            pFD.UserId).ToList();
            _unit.payment.RemoveRange(payments);
            _unit.Save();
            return View(id);
        }

    /*    public IActionResult Plus(int paymentId)
        {
            var cart = _unit.payment.GetFirstOrDefault(u => u.ID == paymentId);
            _unit.payment.IncrementCount(cart, 1);
            _unit.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int paymentId)
        {
            var paymentCart = _unit.payment.GetFirstOrDefault(u => u.ID == paymentId);
            if (paymentCart.Count <= 1)
            {
                _unit.payment.Remove(paymentCart);
                var count = _unit.payment.GetAll(u => u.UserID == paymentCart.UserID).ToList().Count - 1;
                HttpContext.Session.SetInt32(StaticDetails.SessionPayment, count);
            }
            else
            {
                _unit.payment.DecrementCount(paymentCart, 1);
            }
            _unit.Save();
            return RedirectToAction(nameof(Index));
        }*/

        public IActionResult Remove(int paymentId)
        {
            var paymentCart = _unit.payment.GetFirstOrDefault(u => u.ID == paymentId);
            _unit.payment.Remove(paymentCart);
            _unit.Save();
            var count = _unit.payment.GetAll(u => u.UserID == paymentCart.UserID).ToList().Count;
            /*     HttpContext.Session.SetInt32(SD.ssShoppingCart, count);*/
            return RedirectToAction(nameof(Index));
        }





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

