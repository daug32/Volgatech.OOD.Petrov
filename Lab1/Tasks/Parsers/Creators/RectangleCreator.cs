using System.Text.RegularExpressions;
using Lab1.Models;
using SFML.System;

namespace Lab1.Tasks.Parsers.Creators;

public class RectangleCreator
{
    private static RectangleCreator? _creator;

    private RectangleCreator()
    {
    }

    public static RectangleCreator GetInstance()
    {
        _creator ??= new RectangleCreator();
        return _creator;
    }

    public Rectangle Create( Vector2f leftTop, Vector2f rightBottom )
    {
        return new Rectangle( leftTop, rightBottom );
    }

    public Rectangle Create( string data )
    {
        MatchCollection pointsMatches = Regex.Matches(
            data,
            RegexDictionary.NamedVector( "P\\d+" ) );

        if ( pointsMatches.Count != 2 )
        {
            ThrowInvalidDataException( data );
        }

        return Create(
            new Vector2f(
                Single.Parse( pointsMatches[0].Groups[1].Value ),
                Single.Parse( pointsMatches[0].Groups[2].Value ) ),
            new Vector2f(
                Single.Parse( pointsMatches[1].Groups[1].Value ),
                Single.Parse( pointsMatches[1].Groups[2].Value ) ) );
    }

    private static void ThrowInvalidDataException( string data )
    {
        throw new ArgumentException(
            $"Couldn't parse data for a rectangle: \"{data}\"" );
    }
}