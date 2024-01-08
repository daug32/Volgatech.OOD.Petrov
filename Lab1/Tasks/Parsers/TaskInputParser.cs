using System.Text.RegularExpressions;
using Lab1.Models;
using SFML.System;

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

            switch ( shapeType )
            {
                case "triangle":
                    if ( !TryAddTriangle( line, data ) )
                    {
                        throw new ArgumentException( 
                            $"Couldn't parse data for a triangle: \"{line}\"" ); 
                    }
                    break;

                case "circle":
                    if ( !TryAddCircle( line, data ) ) 
                    {
                        throw new ArgumentException( 
                            $"Couldn't parse data for a circle: \"{line}\"" );
                    }
                    
                    break;
                case "rectangle":
                    if ( !TryAddRectangle( line, data) ) 
                    {
                        throw new ArgumentException( 
                            $"Couldn't parse data for a rectangle: \"{line}\"" );
                    }
                    
                    break;
                default:
                    throw new AggregateException( 
                        $"Unknown shape: \"{line}\"" );
            }
        }

        return data;
    }

    private static bool TryAddRectangle( string line, TaskInput data )
    {
        MatchCollection pointsMatches = Regex.Matches(
            line,
            RegexDictionary.NamedVector( "P\\d+" ) );
        if ( pointsMatches.Count != 2 )
        {
            return false;
        }

        var p0 = new Vector2f(
            Single.Parse( pointsMatches[0].Groups[1].Value ),
            Single.Parse( pointsMatches[0].Groups[2].Value ) );
        var p1 = new Vector2f(
            Single.Parse( pointsMatches[1].Groups[1].Value ),
            Single.Parse( pointsMatches[1].Groups[2].Value ) );
        
        data.Rectangles.Add( new Rectangle( p0, p1 ) );
        
        return true;
    }

    private static bool TryAddCircle( string line, TaskInput data )
    {
        MatchCollection pointsMatches = Regex.Matches( 
            line,
            RegexDictionary.NamedVector( "C" ) );
        if ( pointsMatches.Count != 1 )
        {
            return false;
        }

        MatchCollection radiusMatches = Regex.Matches( 
            line,
            RegexDictionary.NamedNumber( "R" ) );
        if ( radiusMatches.Count != 1 )
        {
            return false;
        }

        var center = new Vector2f(
            Single.Parse( pointsMatches[0].Groups[1].Value ),
            Single.Parse( pointsMatches[0].Groups[2].Value ) );

        float radius = Single.Parse( radiusMatches.First().Groups[1].Value );
        
        data.Circles.Add( new Circle( center, radius ) );
        
        return true;
    }

    private static bool TryAddTriangle( string line, TaskInput data )
    {
        MatchCollection pointsMatches = Regex.Matches( 
            line,
            RegexDictionary.NamedVector( "P\\d+" ) );
        
        if ( pointsMatches.Count != 3 )
        {
            return false;
        }

        var p0 = new Vector2f(
            Single.Parse( pointsMatches[0].Groups[1].Value ),
            Single.Parse( pointsMatches[0].Groups[2].Value ) );
        var p1 = new Vector2f(
            Single.Parse( pointsMatches[1].Groups[1].Value ),
            Single.Parse( pointsMatches[1].Groups[2].Value ) );
        var p2 = new Vector2f(
            Single.Parse( pointsMatches[2].Groups[1].Value ),
            Single.Parse( pointsMatches[2].Groups[2].Value ) );
        
        data.Triangles.Add( new Triangle( p0, p1, p2 ) );
        
        return true;
    }
}