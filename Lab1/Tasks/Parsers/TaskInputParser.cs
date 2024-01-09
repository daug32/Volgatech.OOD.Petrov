using Lab1.Models;
using Lab1.Tasks.Parsers.Implementation;

namespace Lab1.Tasks.Parsers;

internal static class TaskInputParser
{
    private static readonly Dictionary<ShapeType, IShapeParser> _parsers = BuildShapeParsers();

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
        var filteredData = rawTaskData
            .Select( line => line.Trim() )
            .Where( line => !String.IsNullOrWhiteSpace( line ) );

        var data = new TaskInput();

        foreach ( string line in filteredData )
        {
            IShapeParser shapeParser = _parsers[ParseShapeType( line )];
            data.Shapes.Add( shapeParser.ParseShape( line ) );
        }

        return data;
    }

    private static ShapeType ParseShapeType( string line )
    {
        // Parse shape type from something like this:
        //  "TRIANGLE: ...",
        //  "TRIANGLE  : ...",
        //  "  TRIANGLE: ..." 
        string rawShapeType = line
            .Substring( 0, line.IndexOf( ':' ) )
            .Trim()
            .ToLower();

        if ( !Enum.TryParse( rawShapeType, true, out ShapeType shapeType ) )
        {
            throw new AggregateException( $"Unknown shape: \"{line}\"" );
        }

        return shapeType;
    }

    private static Dictionary<ShapeType, IShapeParser> BuildShapeParsers() => new()
    {
        { ShapeType.Triangle, TriangleParser.GetInstance() },
        { ShapeType.Circle, CircleParser.GetInstance() },
        { ShapeType.Rectangle, RectangleParser.GetInstance() }
    };
}