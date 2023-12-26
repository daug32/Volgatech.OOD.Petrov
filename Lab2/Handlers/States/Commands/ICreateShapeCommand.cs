using Libs.SFML.Shapes;
using SFML.System;

namespace Lab2.Models.Commands;

public interface ICreateShapeCommand
{
    public Vector2f Position { get; set; }
    
    public CashedShape Execute();
}