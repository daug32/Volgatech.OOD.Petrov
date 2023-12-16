﻿using Libs.SFML.UI.Components.Buttons;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Libs.SFML.UI;

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

    public void AddButton( IButton button )
    {
        _buttons.Add( button );
    }

    public void OnMouseReleased( object? sender, MouseButtonEventArgs args )
    {
        if ( args.Button != Mouse.Button.Left )
        {
            return;
        }
        
        IButton? button = GetClicked( args );
        if ( button is null )
        {
            return;
        }
        
        button.Execute();
    }

    public void Draw( RenderTarget target, RenderStates states )
    {
        _background.Draw( target, states );
        foreach ( IButton button in _buttons )
        {
            button.Draw( target, states );
        }
    }

    private IButton? GetClicked( MouseButtonEventArgs args ) => _buttons.LastOrDefault(
        button => button
            .GetGlobalBounds()
            .Contains( args.X, args.Y ) );
}