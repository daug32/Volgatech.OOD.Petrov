using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Implementation;

namespace Lab2.States.Implementation.Updates.Visitors.Implementation;

internal class SetBorderSizeVisitor : IShapeVisitor
{
    public readonly float BorderSize;

    public SetBorderSizeVisitor( float borderSize )
    {
         BorderSize = borderSize;
    }

    public void Visit( Rectangle shape )
    {
        shape.OutlineThickness = BorderSize;
    }

    public void Visit( Triangle shape )
    {
        shape.OutlineThickness = BorderSize;
    }

    public void Visit( Circle shape )
    {
        shape.OutlineThickness = BorderSize;
    }
}