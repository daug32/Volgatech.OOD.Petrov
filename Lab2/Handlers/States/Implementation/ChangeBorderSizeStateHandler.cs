using Lab2.Models;
using Lab2.Models.Extensions;
using Libs.Models;
using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using Libs.SFML.Shapes.Implementation;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2.Handlers.States.Implementation;

public class ChangeBorderSizeStateHandler : IStateHandler
{
    private readonly ShapesContainer _shapesContainer;
    
    private static readonly ListIterator<float> _allowedBorderSizes = new( new float[]
    {
        1,
        2,
        3,
        0
    } );

    public State State => State.ChangeBorderSize;

    public ChangeBorderSizeStateHandler( IStateContext context, ShapesContainer shapesContainer )
    {
        _shapesContainer = shapesContainer;

        if ( context.CurrentState == State )
        {
            _allowedBorderSizes.MoveToNextValue();
        }
    }

    public IShape GetStateDescription()
    {
        var size = new Vector2f( 20, _allowedBorderSizes.GetCurrentValue() * 2 );
        return new Rectangle( size )
            .SetFillColor( Color.Black );
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
        
        IShape? clickedShape = _shapesContainer.FindByPosition(
            buttonEventArgs.X,
            buttonEventArgs.Y );

        if ( clickedShape is null )
        {
            return;
        }
        
        clickedShape.SetOutlineThickness( _allowedBorderSizes.GetCurrentValue() );
        if ( clickedShape.OutlineColor == Color.Transparent )
        {
            clickedShape.OutlineColor = Color.Black;
        }
    }

    public void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }
}