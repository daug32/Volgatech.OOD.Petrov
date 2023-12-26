using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.Shapes.Extensions;

public static class CashedShapeFluentExtensions
{
    public static T FluentSetOutlineColor<T>( this T shape, Color outlineColor ) where T : CashedShape
    {
        shape.OutlineColor = outlineColor;
        return shape;
    }

    public static T FluentSetOutlineThickness<T>( this T shape, float thickness ) where T : CashedShape
    {
        shape.OutlineThickness = thickness;
        return shape;
    }

    public static T FluentSetFillColor<T>( this T shape, Color fillColor ) where T : CashedShape
    {
        shape.FillColor = fillColor;
        return shape;
    }
    public static T FluentSetPosition<T>( this T shape, float x, float y ) where T : CashedShape
    {
        return shape.FluentSetPosition( new Vector2f( x, y ) );
    }
    
    public static T FluentSetPosition<T>( this T shape, Vector2f position ) where T : CashedShape
    {
        shape.Position = position;
        return shape;
    }

    public static T FluentSetCenterPosition<T>( this T shape, float x, float y ) where T : CashedShape
    {
        return shape.FluentSetCenterPosition( new Vector2f( x, y ) );
    } 

    public static T FluentSetCenterPosition<T>( this T shape, Vector2f position ) where T : CashedShape
    {
        FloatRect bounds = shape.GetGlobalBounds();
        shape.Position = new Vector2f(
            position.X - bounds.Width / 2,
            position.Y - bounds.Height / 2 );

        return shape;
    }

    public static T FluentSetMaxSize<T>( this T shape, Vector2f maxSize ) where T : CashedShape
    {
        FloatRect bounds = shape.GetGlobalBounds();

        float widthMultiplier = bounds.Width > maxSize.X
            ? maxSize.X / bounds.Width
            : shape.Scale.X;

        float heightMultiplier = bounds.Height > maxSize.Y
            ? maxSize.Y / bounds.Height
            : shape.Scale.Y;

        float resultMultiplier = MathF.Min( widthMultiplier, heightMultiplier );
        shape.Scale = new Vector2f( resultMultiplier, resultMultiplier );

        return shape;
    }
}