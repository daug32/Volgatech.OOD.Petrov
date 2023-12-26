using Lab2.Handlers.States.Implementation.Updates.Visitors.Implementation;
using Lab2.Models;
using Lab2.Models.Extensions;
using Libs.Models;
using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using Libs.SFML.Shapes.Implementation;
using SFML.Graphics;
using SFML.Window;

namespace Lab2.Handlers.States.Implementation.Updates;

internal class ChangeFillColorStateHandler : IStateHandler
{
    private readonly ShapesContainer _shapesContainer;
    
    private static readonly ListIterator<SetFillColorVisitor> _allowedColors = new( new[]
    {
        new SetFillColorVisitor( Color.Blue ),
        new SetFillColorVisitor( Color.Red ),
        new SetFillColorVisitor( Color.Yellow ),
        new SetFillColorVisitor( Color.Black ),
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

    public IShape GetStateDescription()
    {
        return new Circle( 20 )
            .SetFillColor( _allowedColors.GetCurrentValue().FillColor );
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
        clickedShape?.AcceptVisitor( _allowedColors.GetCurrentValue() );
    }

    public void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }
}