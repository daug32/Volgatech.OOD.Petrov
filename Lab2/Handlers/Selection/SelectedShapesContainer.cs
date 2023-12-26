using Libs.SFML.Shapes;

namespace Lab2.Handlers.Selection;

public class SelectedShapesContainer
{
    private readonly HashSet<IShape> _selectedShapes = new();
    private readonly HashSet<IShape> _relatedSelectedShapes = new();

    public bool AnyWithSelectionType( SelectionType selectionType )
    {
        switch ( selectionType )
        {
            case SelectionType.TrueSelection: return _selectedShapes.Any();
            case SelectionType.GroupSelection: return _relatedSelectedShapes.Any();
            default: throw new ArgumentOutOfRangeException( nameof( selectionType ), selectionType, null );
        }
    }

    public SelectionType GetSelectionType( IShape baseShape )
    {
        if ( _selectedShapes.Contains( baseShape ) )
        {
            return SelectionType.TrueSelection;
        }

        if ( _relatedSelectedShapes.Contains( baseShape ) )
        {
            return SelectionType.GroupSelection;
        }

        return SelectionType.NotSelected;
    }

    public List<IShape> GetAllSelectedShapes()
    {
        return _selectedShapes
            .Union( _relatedSelectedShapes )
            .ToList();
    }

    public List<IShape> GetSelectedShapes( SelectionType selectionType )
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

    public void Select( IEnumerable<IShape> shapes, SelectionType selectionType )
    {
        foreach ( IShape shape in shapes )
        {
            Select( shape, selectionType );
        }
    }

    public void Select( IShape baseShape, SelectionType selectionType )
    {
        if ( selectionType == SelectionType.TrueSelection )
        {
            _selectedShapes.Add( baseShape );
            return;
        }

        if ( selectionType == SelectionType.GroupSelection )
        {
            // Shape can't be pseudo selected if it's already true selected
            if ( _selectedShapes.Contains( baseShape ) )
            {
                return;
            }

            _relatedSelectedShapes.Add( baseShape );
            return;
        }

        throw new ArgumentOutOfRangeException( nameof( selectionType ) );
    }

    public void UnselectAll()
    {
        _selectedShapes.Clear();
        _relatedSelectedShapes.Clear();
    }

    public void Unselect( IShape baseShape )
    {
        _selectedShapes.Remove( baseShape );
        _relatedSelectedShapes.Remove( baseShape );
    }
}