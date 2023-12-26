using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.UI.Components.Buttons;

public class TextButtonViewParams : ButtonViewParams
{
    public Color Color { get; set; } = Color.White;
    public Vector2f Padding { get; set; } = new( 5, 5 );
    public int? MinHeight { get; set; }
}