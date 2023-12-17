using Lab2.Data;
using Lab2.Handlers;
using Lab2.Handlers.Grouping;
using Lab2.Handlers.Selection;
using Lab2.UI;
using Libs.SFML.Applications;
using Libs.SFML.Shapes;
using Libs.SFML.UI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2;

public class Application : BaseApplication
{
    private readonly ShapesRepository _shapesRepository = new();

    private readonly Menu _toolbar;

    private readonly DragAndDropHandler _dragAndDropHandler = new();
    private readonly GroupingHandler _groupingHandler = new();
    private readonly SelectionHandler _selectionHandler = new();

    public Application() : base( new VideoMode( 800, 600 ) )
    {
        _toolbar = new Toolbar( ( Vector2f )WindowSize );
        
        KeyPressed += OnKeyPressed;
        MouseButtonPressed += OnMouseButtonPressed;
        MouseButtonDoublePressed += OnDoubleClick;
        MouseButtonReleased += OnMouseButtonReleased;
        MouseButtonReleased += _toolbar.OnMouseReleased;
    }

    protected override void Draw()
    {
        ClearWindow( Color.White );
        
        RenderObject( _toolbar );

        _dragAndDropHandler.Update( _selectionHandler.GetAllSelectedShapes() );

        foreach ( CashedShape shape in _shapesRepository.GetAll() )
        {
            RenderObject( shape );

            var shapeMarks = new List<Drawable?>
            {
                _selectionHandler.BuildSelectionMarkIfSelected( shape ),
                _groupingHandler.BuildGroupMarkIfHasGroup( shape )
            };

            foreach ( Drawable? shapeMark in shapeMarks )
            {
                if ( shapeMark is not null )
                {
                    RenderObject( shapeMark );
                }
            }
        }
    }

    private void OnKeyPressed( object? sender, KeyEventArgs keyEventArgs )
    {
        switch ( keyEventArgs.Code )
        {
            case Keyboard.Key.G when Keyboard.IsKeyPressed( Keyboard.Key.LControl ):
            {
                var selectedItems = _selectionHandler.GetAllSelectedShapes();
                _groupingHandler.Group( selectedItems );
                break;
            }
            case Keyboard.Key.U when Keyboard.IsKeyPressed( Keyboard.Key.LControl ):
            {
                var selectedItems = _selectionHandler.GetSelectedShapes( SelectionType.TrueSelection );
                _groupingHandler.Ungroup( selectedItems );
                _selectionHandler.OnUngroup( selectedItems );
                break;
            }
        }
    }

    private void OnMouseButtonPressed( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( mouseEventArgs.Button == Mouse.Button.Left )
        {
            CashedShape? clickedShape = GetShapeByCoordinates( mouseEventArgs.X, mouseEventArgs.Y );
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
            CashedShape? clickedShape = GetShapeByCoordinates( mouseEventArgs.X, mouseEventArgs.Y );
            var relatedShapes = GetRelatedShapes( clickedShape );

            _selectionHandler.OnDoubleClick( clickedShape, relatedShapes );
        }
    }

    private CashedShape? GetShapeByCoordinates( float x, float y )
    {
        return _shapesRepository.LastOrDefault( shape => shape
            .GetGlobalBounds()
            .Contains( x, y ) );
    }

    private List<CashedShape> GetRelatedShapes( CashedShape? clickedShape )
    {
        return clickedShape != null
            ? _groupingHandler.GetRelatedShapes( clickedShape )
            : new List<CashedShape>();
    }
}