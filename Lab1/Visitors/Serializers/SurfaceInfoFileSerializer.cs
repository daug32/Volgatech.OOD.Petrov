using Lab1.Models;
using Lab1.Models.Implementation;

namespace Lab1.Visitors.Serializers;

public class SurfaceInfoFileSerializer : IVisitor
{
    private readonly string _filePath;

    public SurfaceInfoFileSerializer( string filePath )
    {
        _filePath = filePath;
    }

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

    private void Print( ShapeType shapeType, float perimeter, float area )
    {
        using ( var writer = new StreamWriter( _filePath, true ) )
        {
            string surfaceInfo = SurfaceInfoSerializer.Serialize( 
                shapeType.ToString(), 
                perimeter, 
                area );
            writer.WriteLine( surfaceInfo, true );
        }
    }
}