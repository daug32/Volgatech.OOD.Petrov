using Lab2.Handlers;
using Lab2.Handlers.Selection;
using Lab2.Models;
using Lab2.Models.Extensions;
using Lab2.States;
using Lab2.States.Handlers;
using Lab2.UI;
using Libs.SFML.Applications;
using Libs.SFML.Shapes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2;

public class Application : BaseApplication
{
    private readonly Toolbar _toolbar;
    private readonly ShapesContainer _shapesContainer;
    private IStateHandler _stateHandler;
    private readonly StateHandlerFactory _stateHandlerFactory;

    private readonly DragAndDropHandler _dragAndDropHandler = new();
    private readonly SelectionHandler _selectionHandler = new();

    public Application() : base( new VideoMode( 800, 600 ) )
    {
        _shapesContainer = new ShapesContainer();
        _stateHandlerFactory = new StateHandlerFactory( _shapesContainer );
        
        _toolbar = new Toolbar( ( Vector2f )WindowSize );
        _toolbar.StateSwitched += SwitchState;
        MouseButtonReleased += _toolbar.OnMouseReleased;
        
        KeyPressed += OnKeyPressed;
        MouseButtonPressed += OnMouseButtonPressed;
        MouseButtonDoublePressed += OnDoubleClick;
        MouseButtonReleased += OnMouseButtonReleased;
    }

    protected override void Draw()
    {
        ClearWindow( Color.White );
        
        RenderObject( _toolbar );

        _dragAndDropHandler.Update( _selectionHandler.GetAllSelectedShapes() );

        foreach ( CashedShape shape in _shapesContainer.GetAll() )
        {
            RenderObject( shape );

            ShapeMarksBuilder
                .Build(
                    _selectionHandler.GetSelectionType( shape ),
                    _shapesContainer.HasGroup( shape ),
                    shape.GetGlobalBounds() )
                .ForEach( RenderObject );
        }
    }

    private void OnKeyPressed( object? sender, KeyEventArgs keyEventArgs )
    {
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
        if ( mouseEventArgs.Button == Mouse.Button.Left )
        {
            CashedShape? clickedShape = _shapesContainer.FindByPosition( mouseEventArgs.X, mouseEventArgs.Y );
            var relatedShapes = GetRelatedShapes( clickedShape );

            _dragAndDropHandler.OnMousePressed( clickedShape );
            _selectionHandler.OnMousePressed( clickedShape, relatedShapes );
        }
    }

    private void OnMouseButtonReleased( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( mouseEventArgs.Button == Mouse.Button.Left )
        {
            _dragAndDropHandler.OnMouseReleased();
        }
    }

    private void OnDoubleClick( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( mouseEventArgs.Button == Mouse.Button.Left )
        {
            CashedShape? clickedShape = _shapesContainer.FindByPosition( mouseEventArgs.X, mouseEventArgs.Y );
            var relatedShapes = GetRelatedShapes( clickedShape );

            _selectionHandler.OnDoubleClick( clickedShape, relatedShapes );
        }
    }

    private List<CashedShape> GetRelatedShapes( CashedShape? clickedShape )
    {
        return clickedShape != null
            ? _shapesContainer.GetRelatedShapes( clickedShape )
            : new List<CashedShape>();
    }

    private void SwitchState( object? sender, State state )
    {
        _stateHandler = _stateHandlerFactory.Build( state );
    }
}