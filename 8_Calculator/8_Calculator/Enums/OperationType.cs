using _8_Calculator.DB.Entities;

namespace _8_Calculator.Enums
{
    public class OperationEnum
    {
        public enum OperationType
        {
            Add,
            Subtract,
            Multiply,
            Divide
        }

        public static string Convert(string operand)
        {
            string ret = operand switch //переписать switch на старую анотацию
            {
                "Add" => "+",
                "Subtract" => "-",
                "Multiply" => "*",
                "Divide" => "/",
                _ => "Ошибка"
            };
            return ret;
        }
    }
}