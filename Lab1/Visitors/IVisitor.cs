using Lab1.Models.Implementation;

namespace Lab1.Visitors;

public interface IVisitor
{
    public void Visit( Circle shape );
    public void Visit( Rectangle shape );
    public void Visit( Triangle shape );
}