using _8_Calculator.Enums;
using System.ComponentModel.DataAnnotations;

namespace _8_Calculator.DB.Entities
{
    public class Calculation
    {
        [Key]
        public int Id { get; set; }

        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }
    }
}
