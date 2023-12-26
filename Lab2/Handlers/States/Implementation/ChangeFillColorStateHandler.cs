using Lab2.Models;
using Lab2.Models.Extensions;
using Libs.Models;
using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using SFML.Graphics;
using SFML.Window;

namespace Lab2.Handlers.States.Implementation;

public class ChangeFillColorStateHandler : IStateHandler
{
    private readonly ShapesContainer _shapesContainer;
    
    private static readonly ListIterator<Color> _allowedColors = new( new[]
    {
        Color.Blue,
        Color.Red,
        Color.Yellow,
        Color.Black,
    } );

    public State State => State.ChangeFillColor;

    public ChangeFillColorStateHandler( IStateContext context, ShapesContainer shapesContainer )
    {
        _shapesContainer = shapesContainer;

        if ( context.CurrentState == State )
        {
            _allowedColors.MoveToNextValue();
        }
    }

    public ShapeDecorator GetStateDescription()
    {
        return new ShapeDecorator( new CircleShape( 20 ) )
            .SetFillColor( _allowedColors.GetCurrentValue() );
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
        
        ShapeDecorator? clickedShape = _shapesContainer.FindByPosition(
            buttonEventArgs.X,
            buttonEventArgs.Y );
        clickedShape?.SetFillColor( _allowedColors.GetCurrentValue() );
    }

    public void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }
}