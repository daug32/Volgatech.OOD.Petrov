using Lab1.Models;
using Lab1.Models.Implementation;

namespace Lab1.Visitors.Serializers;

public class SurfaceInfoConsoleSerializer : IVisitor
{
    public void Visit( Circle shape )
    {
        Print( shape.ShapeType, shape.GetPerimeter(), shape.GetArea() );
    }

    public void Visit( Rectangle shape )
    {
        Print( shape.ShapeType, shape.GetPerimeter(), shape.GetArea() );
    }

    public void Visit( Triangle shape )
    {
        Print( shape.ShapeType, shape.GetPerimeter(), shape.GetArea() );
    }

    private static void Print( ShapeType shapeType, float perimeter, float area )
    {
        string surfaceInfo = SurfaceInfoSerializer.Serialize( 
            shapeType.ToString(), 
            perimeter, 
            area );
        Console.WriteLine( surfaceInfo );
    }
}