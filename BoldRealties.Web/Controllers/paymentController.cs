using Microsoft.AspNetCore.Mvc;
using BoldRealties.Models;
using BoldRealties.DAL.Repository.IRepository;
using Stripe.Checkout;

namespace BoldRealties.Web.Controllers
{
    public class paymentController : Controller
    {
        private readonly IUnitOfWork _unit;
        public paymentController(IUnitOfWork unit) //implementation of connection string and table to retrieve data
        {
            _unit = unit;
        }
        public IActionResult Summary()
        {
            IEnumerable<payment> objpaymentList = _unit.payment.GetAll();
            return View(objpaymentList);


            //stripe settings
            var domain = "https://localhost:44368/";
            // below a session is created and in this session a list is created
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
        {
          new SessionLineItemOptions
          {
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmount = 2000,
              Currency = "usd",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = "T-shirt",
              },

            },
            Quantity = 1,
          },
        },
                Mode = "payment",
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
