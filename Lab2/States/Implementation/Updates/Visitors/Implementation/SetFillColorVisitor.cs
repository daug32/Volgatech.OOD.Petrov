using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Implementation;
using SFML.Graphics;

namespace Lab2.States.Implementation.Updates.Visitors.Implementation;

internal class SetFillColorVisitor : IShapeVisitor
{
    public readonly Color FillColor;

    public SetFillColorVisitor( Color fillColor )
    {
        FillColor = fillColor;
    }

    public void Visit( Rectangle shape )
    {
        shape.FillColor = FillColor;
    }

    public void Visit( Triangle shape )
    {
        shape.FillColor = FillColor;
    }

    public void Visit( Circle shape )
    {
        shape.FillColor = FillColor;
    }
}