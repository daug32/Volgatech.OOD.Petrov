using Libs.SFML.Shapes;

namespace Lab2.Handlers.States.Commands;

public interface ICreateShapeCommand
{
    public IShape Execute();
}