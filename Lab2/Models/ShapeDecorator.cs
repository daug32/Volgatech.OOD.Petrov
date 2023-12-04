using SFML.Graphics;
using SFML.System;

namespace Lab2.Models;

public class ShapeDecorator : Shape
{
    private readonly Shape _shape;

    public ShapeDecorator( Shape shape )
    {
        _shape = shape;
    }

    public override uint GetPointCount() => _shape.GetPointCount();

    public override Vector2f GetPoint( uint index ) => _shape.GetPoint( index );
}