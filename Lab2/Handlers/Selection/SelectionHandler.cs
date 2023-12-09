using Libs.SFML.Colors;
using Libs.SFML.Shapes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2.Handlers.Selection;

public class SelectionHandler
{
    private readonly SelectedShapesContainer _selectionHandler = new();

    public IEnumerable<CashedShape> GetAllSelectedShapes()
    {
        return _selectionHandler.GetAllSelectedShapes();
    }

    public IEnumerable<CashedShape> GetSelectedShapes( SelectionType selectionType )
    {
        return _selectionHandler.GetSelectedShapes( selectionType );
    }

    public void OnUngroup( IEnumerable<CashedShape> shapes )
    {
        _selectionHandler.UnselectAll();
        _selectionHandler.Select( shapes, SelectionType.TrueSelection );
    }

    public void OnDoubleClick( CashedShape? clickedShape, IEnumerable<CashedShape> relatedShapes )
    {
        if ( clickedShape is null )
        {
            return;
        }

        bool isAnyRelatedShapeNotSelected = relatedShapes
            .Any( x => _selectionHandler.GetSelectionType( x ) == SelectionType.NotSelected );
        if ( isAnyRelatedShapeNotSelected )
        {
            _selectionHandler.Select( relatedShapes, SelectionType.GroupSelection );
            _selectionHandler.Select( clickedShape, SelectionType.TrueSelection );
            return;
        }

        _selectionHandler.UnselectAll();
        _selectionHandler.Select( clickedShape, SelectionType.TrueSelection );
    }

    public void OnMousePressed(
        CashedShape? clickedShape,
        IEnumerable<CashedShape> relatedShapes )
    {
        if ( clickedShape is null )
        {
            if ( !CanSetMultipleSelections() )
            {
                _selectionHandler.UnselectAll();
            }

            return;
        }

        SelectionType selectionType = _selectionHandler.GetSelectionType( clickedShape );
        if ( selectionType == SelectionType.NotSelected )
        {
            SelectNewShape( clickedShape, relatedShapes );
            return;
        }

        ReselectShape( clickedShape, relatedShapes, selectionType );
    }

    public Drawable? BuildSelectionMarkIfSelected( CashedShape cashedShape )
    {
        SelectionType selectionType = _selectionHandler.GetSelectionType( cashedShape );
        if ( selectionType == SelectionType.NotSelected )
        {
            return null;
        }

        FloatRect shapeBounds = cashedShape.GetGlobalBounds();

        var markSize = new Vector2f( shapeBounds.Width, shapeBounds.Height );
        Color markOutlineColor = selectionType == SelectionType.TrueSelection
            ? Color.White
            : CustomColors.LightGray;

        return CashedShape.Create( new RectangleShape( markSize ) )
            .FluentSetPosition( shapeBounds.Left, shapeBounds.Top )
            .FluentSetOutlineColor( markOutlineColor )
            .FluentSetFillColor( Color.Transparent )
            .FluentSetOutlineThickness( 1 );
    }

    private void ReselectShape( CashedShape clickedShape, IEnumerable<CashedShape> relatedShapes, SelectionType selectionType )
    {
        if ( selectionType == SelectionType.GroupSelection )
        {
            if ( CanSetMultipleSelections() )
            {
                _selectionHandler.Select( clickedShape, SelectionType.TrueSelection );
                return;
            }

            _selectionHandler.UnselectAll();
            _selectionHandler.Select( clickedShape, SelectionType.TrueSelection );
            _selectionHandler.Select( relatedShapes, SelectionType.GroupSelection );
            return;
        }

        if ( selectionType == SelectionType.TrueSelection )
        {
            if ( !CanSetMultipleSelections() )
            {
                return;
            }

            _selectionHandler.Unselect( clickedShape );
            if ( !_selectionHandler.AnyWithSelectionType( SelectionType.TrueSelection ) )
            {
                _selectionHandler.UnselectAll();
            }
        }
    }

    private void SelectNewShape( CashedShape clickedShape, IEnumerable<CashedShape> relatedShapes )
    {
        if ( !CanSetMultipleSelections() )
        {
            _selectionHandler.UnselectAll();
        }

        _selectionHandler.Select( clickedShape, SelectionType.TrueSelection );
        _selectionHandler.Select( relatedShapes, SelectionType.GroupSelection );
    }

    private static bool CanSetMultipleSelections()
    {
        return Keyboard.IsKeyPressed( Keyboard.Key.LShift ) || Keyboard.IsKeyPressed( Keyboard.Key.RShift );
    }
}