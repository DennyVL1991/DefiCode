using ConsoleApp;

//Code Golf - Calculatrice par chaîne de caractères
Console.WriteLine("Entrez la chaîne de caractères");
string text = Console.ReadLine();

List<string> lista;
double result = 0;

try
{
    //valider que la chaîne ne se termine pas par un signe
    string signs = "+-*/(sqrt^";
    if (signs.Contains(text[^1].ToString())) 
        throw new Exception("Chaîne Invalide");

    Calculatrice calculatrice = new();
    Utile utile = new();
    lista = utile.SeparateNumbersSigns(text); 
    result = calculatrice.Calculate(lista);

    //arrondir
    result = Math.Round(result, 1);

    Console.WriteLine($"Le résultat est : {result}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}