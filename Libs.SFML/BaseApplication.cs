using SFML.Graphics;
using SFML.Window;

namespace Libs.SFML;

public abstract class BaseApplication
{
    protected readonly RenderWindow Window;

    protected BaseApplication(
        VideoMode videoMode,
        string windowTitle = "SFML window" )
    {
        Window = new RenderWindow( videoMode, windowTitle );
        Window.Closed += OnClosed;
    }

    public void Start()
    {
        Window.SetVisible( true );
        
        while ( Window.IsOpen )
        {
            Window.DispatchEvents();
            Draw();
            Window.Display();
        }
    }

    protected abstract void Draw();

    private void OnClosed( object? sender, EventArgs e )
    {
        Window.SetVisible( false );
        Window.Close();
    }
}