using Lab1.Visitors;
using SFML.Graphics;
using SFML.System;

namespace Lab1.Models.Implementation;

public class Rectangle : RectangleShape, IShape, ISurfaceCalculable
{
    public ShapeType ShapeType => ShapeType.Rectangle;

    public Rectangle( Vector2f leftTop, Vector2f rightBottom )
        : base( new Vector2f(
            Math.Abs( rightBottom.X - leftTop.X ),
            Math.Abs( rightBottom.Y - leftTop.Y ) ) )
    {
        Position = leftTop;
    }

    public float GetArea()
    {
        return Size.X * Size.Y;
    }

    public float GetPerimeter()
    {
        return 2 * ( Size.X + Size.Y );
    }

    public void ApplyVisitor( IVisitor visitor )
    {
        visitor.Visit( this );
    }
}