using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using Libs.SFML.Shapes.Implementation;
using SFML.System;

namespace Lab2.States.Implementation.Additions.Commands.Implementation;

internal class CreateCircleShapeCommand : ICreateShapeCommand
{
    public float Radius;
    public Vector2f Position;

    public CreateCircleShapeCommand( float radius, Vector2f? position = null )
    {
        Radius = radius;
        Position = position ?? new Vector2f();
    }
    
    public IShape Execute()
    {
        return new Circle( Radius )
            .SetPosition( Position );
    }
}