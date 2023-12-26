using Libs.SFML.Shapes.Implementation;

namespace Libs.SFML.Shapes;

public interface IShapeVisitor
{
    void Visit( Rectangle shape );
    void Visit( Triangle shape );
    void Visit( Circle shape );
}