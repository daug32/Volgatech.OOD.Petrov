using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Implementation;
using SFML.Graphics;

namespace Lab2.Handlers.States.Implementation.Updates.Visitors.Implementation;

internal class SetBorderColorVisitor : IShapeVisitor
{
    public readonly Color BorderColor;

    public SetBorderColorVisitor( Color borderColor )
    {
        BorderColor = borderColor;
    }

    public void Visit( Rectangle shape )
    {
        shape.OutlineColor = BorderColor;
    }

    public void Visit( Triangle shape )
    {
        shape.OutlineColor = BorderColor;
    }

    public void Visit( Circle shape )
    {
        shape.OutlineColor = BorderColor;
    }
}