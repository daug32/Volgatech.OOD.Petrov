using Lab2.Handlers.Selection;
using Lab2.Models;
using Lab2.Models.Extensions;
using Libs.SFML.Shapes;
using SFML.Window;

namespace Lab2.Handlers.States.Implementation.Defaults;

internal class DefaultStateHandler : IStateHandler
{
    private readonly ShapesContainer _shapesContainer;
    private readonly SelectionHandler _selectionHandler;
    private readonly DragAndDropHandler _dragAndDropHandler;

    public State State => State.Default;

    public DefaultStateHandler( ShapesContainer shapesContainer, SelectionHandler selectionHandler )
    {
        _shapesContainer = shapesContainer;
        _selectionHandler = selectionHandler;
        _dragAndDropHandler = new DragAndDropHandler();
    }

    public IShape? GetStateDescription()
    {
        return null;
    }

    public void BeforeDraw()
    {
        _dragAndDropHandler.Update( _selectionHandler.GetAllSelectedShapes() );
    }

    public void OnKeyPressed( object? sender, KeyEventArgs keyEventArgs )
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

    public void OnMouseButtonPressed( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( mouseEventArgs.Button != Mouse.Button.Left )
        {
            return;
        }

        IShape? clickedShape = _shapesContainer.FindByPosition( mouseEventArgs.X, mouseEventArgs.Y );
        var relatedShapes = _shapesContainer.GetRelatedShapes( clickedShape );

        _dragAndDropHandler.OnMousePressed( clickedShape );
        _selectionHandler.OnMousePressed( clickedShape, relatedShapes );
    }

    public void OnMouseButtonReleased( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( mouseEventArgs.Button != Mouse.Button.Left )
        {
            return;
        }

        _dragAndDropHandler.OnMouseReleased();
    }

    public void OnDoubleClick( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        if ( mouseEventArgs.Button != Mouse.Button.Left )
        {
            return;
        }

        IShape? clickedShape = _shapesContainer.FindByPosition( mouseEventArgs.X, mouseEventArgs.Y );
        var relatedShapes = _shapesContainer.GetRelatedShapes( clickedShape );

        _selectionHandler.OnDoubleClick( clickedShape, relatedShapes );
    }
}