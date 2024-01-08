using System.Text.RegularExpressions;
using Lab1.Models;
using SFML.System;

namespace Lab1.Tasks.Parsers.Creators;

public class TriangleCreator
{
    private static TriangleCreator? _creator;

    private TriangleCreator()
    {
    }

    public static TriangleCreator GetInstance()
    {
        _creator ??= new TriangleCreator();
        return _creator;
    }

    public Triangle Create(Vector2f p0, Vector2f p1, Vector2f p2)
    {
        return new Triangle(p0, p1, p2);
    }

    public Triangle Create(string data)
    {
        MatchCollection pointsMatches = Regex.Matches( 
            data,
            RegexDictionary.NamedVector( "P\\d+" ) );
        
        if ( pointsMatches.Count != 3 )
        {
            ThrowInvalidDataException( data );
        }
        
        return Create(
            p0: new Vector2f(
                Single.Parse( pointsMatches[0].Groups[1].Value ),
                Single.Parse( pointsMatches[0].Groups[2].Value ) ),
            p1: new Vector2f(
                Single.Parse( pointsMatches[1].Groups[1].Value ),
                Single.Parse( pointsMatches[1].Groups[2].Value ) ),
            p2: new Vector2f(
                Single.Parse( pointsMatches[2].Groups[1].Value ),
                Single.Parse( pointsMatches[2].Groups[2].Value ) ));
    }

    private static void ThrowInvalidDataException(string data)
    {
        throw new ArgumentException( 
            $"Couldn't parse data for a triangle: \"{data}\"" );
    }
}