using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public class Calculatrice
    {
        double result;
        public Calculatrice()
        {
        }
        
        public double Calculate(List<string> list)
        {
            //déterminer s'il y a des opérations prioritaires
            int index = list.FindIndex(x => x.Contains('*') || x.Contains('/') || x.Contains('^') || x.Contains("sqrt"));

            //parenthèse
            int indexParenthesis = list.FindIndex(x => x.Contains('('));
            bool sqrt = indexParenthesis > 0 && list[indexParenthesis - 1].Contains("sqrt");

            if (indexParenthesis != -1 && !sqrt)
                list = ParenthesisOperations(list, indexParenthesis);

            else if (index != -1) // * / ^ sqrt
                list = PrioritizedOperations(list);
            else
                //addition et soustraction
                list = NonPrioritizedOperations(list);

            //recursividad
            if (list.Count == 1)
                result = double.Parse(list[0]);
            else
                Calculate(list);

            return result;
        }

        static List<string> PrioritizedOperations(List<string> list)
        {
            double answer = 0;

            for (int i = 0; i < list.Count; i++)
                if (!int.TryParse(list[i].ToString(), out int aux)) //si ce n'est un numéro
                    switch (list[i])
                    {
                        case "*":
                            answer = BasicOperations.Multiplication(double.Parse(list[i - 1]), double.Parse(list[i + 1]));
                            list = UpdateList(list, answer, i);
                            break;
                        case "/":
                            answer = BasicOperations.Division(double.Parse(list[i - 1]), double.Parse(list[i + 1]));
                            list = UpdateList(list, answer, i);
                            break;
                        case "^":
                            answer = BasicOperations.Exponential(double.Parse(list[i - 1]), double.Parse(list[i + 1]));
                            list = UpdateList(list, answer, i);
                            break;
                        case "sqrt":
                            int index = list.FindIndex(x => x.Contains("sqrt"));
                            answer = BasicOperations.SquareRoot(double.Parse(list[index + 2]));
                            list = UpdateList(list, answer, index + 1, 3);
                            break;
                    }
            return list;
        }

        static List<string> NonPrioritizedOperations(List<string> list)
        {
            double answer;

            for (int i = 0; i < list.Count; i++)
                if (!int.TryParse(list[i].ToString(), out int aux)) //si ce n'est un numéro
                    switch (list[i])
                    {
                        case "+":
                            answer = BasicOperations.Adition(double.Parse(list[i - 1]), double.Parse(list[i + 1]));
                            list = UpdateList(list, answer, i);
                            break;
                        case "-":
                            answer = BasicOperations.Subtraction(double.Parse(list[i - 1]), double.Parse(list[i + 1]));
                            list = UpdateList(list, answer, i);
                            break;
                    }
            return list;
        }

        List<string> ParenthesisOperations(List<string> list, int indexOpen)
        {
            int indexClose = MatchingParentheses(list,indexOpen);
            int count = indexClose - indexOpen;
            if (count==1)
            {
                throw new Exception("Chaîne Invalide");
            }
            List<string> sublist = list.GetRange(indexOpen + 1, count - 1); //hors parenthèses
            double answer = Calculate(sublist);
            list = UpdateList(list, answer, indexOpen + 1, count);
            return list;
        }

        static List<string> UpdateList(List<string> list, double value, int i, int count = 2) 
        {
            //ajouter la valeur de l'opération effectuée et supprimer les opérandes
            list.RemoveRange(i, count);
            list[i - 1] = value.ToString();
            return list;
        }

        static int MatchingParentheses(List<string> list, int indexOpen)
        {
            int cont =0, res = 0;
            bool found = false;

            for (int i = indexOpen+1; !found ; i++)
            {
                if (list[i] == ")" && cont == 0)
                {
                    found = true;
                    res = i;
                }
                else if (list[i] == "(")
                    cont++;
                else if (list[i] == ")")
                    cont--;
            }
            return res;
        }
    }
}
