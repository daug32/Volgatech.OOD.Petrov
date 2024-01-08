using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.Shapes.Implementation;

public class Rectangle : BaseShape
{
    private RectangleShape OriginalShape => ( Shape as RectangleShape )!;
    
    public Rectangle( Vector2f size ) : base( new RectangleShape( size ) )
    {
    }

    public Vector2f Size
    {
        get => OriginalShape.Size;
        set
        {
            HasChanges = true;
            OriginalShape.Size = value;
        }
    }

    public override void AcceptVisitor( IShapeVisitor visitor )
    {
        visitor.Visit( this );
    }
}