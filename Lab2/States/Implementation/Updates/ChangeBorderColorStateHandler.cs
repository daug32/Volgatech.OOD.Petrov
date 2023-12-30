using Lab2.Models;
using Lab2.Models.Extensions;
using Lab2.States.Implementation.Updates.Visitors.Implementation;
using Libs.Linq.Models;
using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using Libs.SFML.Shapes.Implementation;
using SFML.Graphics;
using SFML.Window;

namespace Lab2.States.Implementation.Updates;

internal class ChangeBorderColorStateHandler : IStateHandler
{
    private readonly ShapesContainer _shapesContainer;
    
    private static readonly ListIterator<SetBorderColorVisitor> _allowedColors = new( new[]
    {
        new SetBorderColorVisitor( Color.Blue ),
        new SetBorderColorVisitor( Color.Red ),
        new SetBorderColorVisitor( Color.Yellow ),
        new SetBorderColorVisitor( Color.Black ),
    } );

    public State State => State.ChangeBorderColor;

    public ChangeBorderColorStateHandler( IStateContext context, ShapesContainer shapesContainer )
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
            .SetOutlineThickness( 10 )
            .SetOutlineColor( _allowedColors.GetCurrentValue().BorderColor )
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

        clickedShape.AcceptVisitor( _allowedColors.GetCurrentValue() );     
        if ( clickedShape.OutlineThickness == 0 )
        {
            clickedShape.OutlineThickness = 1;
        }
    }

    public void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }
}