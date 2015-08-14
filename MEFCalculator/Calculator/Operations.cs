using System.ComponentModel.Composition;

namespace Calculator
{

    [Export("Shared.IOperations")]
    public class Operations
    {
        public int Add(int number1, int number2)
        {
            return number1 + number2;
        }

        public int Subtract(int number1, int number2)
        {
            return number1 - number2;
        }

        public long Multiply(int number1, int number2)
        {
            return number1*number2;
        }

        public double Divide(int number1, int number2)
        {
            return number1/number2;
        }
    }
}