﻿using SFML.Graphics;

namespace Libs.SFML.Shapes.Implementation;

public class Circle : BaseShape
{
    private CircleShape OriginalShape => ( Shape as CircleShape )!;

    public float Radius
    {
        get => OriginalShape.Radius;
        set
        {
            HasChanges = true;
            OriginalShape.Radius = value;
        }
    }
    
    public Circle( float radius ) : base( new CircleShape( radius ) )
    {
    }

    public override void AcceptVisitor( IShapeVisitor visitor )
    {
        visitor.Visit( this );   
    }
}