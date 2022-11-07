namespace ConsoleApp
{
    public class BasicOperations
    {
        public static double Adition(double x, double y)
        {
            return x + y;
        }
        public static double Subtraction(double x, double y)
        {
            return x - y;
        }
        public static double Multiplication(double x, double y)
        {
            return x * y;
        }
        public static double Division(double x, double y)
        {
            if (y == 0)
                throw new DivideByZeroException("Erreur - Division par zéro indéfini");
            return x / y;
        }
        public static double Exponential(double x, double y)
        {
            return Math.Pow(x, y);
        }
        public static double SquareRoot(double x)
        {
            return Math.Sqrt(x);
        }
    }
}
