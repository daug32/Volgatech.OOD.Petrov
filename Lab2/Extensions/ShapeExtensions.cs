using SFML.Graphics;

namespace Lab2.Extensions;

public static class ShapeExtensions
{
    public static T FluentSetOutlineColor<T>( this T shape, Color outlineColor ) where T : Shape
    {
        shape.OutlineColor = outlineColor;
        return shape;
    }

    public static T FluentSetOutlineThickness<T>( this T shape, float thickness ) where T : Shape
    {
        shape.OutlineThickness = thickness;
        return shape;
    }

    public static T FluentSetFillColor<T>( this T shape, Color fillColor ) where T : Shape
    {
        shape.FillColor = fillColor;
        return shape;
    }
}