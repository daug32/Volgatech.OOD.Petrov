﻿using SFML.System;

namespace Libs.SFML.Vertices;

public static class Vector2Utils
{
    public static Vector2f GetRandomInBounds( Vector2u bounds )
    {
        return GetRandom( bounds.X, bounds.Y );
    }

    public static Vector2f GetRandom( float width, float height )
    {
        var randomizer = new Random();
        return new Vector2f( randomizer.NextSingle() * width, randomizer.NextSingle() * height );
    }
}