using Libs.SFML.Shapes;
using SFML.Window;

namespace Lab2.Handlers.States;

public interface IStateHandler
{
    State State { get; }
    
    IShape? GetStateDescription();

    void BeforeDraw();
    void OnKeyPressed( object? sender, KeyEventArgs keyEventArgs );
    void OnMouseButtonPressed( object? sender, MouseButtonEventArgs buttonEventArgs );
    void OnMouseButtonReleased( object? sender, MouseButtonEventArgs buttonEventArgs );
    void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs );
}