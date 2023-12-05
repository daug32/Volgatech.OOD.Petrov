using SFML.Graphics;

namespace Lab2.Utils;

public static class ColorUtils
{
    public static Color GetRandom()
    {
        var randomizer = new Random();
        return new Color( 
            ( byte )randomizer.Next( 0, 256 ),
            ( byte )randomizer.Next( 0, 256 ),
            ( byte )randomizer.Next( 0, 256 ) );
    }
}