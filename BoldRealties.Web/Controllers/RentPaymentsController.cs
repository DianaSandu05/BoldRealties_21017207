using BoldRealties.BLL;
using BoldRealties.DAL.Repository.IRepository;
using BoldRealties.Models;
using BoldRealties.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace BoldRealties.Web.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RentPaymentController : Controller
    {
        private readonly IUnitOfWork _unit;
        [BindProperty]
        public RentPaymentVM RentPaymentVM { get; set; }
        public RentPaymentController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int RentPaymentId)
        {
            RentPaymentVM = new RentPaymentVM()
            {
                RentPaymentHeader = _unit.RentPaymentHeader.GetFirstOrDefault(u => u.Id == RentPaymentId, includeProperties: "Users"),
                RentPaymentDetails = _unit.RentPaymentDetails.GetAll(u => u.RentPaymentId == RentPaymentId, includeProperties: "Tenancy"),
            };
            return View(RentPaymentVM);
        }

        [ActionName("Details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details_PAY_NOW()
        {
            RentPaymentVM.RentPaymentHeader = _unit.RentPaymentHeader.GetFirstOrDefault(u => u.Id == RentPaymentVM.RentPaymentHeader.Id, includeProperties: "Users");
            RentPaymentVM.RentPaymentDetails = _unit.RentPaymentDetails.GetAll(u => u.RentPaymentId == RentPaymentVM.RentPaymentHeader.Id, includeProperties: "Tenancy");

            //stripe settings 
            var domain = "https://localhost:44300/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"admin/order/PaymentConfirmation?orderHeaderid={RentPaymentVM.RentPaymentHeader.Id}",
                CancelUrl = domain + $"admin/order/details?orderId={RentPaymentVM.RentPaymentHeader.Id}",
            };

            foreach (var item in RentPaymentVM.RentPaymentDetails)
            {

                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),//20.00 -> 2000
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Tenancies.PropertiesRS.propertyAddress
                        },

                    },
                    Quantity = item.Count,
                };
                options.LineItems.Add(sessionLineItem);

            }

            var service = new SessionService();
            Session session = service.Create(options);
            _unit.RentPaymentHeader.UpdateStripePaymentID(RentPaymentVM.RentPaymentHeader.Id, session.Id, session.PaymentIntentId);
            _unit.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult PaymentConfirmation(int orderHeaderid)
        {
            RentPaymentHeader paymentHeader = _unit.RentPaymentHeader.GetFirstOrDefault(u => u.Id == orderHeaderid);
            if (paymentHeader.PaymentStatus == StaticDetails.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(paymentHeader.SessionId);
                //check the stripe status
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unit.RentPaymentHeader.UpdateStatus(orderHeaderid,paymentHeader.OrderStatus, StaticDetails.PaymentStatusApproved);
                    _unit.Save();
                }
            }
            return View(orderHeaderid);
        }

        [HttpPost]
        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_User)]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderDetail()
        {
            var objFromDb = _unit.RentPaymentHeader.GetFirstOrDefault(u => u.Id == RentPaymentVM.RentPaymentHeader.Id, tracked: false);
            objFromDb.FirstName = RentPaymentVM.RentPaymentHeader.FirstName;
            objFromDb.LastName = RentPaymentVM.RentPaymentHeader.LastName;
            objFromDb.PhoneNumber = RentPaymentVM.RentPaymentHeader.PhoneNumber;
            objFromDb.Email = RentPaymentVM.RentPaymentHeader.Email;
            objFromDb.Address = RentPaymentVM.RentPaymentHeader.Address;

            if (RentPaymentVM.RentPaymentHeader.Carrier != null)
            {
                objFromDb.Carrier = RentPaymentVM.RentPaymentHeader.Carrier;
            }
            if (RentPaymentVM.RentPaymentHeader.TrackingNumber != null)
            {
                objFromDb.TrackingNumber = RentPaymentVM.RentPaymentHeader.TrackingNumber;
            }
            _unit.RentPaymentHeader.Update(objFromDb);
            _unit.Save();
            TempData["Success"] = "Order Details Updated Successfully.";
            return RedirectToAction("Details", "Order", new { orderId = objFromDb.Id });
        }

        [HttpPost]
        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_User)]
        [ValidateAntiForgeryToken]
        public IActionResult StartProcessing()
        {
            _unit.RentPaymentHeader.UpdateStatus(RentPaymentVM.RentPaymentHeader.Id, StaticDetails.StatusInProcess);
            _unit.Save();
            TempData["Success"] = "Order Status Updated Successfully.";
            return RedirectToAction("Details", "Order", new { orderId = RentPaymentVM.RentPaymentHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_User)]
        [ValidateAntiForgeryToken]
        public IActionResult ShipOrder()
        {
            var orderHeader = _unit.RentPaymentHeader.GetFirstOrDefault(u => u.Id == RentPaymentVM.RentPaymentHeader.Id, tracked: false);
            orderHeader.TrackingNumber = RentPaymentVM.RentPaymentHeader.TrackingNumber;
            orderHeader.Carrier = RentPaymentVM.RentPaymentHeader.Carrier;
            orderHeader.OrderStatus = StaticDetails.StatusShipped;
            orderHeader.ShippingDate = DateTime.Now;
            if (orderHeader.PaymentStatus == StaticDetails.PaymentStatusDelayedPayment)
            {
                orderHeader.PaymentDueDate = DateTime.Now.AddDays(30);
            }
            _unit.RentPaymentHeader.Update(orderHeader);
            _unit.Save();
            TempData["Success"] = "Order Shipped Successfully.";
            return RedirectToAction("Details", "Order", new { orderId = RentPaymentVM.RentPaymentHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = StaticDetails.Role_Admin + "," + StaticDetails.Role_User)]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder()
        {
            var orderHeader = _unit.RentPaymentHeader.GetFirstOrDefault(u => u.Id == RentPaymentVM.RentPaymentHeader.Id, tracked: false);
            if (orderHeader.PaymentStatus == StaticDetails.PaymentStatusApproved)
            {
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentIntentId
                };

                var service = new RefundService();
                Refund refund = service.Create(options);

                _unit.RentPaymentHeader.UpdateStatus(orderHeader.Id, StaticDetails.StatusCancelled, StaticDetails.StatusRefunded);
            }
            else
            {
                _unit.RentPaymentHeader.UpdateStatus(orderHeader.Id, StaticDetails.StatusCancelled, StaticDetails.StatusCancelled);
            }
            _unit.Save();

            TempData["Success"] = "Order Cancelled Successfully.";
            return RedirectToAction("Details", "Order", new { orderId = RentPaymentVM.RentPaymentHeader.Id });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<RentPaymentHeader> orderHeaders;

            if (User.IsInRole(StaticDetails.Role_Admin) || User.IsInRole(StaticDetails.Role_User))
            {
                orderHeaders = _unit.RentPaymentHeader.GetAll(includeProperties: "Users");
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeaders = _unit.RentPaymentHeader.GetAll(u => u.UserId == claim.Value, includeProperties: "Users");
            }

            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(u => u.PaymentStatus == StaticDetails.PaymentStatusDelayedPayment);
                    break;
                case "inprocess":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == StaticDetails.StatusInProcess);
                    break;
                case "completed":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == StaticDetails.StatusShipped);
                    break;
                case "approved":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == StaticDetails.StatusApproved);
                    break;
                default:
                    break;
            }


            return Json(new { data = orderHeaders });
        }
        #endregion
    }
}
