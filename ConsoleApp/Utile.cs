using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public class Utile
    {
        public Utile()
        {

        }
        public List<string> SeparateNumbersSigns(string text)
        {
            StringBuilder str = new();
            List<string> list = new();
            bool isSqrt = false;

            //supprimer les espaces
            text = Regex.Replace(text, @"\s+", String.Empty);

            foreach (var item in text)
            {
                if (double.TryParse(item.ToString(), out double aux)) //c'est un nombre
                    str.Append(aux);

                else if (item == '.' || item == ',') //c'est un decimal
                    str.Append(',');

                //ce n'est pas un nombre
                else
                {
                    if (str.Length != 0 && !isSqrt) //ajouter un numéro à la liste s'il n'est pas vide
                    {
                        list.Add(str.ToString());
                        str.Clear();
                    }

                    //sqrt
                    if ("sqrt".Contains(item))
                    {
                        isSqrt = true;
                        str.Append(item);

                        if ("t".Contains(item))
                            isSqrt = false;
                    }

                    //ajouter le signe si le caractère précédent est un nombre ou une parenthèse
                    else if (item == '(' || item == ')' ||
                        (list.Count > 0 && (double.TryParse(list[^1].ToString(), out aux) || list[^1] == ")")))
                        list.Add(item.ToString());

                    else if (item == '-') //nombre négatif
                        str.Append('-');

                    else //deux ou plusieurs signes consécutifs
                        throw new Exception("Chaîne Invalide");
                }
            }

            //ajouter le dernier numéro
            if (str.Length != 0)
            {
                list.Add(str.ToString());
                str.Clear();
            }

            //vérifier que les parenthèses sont paires et sont dans l´order correct
            int ind = list.FindIndex(x => x.Contains('(') || x.Contains(')'));
            if (ind != -1 && !ValidateParenthesesOrder(list))
                throw new Exception("Chaîne Invalide");

            return list;
        }

        public static bool ValidateParenthesesOrder(List<string> list)
        {
            string[] queue = new string[list.Count];
            int index = 0;

            foreach (var item in list)
            {
                if (item == "(")
                {
                    if (index > 0)
                        queue.SetValue(item.ToString(), index);
                    index++;
                }
                else if (item == ")")
                {
                    index--;

                    if (index > 0)
                        queue.SetValue(null, index);
                }
            }

            if (queue[0] == null && index == 0)
                return true;
            return false;
        }
    }
}
