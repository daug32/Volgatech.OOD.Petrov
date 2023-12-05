using SFML.Graphics;
using SFML.System;

namespace Lab2.Extensions;

public static class TransformableExtensions
{
    public static T FluentSetPosition<T>( this T shape, float x, float y ) where T : Transformable
    {
        shape.Position = new Vector2f( x, y );
        return shape;
    }
    
    public static T FluentSetPosition<T>( this T shape, Vector2f position ) where T : Transformable
    {
        shape.Position = position;
        return shape;
    }
}