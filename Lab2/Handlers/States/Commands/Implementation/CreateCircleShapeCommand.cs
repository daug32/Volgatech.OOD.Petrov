using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Lab2.Handlers.States.Commands.Implementation;

public class CreateCircleShapeCommand : ICreateShapeCommand
{
    public float Radius;

    public Vector2f Position { get; set; }

    public CreateCircleShapeCommand( float radius, Vector2f? position = null )
    {
        Radius = radius;
        Position = position ?? new Vector2f();
    }
    
    public ShapeDecorator Execute()
    {
        return new ShapeDecorator( new CircleShape( Radius ) )
            .SetPosition( Position );
    }
}