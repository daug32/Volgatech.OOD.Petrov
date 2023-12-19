using Libs.SFML.Shapes;
using SFML.Window;

namespace Lab2.Handlers.Selection;

public class SelectionHandler
{
    private readonly SelectedShapesContainer _selectionContainer = new();

    public IEnumerable<CashedShape> GetAllSelectedShapes()
    {
        return _selectionContainer.GetAllSelectedShapes();
    }

    public IEnumerable<CashedShape> GetSelectedShapes( SelectionType selectionType )
    {
        return _selectionContainer.GetSelectedShapes( selectionType );
    }

    public SelectionType GetSelectionType( CashedShape shape )
    {
        return _selectionContainer.GetSelectionType( shape );
    }

    public void OnUngroup( IEnumerable<CashedShape> shapes )
    {
        _selectionContainer.UnselectAll();
        _selectionContainer.Select( shapes, SelectionType.TrueSelection );
    }

    public void OnDoubleClick( CashedShape? clickedShape, IEnumerable<CashedShape> relatedShapes )
    {
        if ( clickedShape is null )
        {
            return;
        }

        bool isAnyRelatedShapeNotSelected = relatedShapes
            .Any( x => _selectionContainer.GetSelectionType( x ) == SelectionType.NotSelected );

        if ( isAnyRelatedShapeNotSelected )
        {
            _selectionContainer.Select( relatedShapes, SelectionType.GroupSelection );
            _selectionContainer.Select( clickedShape, SelectionType.TrueSelection );
            return;
        }

        _selectionContainer.UnselectAll();
        _selectionContainer.Select( clickedShape, SelectionType.TrueSelection );
    }

    public void OnMousePressed(
        CashedShape? clickedShape,
        IEnumerable<CashedShape> relatedShapes )
    {
        if ( clickedShape is null )
        {
            if ( !CanSetMultipleSelections() )
            {
                _selectionContainer.UnselectAll();
            }

            return;
        }

        SelectionType selectionType = _selectionContainer.GetSelectionType( clickedShape );
        if ( selectionType == SelectionType.NotSelected )
        {
            SelectNewShape( clickedShape, relatedShapes );
            return;
        }

        ReselectShape( clickedShape, relatedShapes, selectionType );
    }

    private void ReselectShape( CashedShape clickedShape, IEnumerable<CashedShape> relatedShapes, SelectionType selectionType )
    {
        if ( selectionType == SelectionType.GroupSelection )
        {
            if ( CanSetMultipleSelections() )
            {
                _selectionContainer.Select( clickedShape, SelectionType.TrueSelection );
                return;
            }

            _selectionContainer.UnselectAll();
            _selectionContainer.Select( clickedShape, SelectionType.TrueSelection );
            _selectionContainer.Select( relatedShapes, SelectionType.GroupSelection );
            return;
        }

        if ( selectionType == SelectionType.TrueSelection )
        {
            if ( !CanSetMultipleSelections() )
            {
                return;
            }

            _selectionContainer.Unselect( clickedShape );
            if ( !_selectionContainer.AnyWithSelectionType( SelectionType.TrueSelection ) )
            {
                _selectionContainer.UnselectAll();
            }

            return;
        }

        throw new ArgumentOutOfRangeException();
    }

    private void SelectNewShape( CashedShape clickedShape, IEnumerable<CashedShape> relatedShapes )
    {
        if ( !CanSetMultipleSelections() )
        {
            _selectionContainer.UnselectAll();
        }

        _selectionContainer.Select( clickedShape, SelectionType.TrueSelection );
        _selectionContainer.Select( relatedShapes, SelectionType.GroupSelection );
    }

    private static bool CanSetMultipleSelections()
    {
        return Keyboard.IsKeyPressed( Keyboard.Key.LShift ) || Keyboard.IsKeyPressed( Keyboard.Key.RShift );
    }
}