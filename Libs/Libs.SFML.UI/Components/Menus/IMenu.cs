using Libs.SFML.UI.Components.Buttons;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Libs.SFML.UI.Components.Menus;

public interface IMenu : Drawable
{
    Color BackgroundColor { get; set; }
    Vector2f Size { get; set; }
    Vector2f Position { get; set; }

    void AddButton( IButton button );
    void AddButtons( IEnumerable<IButton> buttons );

    /// <returns>True if anything was processed</returns>
    bool OnMouseReleased( object? sender, MouseButtonEventArgs args );
}