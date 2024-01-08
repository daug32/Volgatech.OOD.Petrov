using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.Shapes.Implementation;

public class Triangle : BaseShape
{
    private class TriangleShape : ConvexShape
    {
        public TriangleShape() : this( new Vector2f( 0, 0 ), new Vector2f( 1, 0 ), new Vector2f( 0, 1 ) )
        {
        }

        public TriangleShape( Vector2f p0, Vector2f p1, Vector2f p2 ) : base( 3 )
        {
            SetPoint( 0, p0 );
            SetPoint( 1, p1 );
            SetPoint( 2, p2 );
        }
    }

    private TriangleShape OriginalShape => ( Shape as TriangleShape )!;

    public Triangle( Vector2f p0, Vector2f p1, Vector2f p2 ) : base( new TriangleShape( p0, p1, p2 ) )
    {
    } 

    public override void AcceptVisitor( IShapeVisitor visitor )
    {
        visitor.Visit( this );
    }
}