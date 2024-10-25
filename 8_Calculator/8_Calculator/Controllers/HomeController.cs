using _8_Calculator.DB.Entities;
using _8_Calculator.Enums;
using _8_Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
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
        public IActionResult Index()
        {
            SetOldResults();
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
            SetOldResults();
            return View("Index");
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

            SetOldResults();

            return View("Index");
        }
        public void SetOldResults()
        {
            var calculations = _context.Calculations.ToList();
            foreach (var calculation in calculations)
            {
                calculation.Operation = OperationEnum.Convert(calculation.Operation);
            }
            ViewBag.Calculations = calculations;
        }
    }
}
