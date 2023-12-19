using Lab2.Models;
using Libs.Models;
using Libs.SFML.Shapes;
using SFML.Graphics;
using SFML.Window;

namespace Lab2.States.Handlers.Implementation;

public class AddShapeStateHandler : IStateHandler
{
    private readonly ShapesContainer _shapesContainer;

    private static readonly ListIterator<CashedShape> _shapesIterator = new( new[]
    {
        CashedShape.Create( new CircleShape() ),   
        CashedShape.Create( new RectangleShape() ),    
        CashedShape.Create( new TriangleShape() )  
    } );

    public State State { get; } = State.AddShape;

    public AddShapeStateHandler( ShapesContainer shapesContainer )
    {
        _shapesContainer = shapesContainer;
    }

    public void OnKeyPressed( object? sender, KeyEventArgs eventArgs )
    {
    }

    public void OnMousePressed( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }

    public void OnMouseReleased( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
        if ( buttonEventArgs.Button != Mouse.Button.Left )
        {
            return;
        }

        _shapesContainer.Add( _shapesIterator.GetCurrentValue() );
    }

    public void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }
}