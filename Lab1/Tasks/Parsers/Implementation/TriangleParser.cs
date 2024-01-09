using System.Text.RegularExpressions;
using Lab1.Models;
using Lab1.Models.Implementation;
using Lab1.Tasks.Parsers.Dictionaries;
using SFML.System;

namespace Lab1.Tasks.Parsers.Implementation;

internal class TriangleParser : IShapeParser
{
    private static TriangleParser? _creator;

    private TriangleParser()
    {
    }

    public static TriangleParser GetInstance()
    {
        _creator ??= new TriangleParser();
        return _creator;
    }

    public IShape ParseShape( string data )
    {
        MatchCollection pointsMatches = Regex.Matches(
            data,
            RegexDictionary.NamedVector( "P\\d+" ) );
        if ( pointsMatches.Count != 3 )
        {
            ThrowInvalidDataException( data );
        }

        return new Triangle(
            p0: new Vector2f(
                Single.Parse( pointsMatches[0].Groups[1].Value ),
                Single.Parse( pointsMatches[0].Groups[2].Value ) ),
            p1: new Vector2f(
                Single.Parse( pointsMatches[1].Groups[1].Value ),
                Single.Parse( pointsMatches[1].Groups[2].Value ) ),
            p2: new Vector2f(
                Single.Parse( pointsMatches[2].Groups[1].Value ),
                Single.Parse( pointsMatches[2].Groups[2].Value ) ) );
    }

    private static void ThrowInvalidDataException( string data )
    {
        throw new ArgumentException(
            $"Couldn't parse data for a triangle: \"{data}\"" );
    }
}