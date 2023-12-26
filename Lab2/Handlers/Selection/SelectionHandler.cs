using Libs.SFML.Shapes;
using SFML.Window;

namespace Lab2.Handlers.Selection;

public class SelectionHandler
{
    private readonly SelectedShapesContainer _selectionContainer = new();

    public IEnumerable<ShapeDecorator> GetAllSelectedShapes()
    {
        return _selectionContainer.GetAllSelectedShapes();
    }

    public List<ShapeDecorator> GetSelectedShapes( SelectionType selectionType )
    {
        return _selectionContainer.GetSelectedShapes( selectionType );
    }

    public SelectionType GetSelectionType( ShapeDecorator shapeDecorator )
    {
        return _selectionContainer.GetSelectionType( shapeDecorator );
    }

    public void OnUngroup( IEnumerable<ShapeDecorator> shapes )
    {
        _selectionContainer.UnselectAll();
        _selectionContainer.Select( shapes, SelectionType.TrueSelection );
    }

    public void OnDoubleClick( ShapeDecorator? clickedShape, List<ShapeDecorator> relatedShapes )
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
        ShapeDecorator? clickedShape,
        IEnumerable<ShapeDecorator> relatedShapes )
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

    private void ReselectShape( ShapeDecorator clickedShapeDecorator, IEnumerable<ShapeDecorator> relatedShapes, SelectionType selectionType )
    {
        if ( selectionType == SelectionType.GroupSelection )
        {
            if ( CanSetMultipleSelections() )
            {
                _selectionContainer.Select( clickedShapeDecorator, SelectionType.TrueSelection );
                return;
            }

            _selectionContainer.UnselectAll();
            _selectionContainer.Select( clickedShapeDecorator, SelectionType.TrueSelection );
            _selectionContainer.Select( relatedShapes, SelectionType.GroupSelection );
            return;
        }

        if ( selectionType == SelectionType.TrueSelection )
        {
            if ( !CanSetMultipleSelections() )
            {
                return;
            }

            _selectionContainer.Unselect( clickedShapeDecorator );
            if ( !_selectionContainer.AnyWithSelectionType( SelectionType.TrueSelection ) )
            {
                _selectionContainer.UnselectAll();
            }

            return;
        }

        throw new ArgumentOutOfRangeException();
    }

    private void SelectNewShape( ShapeDecorator clickedShapeDecorator, IEnumerable<ShapeDecorator> relatedShapes )
    {
        if ( !CanSetMultipleSelections() )
        {
            _selectionContainer.UnselectAll();
        }

        _selectionContainer.Select( clickedShapeDecorator, SelectionType.TrueSelection );
        _selectionContainer.Select( relatedShapes, SelectionType.GroupSelection );
    }

    private static bool CanSetMultipleSelections()
    {
        return Keyboard.IsKeyPressed( Keyboard.Key.LShift ) || Keyboard.IsKeyPressed( Keyboard.Key.RShift );
    }
}