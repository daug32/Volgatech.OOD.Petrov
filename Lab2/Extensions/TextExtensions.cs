using SFML.Graphics;

namespace Lab2.Extensions;

public static class TextExtensions
{
    public static T FluentSetCharacterSize<T>( this T shape, uint characterSize ) where T : Text
    {
        shape.CharacterSize = characterSize;
        return shape;
    }
}