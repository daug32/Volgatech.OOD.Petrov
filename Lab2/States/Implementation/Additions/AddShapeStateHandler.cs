using Lab2.Models;
using Lab2.States.Implementation.Additions.Commands;
using Lab2.States.Implementation.Additions.Commands.Implementation;
using Libs.Linq.Models;
using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2.States.Implementation.Additions;

internal class AddShapeStateHandler : IStateHandler
{
    private readonly ShapesContainer _shapesContainer;

    private static readonly ListIterator<ICreateShapeCommand> _shapesIterator = new( new ICreateShapeCommand[]
    {
        new CreateCircleShapeCommand( 20 ),
        new CreateRectangleShapeCommand( new Vector2f( 20, 20 ) ),
        new CreateTriangleShapeCommand( new Vector2f( 0, 0 ), new Vector2f( 20, 0 ), new Vector2f( 0, 20 ) ),
    } );

    public State State => State.AddShape;

    public AddShapeStateHandler( IStateContext context, ShapesContainer shapesContainer )
    {
        _shapesContainer = shapesContainer;

        if ( context.CurrentState == State )
        {
            _shapesIterator.MoveToNextValue();
        }
    }

    public IShape GetStateDescription()
    {
        return GetCurrentShape();
    }

    public void BeforeDraw()
    {
    }

    public void OnKeyPressed( object? sender, KeyEventArgs keyEventArgs )
    {
    }

    public void OnMouseButtonPressed( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }

    public void OnMouseButtonReleased( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
        if ( buttonEventArgs.Button != Mouse.Button.Left )
        {
            return;
        }

        _shapesContainer.Add( GetCurrentShape()
            .SetCenterPosition( buttonEventArgs.X, buttonEventArgs.Y ) );
    }

    public void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }

    private static IShape GetCurrentShape()
    {
        IShape shape = _shapesIterator.GetCurrentValue().Execute();
        return shape
            .SetFillColor( Color.Black )
            .SetOutlineThickness( 0 );
    }
}