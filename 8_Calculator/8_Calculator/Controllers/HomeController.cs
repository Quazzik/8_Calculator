using _8_Calculator.DB.Entities;
using _8_Calculator.Enums;
using Microsoft.AspNetCore.Mvc;
using static _8_Calculator.Enums.OperationEnum;

namespace _8_Calculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly BatabaseContext _context;

        public HomeController(BatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
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

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var calculation = _context.Calculations.Find(Id);

            if (calculation != null)
            {
                _context.Calculations.Remove(calculation);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calculate(double num1, double num2, OperationType operation)
        {
            double result = 0;
            switch (operation)
            {
                case OperationType.Add:
                    result = num1 + num2;
                    break;
                case OperationType.Subtract:
                    result = num1 - num2;
                    break;
                case OperationType.Multiply:
                    result = num1 * num2;
                    break;
                case OperationType.Divide:
                    result = num1 / num2;
                    break;
            }
            ViewBag.Result = result;

            var calc = new Calculation
            {
                Operand1 = num1,
                Operand2 = num2,
                Result = result,
                Operation = operation.ToString()
            };
            _context.Calculations.Add(calc);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
