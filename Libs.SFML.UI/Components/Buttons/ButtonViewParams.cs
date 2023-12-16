using SFML.Graphics;

namespace Libs.SFML.UI.Components.Buttons;

public class ButtonViewParams
{
    public Color BackgroundColor { get; set; } = Color.Black;
    public Color BorderColor { get; set; } = Color.Transparent;
    public float BorderThickness { get; set; } = 0;
}