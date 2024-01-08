using SFML.Graphics;

namespace Libs.SFML.Colors;

public static class ColorExtensions
{
    public static Color SetAlpha( this Color color, byte alpha )
    {
        return new Color( color.R, color.G, color.B, alpha );
    } 
}