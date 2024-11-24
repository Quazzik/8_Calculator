using _8_Calculator.DB.Entities;
using static _8_Calculator.Enums.OperationEnum;

namespace _8_Calculator
{
    public class CalcLib
    {
        private readonly DatabaseContext _context;

        public CalcLib(DatabaseContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var calculation = _context.Calculations.Find(id);

            if (calculation != null)
            {
                _context.Calculations.Remove(calculation);
                _context.SaveChanges();
            }
            return;
        }

        public string Calculate(double num1, double num2, string operation)
        {
            var res = GetResult(num1,num2,operation);

            _context.Calculations.Add(res);
            _context.SaveChanges();
            return res.ToString();
        }

        public static Calculation GetResult(double num1, double num2, string operation)
        {
            double result = 0;
            switch (operation)
            {
                case "Add":
                    result = num1 + num2;
                    break;
                case "Subtract":
                    result = num1 - num2;
                    break;
                case "Multiply":
                    result = num1 * num2;
                    break;
                case "Divide":
                    result = num1 / num2;
                    break;
            }
            var calc = new Calculation
            {
                Operand1 = num1,
                Operand2 = num2,
                Result = result,
                Operation = operation.ToString()
            };
            return calc;
        }
    }
}
