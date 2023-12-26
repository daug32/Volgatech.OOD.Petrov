using Lab2.Handlers;
using Lab2.Handlers.Selection;
using Lab2.Handlers.States;
using Lab2.Models;
using Lab2.Models.Extensions;
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

    // Handlers
    private readonly DragAndDropHandler _dragAndDropHandler = new();
    private readonly SelectionHandler _selectionHandler = new();

    public Application() : base( new VideoMode( 800, 600 ) )
    {
        _shapesContainer = new ShapesContainer();
        
        // States
        _stateStateHandlerFactory = new StateHandlerFactory( _shapesContainer );
        _stateHandler = _stateStateHandlerFactory.Build( this, State.Default );
        
        // UI
        _toolbar = new Toolbar( ( Vector2f )WindowSize );
        _toolbar.StateSwitched += ( _, state ) => SwitchState( state );
        _shapeMarksBuilder = new ShapeMarksBuilder();
        
        KeyPressed += OnKeyPressed;
        MouseButtonPressed += OnMouseButtonPressed;
        MouseButtonDoublePressed += OnDoubleClick;
        MouseButtonReleased += OnMouseButtonReleased;
    }

    protected override void Draw()
    {
        ClearWindow( Color.White );

        _dragAndDropHandler.Update( _selectionHandler.GetAllSelectedShapes() );

        foreach ( ShapeDecorator shape in _shapesContainer.GetAll() )
        {
            RenderObject( shape );

            _shapeMarksBuilder
                .Build(
                    _selectionHandler.GetSelectionType( shape ),
                    _shapesContainer.HasGroup( shape ),
                    shape.GetGlobalBounds() )
                .ForEach( RenderObject );
        }
        
        RenderObject( _toolbar );
    }

    private void OnKeyPressed( object? sender, KeyEventArgs keyEventArgs )
    {
        if ( CurrentState != State.Default )
        {
            _stateHandler.OnKeyPressed( sender, keyEventArgs );
            return;
        }

        switch ( keyEventArgs.Code )
        {
            case Keyboard.Key.G when Keyboard.IsKeyPressed( Keyboard.Key.LControl ):
            {
                var selectedItems = _selectionHandler.GetAllSelectedShapes();
                _shapesContainer.Group( selectedItems );
                break;
            }
            case Keyboard.Key.U when Keyboard.IsKeyPressed( Keyboard.Key.LControl ):
            {
                var selectedItems = _selectionHandler.GetSelectedShapes( SelectionType.TrueSelection );
                _shapesContainer.Ungroup( selectedItems );
                _selectionHandler.OnUngroup( selectedItems );
                break;
            }
        }
    }

    private void OnMouseButtonPressed( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( CurrentState != State.Default )
        {
            _stateHandler.OnMouseButtonPressed( sender, mouseEventArgs );
            return;
        }
        
        if ( mouseEventArgs.Button == Mouse.Button.Left )
        {
            ShapeDecorator? clickedShape = _shapesContainer.FindByPosition( mouseEventArgs.X, mouseEventArgs.Y );
            var relatedShapes = _shapesContainer.GetRelatedShapes( clickedShape );

            _dragAndDropHandler.OnMousePressed( clickedShape );
            _selectionHandler.OnMousePressed( clickedShape, relatedShapes );
        }
    }

    private void OnMouseButtonReleased( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( _toolbar.OnMouseReleased( sender, mouseEventArgs ) )
        {
            return;
        }
        
        if ( CurrentState != State.Default )
        {
            _stateHandler.OnMouseButtonReleased( sender, mouseEventArgs );
            return;
        }
        
        if ( mouseEventArgs.Button == Mouse.Button.Left )
        {
            _dragAndDropHandler.OnMouseReleased();
        }
    }

    private void OnDoubleClick( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( CurrentState != State.Default )
        {
            _stateHandler.OnDoubleClick( sender, mouseEventArgs );
            return;
        }

        if ( mouseEventArgs.Button == Mouse.Button.Left )
        {
            ShapeDecorator? clickedShape = _shapesContainer.FindByPosition( mouseEventArgs.X, mouseEventArgs.Y );
            var relatedShapes = _shapesContainer.GetRelatedShapes( clickedShape );

            _selectionHandler.OnDoubleClick( clickedShape, relatedShapes );
        }
    }
    
    private void SwitchState( State state )
    {
        _stateHandler = _stateStateHandlerFactory.Build( this, state );
        CurrentState = state;
        _toolbar.SetStateDescription( _stateHandler.GetStateDescription() );
    }
}