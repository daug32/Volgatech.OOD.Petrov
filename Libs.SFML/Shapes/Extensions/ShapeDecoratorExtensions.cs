using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.Shapes.Extensions;

public static class ShapeDecoratorExtensions
{
    public static T SetMaxSize<T>( this T shape, Vector2f maxSize ) where T : ShapeDecorator
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