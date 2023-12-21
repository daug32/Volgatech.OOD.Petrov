using Lab2.Models;
using Lab2.States.Handlers.Implementation;

namespace Lab2.States.Handlers;

public class StateHandlerFactory
{
    private readonly ShapesContainer _shapesContainer;

    public StateHandlerFactory( ShapesContainer shapesContainer )
    {
        _shapesContainer = shapesContainer;
    }

    public IStateHandler Build( IStateContext context, State newState )
    {
        return newState switch
        {
            State.Default => new DefaultStateHandler( context ),
            State.AddShape => new AddShapeStateHandler( context, _shapesContainer ),
            _ => throw new ArgumentOutOfRangeException( nameof( newState ), newState, null )
        };
    }
}