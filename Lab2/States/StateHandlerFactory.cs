using Lab2.Handlers.Selection;
using Lab2.Models;
using Lab2.States.Implementation.Additions;
using Lab2.States.Implementation.Defaults;
using Lab2.States.Implementation.Updates;

namespace Lab2.States;

public class StateHandlerFactory
{
    private readonly ShapesContainer _shapesContainer;
    private readonly SelectionHandler _selectionHandler;

    public StateHandlerFactory( ShapesContainer shapesContainer, SelectionHandler selectionHandler)
    {
        _shapesContainer = shapesContainer;
        _selectionHandler = selectionHandler;
    }

    public IStateHandler Build( IStateContext context, State newState )
    {
        return newState switch
        {
            State.Default => new DefaultStateHandler( _shapesContainer, _selectionHandler ),
            State.AddShape => new AddShapeStateHandler( context, _shapesContainer ),
            State.ChangeFillColor => new ChangeFillColorStateHandler( context, _shapesContainer ),
            State.ChangeBorderColor => new ChangeBorderColorStateHandler( context, _shapesContainer ),
            State.ChangeBorderSize => new ChangeBorderSizeStateHandler( context, _shapesContainer ),
            _ => throw new ArgumentOutOfRangeException( nameof( newState ), newState, null )
        };
    }
}