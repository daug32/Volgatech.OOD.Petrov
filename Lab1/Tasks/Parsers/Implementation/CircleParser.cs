using System.Text.RegularExpressions;
using Lab1.Models;
using Lab1.Models.Implementation;
using Lab1.Tasks.Parsers.Dictionaries;
using SFML.System;

namespace Lab1.Tasks.Parsers.Implementation;

internal class CircleParser : IShapeParser
{
    private static CircleParser? _creator;

    private CircleParser()
    {
    }

    public static CircleParser GetInstance()
    {
        _creator ??= new CircleParser();
        return _creator;
    }

    public IShape ParseShape( string data )
    {
        MatchCollection pointsMatches = Regex.Matches(
            data,
            RegexDictionary.NamedVector( "C" ) );
        if ( pointsMatches.Count != 1 )
        {
            ThrowInvalidDataException( data );
        }

        MatchCollection radiusMatches = Regex.Matches(
            data,
            RegexDictionary.NamedNumber( "R" ) );
        if ( radiusMatches.Count != 1 )
        {
            ThrowInvalidDataException( data );
        }

        return new Circle(
            center: new Vector2f(
                Single.Parse( pointsMatches[0].Groups[1].Value ),
                Single.Parse( pointsMatches[0].Groups[2].Value ) ),
            radius: Single.Parse( radiusMatches.First().Groups[1].Value ) );
    }

    private static void ThrowInvalidDataException( string data )
    {
        throw new ArgumentException(
            $"Couldn't parse data for a circle: \"{data}\"" );
    }
}