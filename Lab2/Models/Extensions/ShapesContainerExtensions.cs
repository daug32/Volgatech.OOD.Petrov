using Libs.SFML.Shapes;

namespace Lab2.Models.Extensions;

public static class ShapesContainerExtensions
{
    public static ShapeDecorator? FindByPosition( this ShapesContainer container, float x, float y )
    {
        return container.FirstOrDefault( shape => shape
            .GetGlobalBounds()
            .Contains( x, y ) );
    }
}