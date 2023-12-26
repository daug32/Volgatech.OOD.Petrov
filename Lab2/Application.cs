using Lab2.Handlers.Selection;
using Lab2.Handlers.States;
using Lab2.Models;
using Lab2.UI;
using Libs.SFML.Applications;
using Libs.SFML.Shapes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2;

public class Application : BaseApplication, IStateContext
{
    // Data holder
    private readonly ShapesContainer _shapesContainer;

    // UI components
    private readonly Toolbar _toolbar;
    private readonly ShapeMarksBuilder _shapeMarksBuilder;
    
    // States
    public State CurrentState { get; private set; } = State.Default;
    private readonly StateHandlerFactory _stateStateHandlerFactory;
    private IStateHandler _stateHandler;

    public Application() : base( new VideoMode( 800, 600 ) )
    {
        var selectionHandler = new SelectionHandler();
        
        // Data holder
        _shapesContainer = new ShapesContainer();
        
        // UI
        _toolbar = new Toolbar( ( Vector2f )WindowSize );
        _toolbar.StateSwitched += ( _, state ) => SwitchState( state );
        _shapeMarksBuilder = new ShapeMarksBuilder( _shapesContainer, selectionHandler );
        
        // States
        _stateStateHandlerFactory = new StateHandlerFactory( 
            _shapesContainer, 
            selectionHandler );
        _stateHandler = _stateStateHandlerFactory.Build( this, State.Default );
        
        // Events
        KeyPressed += OnKeyPressed;
        MouseButtonPressed += OnMouseButtonPressed;
        MouseButtonDoublePressed += OnDoubleClick;
        MouseButtonReleased += OnMouseButtonReleased;
    }

    protected override void Draw()
    {
        _stateHandler.BeforeDraw();

        ClearWindow( Color.White );

        foreach ( IShape shape in _shapesContainer.GetAll() )
        {
            RenderObject( shape );

            foreach ( Drawable mark in _shapeMarksBuilder.GetMarks( shape ) )
            {
                RenderObject( mark );
            }
        }
        
        RenderObject( _toolbar );
    }

    private void OnKeyPressed( object? sender, KeyEventArgs keyEventArgs )
    {
        _stateHandler.OnKeyPressed( sender, keyEventArgs );
    }

    private void OnMouseButtonPressed( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        _stateHandler.OnMouseButtonPressed( sender, mouseEventArgs );
    }

    private void OnMouseButtonReleased( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( _toolbar.OnMouseReleased( sender, mouseEventArgs ) )
        {
            return;
        }
        
        _stateHandler.OnMouseButtonReleased( sender, mouseEventArgs );
    }

    private void OnDoubleClick( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        _stateHandler.OnDoubleClick( sender, mouseEventArgs );
    }
    
    private void SwitchState( State state )
    {
        _stateHandler = _stateStateHandlerFactory.Build( this, state );
        CurrentState = state;
        _toolbar.SetStateDescription( _stateHandler.GetStateDescription() );
    }
}