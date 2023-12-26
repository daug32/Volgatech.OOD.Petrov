using Libs.SFML.Shapes;

namespace Lab2.Handlers.States.Implementation.Additions.Commands;

internal interface ICreateShapeCommand
{
    public IShape Execute();
}