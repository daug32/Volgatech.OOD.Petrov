using Lab2.Models;
using Libs.Models;
using Libs.SFML.Shapes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2.States.Handlers.Implementation;

public class AddShapeStateHandler : IIterableStateHandler
{
    private readonly ShapesContainer _shapesContainer;

    private static readonly ListIterator<Func<CashedShape>> _shapesIterator = new( new Func<CashedShape>[]
    {
        () => CashedShape.Create( new CircleShape( 20 ) ),
        () => CashedShape.Create( new RectangleShape( new Vector2f( 20, 20 ) ) ),   
        () => CashedShape.Create( new TriangleShape( new Vector2f( 0, 0 ), new Vector2f( 10, 0 ), new Vector2f( 0, 10 ) ) )
    } );

    public State State { get; } = State.AddShape;

    public AddShapeStateHandler( IStateContext context, ShapesContainer shapesContainer )
    {
        _shapesContainer = shapesContainer;

        if ( context.CurrentState == State )
        {
            _shapesIterator.MoveToNextValue();
        }
    }

    public void MoveToNextValue() => _shapesIterator.MoveToNextValue();

    public Drawable GetStateDescription() => _shapesIterator.GetCurrentValue()();

    public void OnKeyPressed( object? sender, KeyEventArgs eventArgs )
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

        CashedShape shape = _shapesIterator.GetCurrentValue()();
        shape.FillColor = Color.Black;
        shape.OutlineThickness = 0;
        
        FloatRect globalBounds = shape.GetGlobalBounds();
        shape.Position = new Vector2f(
            buttonEventArgs.X - globalBounds.Width / 2, 
            buttonEventArgs.Y - globalBounds.Height / 2 );

        _shapesContainer.Add( shape );
    }

    public void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }
}