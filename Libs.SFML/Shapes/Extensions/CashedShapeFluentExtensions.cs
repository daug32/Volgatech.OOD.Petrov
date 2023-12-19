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
        shape.Position = new Vector2f( x, y );
        return shape;
    }
    
    public static T FluentSetPosition<T>( this T shape, Vector2f position ) where T : CashedShape
    {
        shape.Position = position;
        return shape;
    }
}