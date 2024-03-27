using ControllersLearning.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersLearning.Controllers {

    public class HomeController : Controller {

        [HttpPost]
        [Route("/order")]
        public IActionResult Index([Bind(nameof(Order.OrderDate),
            nameof(Order.InvoicePrice),
            nameof(Order.Products))]
            Order order) {
            if (!ModelState.IsValid) {
                string message = string.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }

            Random random = new Random();
            int randomOrderNumber = random.Next(1, 99999);
            return Json(new {
                orderNumber = randomOrderNumber,
                result = order
            });
        }
    }
}