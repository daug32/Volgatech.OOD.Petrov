using Lab2.Handlers.States.Implementation;
using Lab2.Models;

namespace Lab2.Handlers.States;

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
            State.Default => new DefaultStateHandler(),
            State.AddShape => new AddShapeStateHandler( context, _shapesContainer ),
            _ => throw new ArgumentOutOfRangeException( nameof( newState ), newState, null )
        };
    }
}