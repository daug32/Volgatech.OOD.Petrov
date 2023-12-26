using Libs.SFML.Shapes;
using SFML.System;

namespace Lab2.Handlers.States.Commands;

public interface ICreateShapeCommand
{
    public Vector2f Position { get; set; }
    
    public ShapeDecorator Execute();
}