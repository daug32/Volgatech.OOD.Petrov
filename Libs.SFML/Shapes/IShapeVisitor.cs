using Libs.SFML.Shapes.Implementation;

namespace Libs.SFML.Shapes;

public interface IShapeVisitor<out TResult>
{
    TResult Visit( Rectangle shape );
    TResult Visit( Triangle shape );
    TResult Visit( Circle shape );
}