using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using SFML.System;

namespace Lab2.Models.Commands.Implementation;

public class CreateTriangleShapeCommand : ICreateShapeCommand
{ 
    public Vector2f P0;
    public Vector2f P1;
    public Vector2f P2;
    
    public Vector2f Position { get; set; }

    public CreateTriangleShapeCommand(
        Vector2f p0,
        Vector2f p1,
        Vector2f p2,
        Vector2f? position = null )
    {
        P0 = p0;
        P1 = p1;
        P2 = p2;

        Position = position ?? new Vector2f();
    }

    public ShapeDecorator Execute()
    {
        return new ShapeDecorator( new TriangleShape( P0, P1, P2 ) )
            .SetPosition( Position );
    }
}