using Libs.SFML.Applications.Implementation;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Libs.SFML.Applications;

public abstract class BaseApplication
{
    private readonly RenderWindow _window;
    private readonly DoubleClickDetector _doubleClickDetector = new();

    protected Vector2u WindowSize => _window.Size;

    protected event EventHandler<KeyEventArgs>? KeyPressed;
    protected event EventHandler<MouseButtonEventArgs>? MouseButtonDoublePressed;
    protected event EventHandler<MouseButtonEventArgs>? MouseButtonPressed;
    protected event EventHandler<MouseButtonEventArgs>? MouseButtonReleased;

    protected BaseApplication(
        VideoMode videoMode,
        string windowTitle = "SFML application",
        uint maxFps = 60 )
    {
        _window = new RenderWindow( videoMode, windowTitle );
        _window.SetFramerateLimit( maxFps );

        _window.Closed += OnClosed;

        _window.KeyPressed += OnKeyPressed;
        _window.MouseButtonPressed += OnMouseButtonPressed;
        _window.MouseButtonReleased += OnMouseButtonReleased;
    }

    public void Start()
    {
        _window.SetVisible( true );

        while ( _window.IsOpen )
        {
            _window.DispatchEvents();
            Draw();
            _window.Display();
        }
    }

    protected abstract void Draw();

    protected void RenderObject( Drawable drawable )
    {
        _window.Draw( drawable );
    }

    protected void ClearWindow( Color color )
    {
        _window.Clear( color );
    }

    private void OnClosed( object? sender, EventArgs e )
    {
        _window.SetVisible( false );
        _window.Close();
    }

    private void OnKeyPressed( object? sender, KeyEventArgs e )
    {
        KeyPressed?.Invoke( sender, e );
    }

    private void OnMouseButtonReleased( object? sender, MouseButtonEventArgs e )
    {
        MouseButtonReleased?.Invoke( sender, e );
    }

    private void OnMouseButtonPressed( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( _doubleClickDetector.IsDoubleClick( mouseEventArgs ) )
        {
            MouseButtonDoublePressed?.Invoke( sender, mouseEventArgs );
            return;
        }

        MouseButtonPressed?.Invoke( sender, mouseEventArgs );
    }
}