using Libs.SFML.Shapes;

namespace Lab2.States.Implementation.Additions.Commands;

internal interface ICreateShapeCommand
{
    public IShape Execute();
}