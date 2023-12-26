using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.Shapes.Extensions;

public static class ShapeDecoratorFluentExtensions
{
    public static T SetOutlineColor<T>( this T shape, Color outlineColor ) where T : ShapeDecorator
    {
        shape.OutlineColor = outlineColor;
        return shape;
    }

    public static T SetOutlineThickness<T>( this T shape, float thickness ) where T : ShapeDecorator
    {
        shape.OutlineThickness = thickness;
        return shape;
    }

    public static T SetFillColor<T>( this T shape, Color fillColor ) where T : ShapeDecorator
    {
        shape.FillColor = fillColor;
        return shape;
    }

    public static T SetPosition<T>( this T shape, float x, float y ) where T : ShapeDecorator
    {
        return shape.SetPosition( new Vector2f( x, y ) );
    }

    public static T SetPosition<T>( this T shape, Vector2f position ) where T : ShapeDecorator
    {
        shape.Position = position;
        return shape;
    }

    public static T SetCenterPosition<T>( this T shape, float x, float y ) where T : ShapeDecorator
    {
        return shape.SetCenterPosition( new Vector2f( x, y ) );
    }

    public static T SetCenterPosition<T>( this T shape, Vector2f position ) where T : ShapeDecorator
    {
        FloatRect bounds = shape.GetGlobalBounds();
        shape.Position = new Vector2f(
            position.X - bounds.Width / 2,
            position.Y - bounds.Height / 2 );

        return shape;
    }
}