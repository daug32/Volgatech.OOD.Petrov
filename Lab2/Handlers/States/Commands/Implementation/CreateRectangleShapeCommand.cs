using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Lab2.Models.Commands.Implementation;

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

    public CashedShape Execute()
    {
        return new CashedShape( new RectangleShape( Size ) )
            .FluentSetPosition( Position );
    }
}