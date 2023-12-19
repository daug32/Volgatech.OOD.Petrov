using SFML.Window;

namespace Lab2.States.Handlers;

public interface IStateHandler
{
    State State { get; }
    void OnKeyPressed( object? sender, KeyEventArgs eventArgs );
    void OnMouseButtonPressed( object? sender, MouseButtonEventArgs buttonEventArgs );
    void OnMouseButtonReleased( object? sender, MouseButtonEventArgs buttonEventArgs );
    void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs );
}