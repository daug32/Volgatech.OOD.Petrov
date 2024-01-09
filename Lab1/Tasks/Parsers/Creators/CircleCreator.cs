using System.Text.RegularExpressions;
using Lab1.Models;
using SFML.System;

namespace Lab1.Tasks.Parsers.Creators;

public class CircleCreator
{
    private static CircleCreator? _creator;

    private CircleCreator()
    {
    }

    public static CircleCreator GetInstance()
    {
        _creator ??= new CircleCreator();
        return _creator;
    }

    public Circle Create( Vector2f center, float radius )
    {
        return new Circle( center, radius );
    }

    public Circle Create( string data )
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

        return Create(
            new Vector2f(
                Single.Parse( pointsMatches[0].Groups[1].Value ),
                Single.Parse( pointsMatches[0].Groups[2].Value ) ),
            Single.Parse( radiusMatches.First().Groups[1].Value ) );
    }

    private static void ThrowInvalidDataException( string data )
    {
        throw new ArgumentException(
            $"Couldn't parse data for a circle: \"{data}\"" );
    }
}