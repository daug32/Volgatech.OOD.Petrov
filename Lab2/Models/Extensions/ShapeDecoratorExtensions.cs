using SFML.Graphics;

namespace Lab2.Models.Extensions;

public static class ShapeDecoratorExtensions
{
    public static ShapeDecorator ToDecorator<TShape>( this TShape shape ) where TShape : Shape
    {
        return new ShapeDecorator( shape );
    }
}