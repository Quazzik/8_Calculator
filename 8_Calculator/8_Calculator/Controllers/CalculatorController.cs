using _8_Calculator.DB.Entities;
using _8_Calculator.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly BatabaseContext _context;

        public CalculatorController(BatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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

            return View("Index");
        }
    }
}
