using Lab1.Visitors;
using SFML.Graphics;
using SFML.System;

namespace Lab1.Models.Implementation;

public class Circle : CircleShape, IShape, ISurfaceCalculable
{
    public ShapeType ShapeType => ShapeType.Circle;
    
    public Circle( Vector2f center, float radius )
        : base( radius )
    {
        Position = center;
    }

    public float GetArea()
    {
        return MathF.PI * Radius * Radius;
    }

    public float GetPerimeter()
    {
        return 2 * MathF.PI * Radius;
    }

    public void ApplyVisitor( IVisitor visitor )
    {
        visitor.Visit( this );
    }
}