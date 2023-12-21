using SFML.Graphics;
using SFML.Window;

namespace Lab2.Handlers.States.Implementation;

public class DefaultStateHandler : IStateHandler
{
    public State State { get; } = State.Default;

    public void MoveToNextValue()
    {
    }

    public Drawable? GetStateDescription()
    {
        return null;
    }

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