using Lab1.Visitors;

namespace Lab1.Models;

public interface IShape
{
    public ShapeType ShapeType { get; }
    public void ApplyVisitor( IVisitor visitor );
}