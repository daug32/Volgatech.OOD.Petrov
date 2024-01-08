using Lab1.Models;
using Lab1.Tasks.Parsers.Creators;

namespace Lab1.Tasks.Parsers;

public static class TaskInputParser
{
    public static TaskInput ParseFromFile( string inputFilePath )
    {
        if ( !File.Exists( inputFilePath ) )
        {
            throw new ArgumentException( 
                $"Input file was not found. Path: {Path.GetFullPath( inputFilePath )}" );
        }

        return ParseFromStrings( File.ReadAllLines( inputFilePath ) );
    }

    private static TaskInput ParseFromStrings( IEnumerable<string> rawTaskData )
    {
        IEnumerable<string> filteredData = rawTaskData
            .Select( line => line.Trim() )
            .Where( line => !String.IsNullOrWhiteSpace( line ) );
        
        var data = new TaskInput();
        
        foreach ( string line in filteredData )
        {
            // Parse shape type from something like this:
            //  "TRIANGLE: ...",
            //  "TRIANGLE  : ...",
            //  "  TRIANGLE: ..."  
            string shapeType = line
                .Substring( 0, line.IndexOf( ':' ) )
                .Trim()
                .ToLower();

            ISurface surface = BuildSurface( shapeType, line );
            
            data.Surfaces.Add(surface);
        }

        return data;
    }

    private static ISurface BuildSurface( string shapeType, string data )
    {
        return shapeType switch
        {
            "triangle" => TriangleCreator.GetInstance().Create(data),
            "circle" => CircleCreator.GetInstance().Create(data),
            "rectangle" => RectangleCreator.GetInstance().Create(data),
            _ => throw new AggregateException($"Unknown shape: \"{data}\"")
        };
    }
}