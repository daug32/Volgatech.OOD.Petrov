using Lab2.Data;
using Lab2.Handlers;
using Lab2.Handlers.Grouping;
using Lab2.Handlers.Selection;
using Lab2.Public;
using Libs.SFML.Applications;
using Libs.SFML.Colors;
using Libs.SFML.Shapes;
using Libs.SFML.UI;
using Libs.SFML.UI.Components.Buttons;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2;

public class Application : BaseApplication
{
    private readonly HashSet<CashedShape> _shapes = new( DataLoader.LoadData() );

    private readonly Menu _toolbar;

    private readonly DragAndDropHandler _dragAndDropHandler = new();
    private readonly GroupingHandler _groupingHandler = new();
    private readonly SelectionHandler _selectionHandler = new();

    public Application() : base( new VideoMode( 800, 600 ) )
    {
        _toolbar = new Menu( new Vector2f( WindowSize.X, 50 ) )
        {
            BackgroundColor = CustomColors.Purple
        };

        _toolbar.AddButton( new TextButton(
            position: new Vector2f( 20, 10f  ),
            text: new Text( "Button 1", Resources.Fonts.Roboto, 20 ),
            onClick: button => Console.WriteLine( $"{button.GetGlobalBounds()}" ) ) );
        
        KeyPressed += OnKeyPressed;
        MouseButtonPressed += OnMouseButtonPressed;
        MouseButtonDoublePressed += OnDoubleClick;
        MouseButtonReleased += OnMouseButtonReleased;
        MouseButtonReleased += _toolbar.OnMouseReleased;
    }

    protected override void Draw()
    {
        ClearWindow( CustomColors.PinkWhite );
        
        RenderObject( _toolbar );

        _dragAndDropHandler.Update( _selectionHandler.GetAllSelectedShapes() );

        foreach ( CashedShape shape in _shapes.ToList() )
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
            CashedShape? clickedShape = GetClickedShape( mouseEventArgs );
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
            CashedShape? clickedShape = GetClickedShape( mouseEventArgs );
            var relatedShapes = GetRelatedShapes( clickedShape );

            _selectionHandler.OnDoubleClick( clickedShape, relatedShapes );
        }
    }

    private CashedShape? GetClickedShape( MouseButtonEventArgs args )
    {
        return _shapes
            .LastOrDefault( shape => shape
                .GetGlobalBounds()
                .Contains( args.X, args.Y ) );
    }

    private List<CashedShape> GetRelatedShapes( CashedShape? clickedShape )
    {
        return clickedShape != null
            ? _groupingHandler.GetRelatedShapes( clickedShape )
            : new List<CashedShape>();
    }
}