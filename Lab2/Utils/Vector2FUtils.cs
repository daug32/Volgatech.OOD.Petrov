using SFML.System;

namespace Lab2.Utils;

public static class Vector2FUtils
{
    public static Vector2f GetRandomInBounds( Vector2u bounds ) => GetRandom( bounds.X, bounds.Y );

    public static Vector2f GetRandom( float width, float height )
    {
        var randomizer = new Random();
        return new Vector2f( randomizer.NextSingle() * width, randomizer.NextSingle() * height );
    }
}