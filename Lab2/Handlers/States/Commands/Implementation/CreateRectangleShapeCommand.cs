using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Lab2.Handlers.States.Commands.Implementation;

public class CreateRectangleShapeCommand : ICreateShapeCommand
{
    public Vector2f Size;
    public Vector2f Position { get; set; }

    public CreateRectangleShapeCommand( 
        Vector2f size, 
        Vector2f? position = null )
    {
        Size = size;
        Position = position ?? new Vector2f();
    }

    public ShapeDecorator Execute()
    {
        return new ShapeDecorator( new RectangleShape( Size ) )
            .SetPosition( Position );
    }
}