using Lab1.Visitors;
using Libs.SFML.Vertices;
using SFML.Graphics;
using SFML.System;

namespace Lab1.Models.Implementation;

public class Triangle : ConvexShape, IShape, ISurfaceCalculable
{
    public ShapeType ShapeType => ShapeType.Triangle;
    
    public IReadOnlyList<Vector2f> Points { get; }

    public Triangle( Vector2f p0, Vector2f p1, Vector2f p2 ) : base( 3 )
    {
        Points = new List<Vector2f> { p0, p1, p2 };
    }

    public float GetArea()
    {
        return 0.5f * (
           Points[0].X * ( Points[1].Y - Points[2].Y )
           + Points[1].X * ( Points[2].Y - Points[0].Y )
           + Points[2].X * ( Points[0].Y - Points[1].Y ) );
    }

    public float GetPerimeter()
    {
        float ab = MathF.Sqrt( Points[0].GetSquareDistance( Points[1] ) );
        float bc = MathF.Sqrt( Points[1].GetSquareDistance( Points[2] ) );
        float ac = MathF.Sqrt( Points[2].GetSquareDistance( Points[0] ) );
        return ab + bc + ac;
    }

    public void ApplyVisitor( IVisitor visitor )
    {
        visitor.Visit( this );
    }
}