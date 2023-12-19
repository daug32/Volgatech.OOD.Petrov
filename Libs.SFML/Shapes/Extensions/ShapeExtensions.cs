using SFML.Graphics;

namespace Libs.SFML.Shapes.Extensions;

public static class ShapeExtensions
{
    public static RectangleShape Copy( this RectangleShape shape )
    {
        return new RectangleShape( shape );
    }

    public static CircleShape Copy( this CircleShape shape )
    {
        return new CircleShape( shape );
    }

    public static ConvexShape Copy( this ConvexShape shape )
    {
        return new ConvexShape( shape );
    }
}