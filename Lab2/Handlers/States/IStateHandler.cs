﻿using Libs.SFML.Shapes;
using SFML.Window;

namespace Lab2.Handlers.States;

public interface IStateHandler
{
    State State { get; }
    
    ShapeDecorator? GetStateDescription();

    void OnKeyPressed( object? sender, KeyEventArgs eventArgs );
    void OnMouseButtonPressed( object? sender, MouseButtonEventArgs buttonEventArgs );
    void OnMouseButtonReleased( object? sender, MouseButtonEventArgs buttonEventArgs );
    void OnDoubleClick( object? sender, MouseButtonEventArgs buttonEventArgs );
}