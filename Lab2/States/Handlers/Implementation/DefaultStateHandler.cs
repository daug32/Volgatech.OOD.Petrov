using SFML.Window;

namespace Lab2.States.Handlers.Implementation;

public class DefaultStateHandler : IStateHandler
{
    private readonly IStateContext _context;

    public DefaultStateHandler( IStateContext context )
    {
        _context = context;
    }

    public State State { get; } = State.Default;

    public void OnKeyPressed( object? sender, KeyEventArgs eventArgs )
    {
    }

    public void OnMouseButtonPressed( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }

    public void OnMouseButtonReleased( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }

    public void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }
}