using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using Libs.SFML.Shapes.Implementation;
using SFML.System;

namespace Lab2.Handlers.States.Commands.Implementation;

public class CreateTriangleShapeCommand : ICreateShapeCommand
{ 
    public Vector2f P0;
    public Vector2f P1;
    public Vector2f P2;
    public Vector2f Position;

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

    public IShape Execute()
    {
        return new Triangle( P0, P1, P2 )
            .SetPosition( Position );
    }
}