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

    public IStateHandler Build( 
        State currentState = State.Default,
        State newState = State.Default )
    {
        return newState switch
        {
            State.Default => new DefaultStateHandler(),
            State.AddShape => new AddShapeStateHandler( currentState, _shapesContainer ),
            _ => throw new ArgumentOutOfRangeException( nameof( newState ), newState, null )
        };
    }
}