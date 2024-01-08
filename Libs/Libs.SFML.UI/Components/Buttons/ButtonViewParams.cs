using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.UI.Components.Buttons;

public class ButtonViewParams
{
    public Vector2f Size { get; set; } = new();
    public Vector2f Position { get; set; } = new();
    public Color BackgroundColor { get; set; } = Color.Black;
    public Color BorderColor { get; set; } = Color.Transparent;
    public float BorderThickness { get; set; } = 0;
}