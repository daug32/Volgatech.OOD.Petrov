using Libs.SFML.UI.Components.Buttons;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Libs.SFML.UI.Components.Menus;

public class Menu : IMenu
{
    private readonly HashSet<IButton> _buttons = new();
    private readonly RectangleShape _background;

    public Color BackgroundColor
    {
        get => _background.FillColor;
        set => _background.FillColor = value;
    }

    public Vector2f Size
    {
        get => _background.Size;
        set => _background.Size = value;
    }

    public Vector2f Position
    {
        get => _background.Position;
        set => _background.Position = value;
    }

    public Menu( Vector2f size )
    {
        _background = new RectangleShape( size );
    }

    public void AddButtons( IEnumerable<IButton> buttons )
    {
        foreach ( IButton button in buttons )
        {
            _buttons.Add( button );
        }
    }

    public void AddButton( IButton button )
    {
        _buttons.Add( button );
    }

    public bool OnMouseReleased( object? sender, MouseButtonEventArgs args )
    {
        if ( args.Button != Mouse.Button.Left )
        {
            return false;
        } 
        
        IButton? button = GetClicked( args.X, args.Y );
        if ( button is null )
        {
            return false;
        }
        
        button.Execute();
        return true;
    }

    public void Draw( RenderTarget target, RenderStates states )
    {
        _background.Draw( target, states );
        foreach ( IButton button in _buttons )
        {
            button.Draw( target, states );
        }
    }

    private IButton? GetClicked( float x, float y )
    {
        return _buttons.LastOrDefault( button => button
            .GetGlobalBounds()
            .Contains( x, y ) );
    }
}