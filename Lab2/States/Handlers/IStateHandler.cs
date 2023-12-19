using SFML.Window;

namespace Lab2.States.Handlers;

public interface IStateHandler
{
    State State { get; }
    void OnKeyPressed( object? sender, KeyEventArgs eventArgs );
    void OnMousePressed( object? sender, MouseButtonEventArgs buttonEventArgs );
    void OnMouseReleased( object? sender, MouseButtonEventArgs buttonEventArgs );
    void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs );
}