namespace Lab1.Tasks.Parsers;

internal class RegexDictionary
{
    public static readonly string Number = @"-{0,1}\d+";
    
    public static readonly string Vector = $"({Number})\\s*,\\s*({Number})";
    
    public static string NamedVector( string namePattern ) => $"{namePattern}\\s*[=:]\\s*{Vector}\\s*";
    
    public static string NamedNumber( string namePattern ) => $"{namePattern}\\s*[=:]\\s*({Number})\\s*";
}