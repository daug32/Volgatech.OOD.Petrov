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

    public IStateHandler Build( State state ) => state switch
    {
        State.Default => new DefaultStateHandler(),
        State.AddShape => new AddShapeStateHandler( _shapesContainer ),
        _ => throw new ArgumentOutOfRangeException( nameof( state ), state, null )
    };
}