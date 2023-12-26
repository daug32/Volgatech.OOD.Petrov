using Libs.SFML.Shapes;

namespace Lab2.Handlers.Selection;

public class SelectedShapesContainer
{
    private readonly HashSet<ShapeDecorator> _selectedShapes = new();
    private readonly HashSet<ShapeDecorator> _relatedSelectedShapes = new();

    public bool AnyWithSelectionType( SelectionType selectionType )
    {
        switch ( selectionType )
        {
            case SelectionType.TrueSelection: return _selectedShapes.Any();
            case SelectionType.GroupSelection: return _relatedSelectedShapes.Any();
            default: throw new ArgumentOutOfRangeException( nameof( selectionType ), selectionType, null );
        }
    }

    public SelectionType GetSelectionType( ShapeDecorator shapeDecorator )
    {
        if ( _selectedShapes.Contains( shapeDecorator ) )
        {
            return SelectionType.TrueSelection;
        }

        if ( _relatedSelectedShapes.Contains( shapeDecorator ) )
        {
            return SelectionType.GroupSelection;
        }

        return SelectionType.NotSelected;
    }

    public List<ShapeDecorator> GetAllSelectedShapes()
    {
        return _selectedShapes
            .Union( _relatedSelectedShapes )
            .ToList();
    }

    public List<ShapeDecorator> GetSelectedShapes( SelectionType selectionType )
    {
        if ( selectionType == SelectionType.TrueSelection )
        {
            return _selectedShapes.ToList();
        }

        if ( selectionType == SelectionType.GroupSelection )
        {
            return _relatedSelectedShapes.ToList();
        }

        throw new ArgumentOutOfRangeException( nameof( selectionType ) );
    }

    public void Select( IEnumerable<ShapeDecorator> shapes, SelectionType selectionType )
    {
        foreach ( ShapeDecorator shape in shapes )
        {
            Select( shape, selectionType );
        }
    }

    public void Select( ShapeDecorator shapeDecorator, SelectionType selectionType )
    {
        if ( selectionType == SelectionType.TrueSelection )
        {
            _selectedShapes.Add( shapeDecorator );
            return;
        }

        if ( selectionType == SelectionType.GroupSelection )
        {
            // Shape can't be pseudo selected if it's already true selected
            if ( _selectedShapes.Contains( shapeDecorator ) )
            {
                return;
            }

            _relatedSelectedShapes.Add( shapeDecorator );
            return;
        }

        throw new ArgumentOutOfRangeException( nameof( selectionType ) );
    }

    public void UnselectAll()
    {
        _selectedShapes.Clear();
        _relatedSelectedShapes.Clear();
    }

    public void Unselect( ShapeDecorator shapeDecorator )
    {
        _selectedShapes.Remove( shapeDecorator );
        _relatedSelectedShapes.Remove( shapeDecorator );
    }
}