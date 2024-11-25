using _8_Calculator.Enums;
using _8_Calculator.Kafka;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using static _8_Calculator.Enums.OperationEnum;

namespace _8_Calculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly CalcLib _calcLib;

        public HomeController(DatabaseContext context, KafkaProducerService<Null, string> producer)
        {
            _context = context;
            _calcLib = new CalcLib(context, producer);
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            if (_context.Calculations != null)
            {
                const int pageSize = 8; // количество записей на странице
                var calculations = _context.Calculations.ToList();
                calculations.Reverse();

                foreach (var calculation in calculations)
                {
                    calculation.Operation = OperationEnum.Convert(calculation.Operation);
                }

                var totalCount = calculations.Count;
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                var results = calculations.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.Calculations = results;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _calcLib.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calculate(double num1, double num2, OperationType operation)
        {
            TempData["Result"] = _calcLib.Calculate(num1, num2, operation.ToString());
            return RedirectToAction("Index");
        }
    }
}