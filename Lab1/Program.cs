using System.Text;
using Lab1.Tasks;
using Lab1.Tasks.Parsers;
using Libs.Extensions;

namespace Lab1;

public class Program
{
    private static readonly string _inputFile = "input.txt";
    private static readonly string _outputFile = "output.txt";
    
    public static void Main()
    {
        TaskInput? taskData = TaskInputParser.ParseFromFile( _inputFile );
        if ( taskData is null )
        {
            return;
        }

        var stringBuilder = new StringBuilder();
        taskData
            .GetSurfaces()
            .ForEach( surface => stringBuilder.AppendLine( surface.GetSurfaceInfo() ) );
        
        File.WriteAllText( _outputFile, stringBuilder.ToString() );
    }
}