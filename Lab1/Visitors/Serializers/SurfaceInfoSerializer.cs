namespace Lab1.Visitors.Serializers;

internal static class SurfaceInfoSerializer
{
    public static string Serialize( string shapeType, float perimeter, float area )
    {
        return $"{shapeType.ToUpper()}: P={perimeter}; S={area}";
    }
}