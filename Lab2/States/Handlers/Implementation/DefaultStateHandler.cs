using SFML.Window;

namespace Lab2.States.Handlers.Implementation;

public class DefaultStateHandler : IStateHandler
{
    public State State { get; } = State.Default;

    public void OnKeyPressed( object? sender, KeyEventArgs eventArgs )
    {
    }

    public void OnMousePressed( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }

    public void OnMouseReleased( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }

    public void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs )
    {
    }
}