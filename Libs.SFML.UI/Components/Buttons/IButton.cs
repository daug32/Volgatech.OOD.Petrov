using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.UI.Components.Buttons;

public interface IButton : Drawable
{
    Vector2f Size { get; set; }
    Vector2f Position { get; set; }
    Color BackgroundColor { get; set; }
    Color BorderColor { get; set; }
    float BorderThickness { get; set; }
    
    void Execute();
    FloatRect GetGlobalBounds();
}