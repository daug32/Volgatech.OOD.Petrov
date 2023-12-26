using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using Libs.SFML.Shapes.Implementation;
using SFML.System;

namespace Lab2.Handlers.States.Implementation.Additions.Commands.Implementation;

internal class CreateRectangleShapeCommand : ICreateShapeCommand
{
    public Vector2f Size;
    public Vector2f Position;

    public CreateRectangleShapeCommand( 
        Vector2f size, 
        Vector2f? position = null )
    {
        Size = size;
        Position = position ?? new Vector2f();
    }

    public IShape Execute()
    {
        return new Rectangle( Size )
            .SetPosition( Position );
    }
}