using BoldRealties.BLL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LCDSArtGalleryWeb.Areas.Customer.Controllers
{

    [Authorize]
    public class RentPaymentsController : Controller
    {
        private readonly IUnitOfWork _unit;
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public RentPaymentsController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        [Authorize(Roles = StaticDetails.Role_Admin)]
        //displays the list of payments in the admin portal

        public IActionResult Index()
        {
            IEnumerable<OrderHeader> obj = _unit.OrderHeader.GetAll();
            return View(obj);
        }
        //get details about payments made for the logged in user
        [Authorize(Roles = StaticDetails.Role_Tenant)]

        public IActionResult TenantPayments()
        {
            // we create a variable 'claimsIdentity' and we initialize it with the name identifier
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            // we then create another variable 'claim' to extract the claim from the ClaimsIdentity
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<OrderHeader> obj = _unit.OrderHeader.GetAll().Where(u=> u.UserId == claim.Value);
            return View(obj);
        }
   
        public IActionResult Details(int orderId)
        {
            // used to display the info about order and user in the summary
            OrderVM = new OrderVM()
            {
                OrderHeader = _unit.OrderHeader.GetFirstOrDefault(u => u.Id == orderId, includeProperties: "Users"),
                OrderDetails = _unit.OrderDetails.GetAll(u => u.OrderId == orderId, includeProperties: "tenancies"),
            };
            return View(OrderVM);
        }

        [ActionName("Details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Function used for payment and for getting all the info about order and user for stripe payment
        public IActionResult Details_PAY_NOW()
        {
            OrderVM.OrderHeader = _unit.OrderHeader.GetFirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id, includeProperties: "Users");
            OrderVM.OrderDetails = _unit.OrderDetails.GetAll(u => u.OrderId == OrderVM.OrderHeader.Id, includeProperties: "tenancies");

            //stripe settings 
            // when the payment succedeed, the user is redirected from stripe portal back to the web app LCDSArtGallery
            //the domain is needed for redirecting the user back
            var domain = "https://localhost:7208/";
            //payment options. Same as in shopping cart controller. I explained the process of payment options there
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"payment/PaymentConfirmation?orderHeaderid={OrderVM.OrderHeader.Id}",
                CancelUrl = domain + $"payment/details?orderId={OrderVM.OrderHeader.Id}",
            };

            foreach (var item in OrderVM.OrderDetails)
            {
                // the code below represents the 'LineItems' options 
                // so foreach statement will apply the options
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),//20.00 -> 2000
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.tenancies.Id.ToString()
                        },

                    },
                    Quantity = item.Count,
                };
                options.LineItems.Add(sessionLineItem);

            }

            var service = new SessionService();
            Session session = service.Create(options);
            _unit.OrderHeader.UpdateStripePaymentID(OrderVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unit.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        // function for payment confirmation
        public IActionResult PaymentConfirmation(int orderHeaderid)
        {
            //get the id of order header and compare it to the param value, when the record is found its value is assigned to the obj orderHeader
            OrderHeader orderHeader = _unit.OrderHeader.GetFirstOrDefault(u => u.Id == orderHeaderid);
            if (orderHeader.PaymentStatus == StaticDetails.PaymentStatusDelayedPayment)
            {
                // create instance of SessionService and use this service to get the session id & check the stripe status
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                //check the stripe status
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unit.Save();
                }
            }
            return View(orderHeaderid);
        }
    }
}
