using Lab2.Handlers.Selection;
using Lab2.Handlers.States.Implementation;
using Lab2.Models;

namespace Lab2.Handlers.States;

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
            _ => throw new ArgumentOutOfRangeException( nameof( newState ), newState, null )
        };
    }
}