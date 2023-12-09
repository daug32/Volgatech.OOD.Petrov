using Libs.SFML.Shapes;

namespace Lab2.Handlers.Selection;

public class SelectedShapesContainer
{
    private readonly HashSet<CashedShape> _selectedShapes = new();
    private readonly HashSet<CashedShape> _relatedSelectedShapes = new();

    public bool AnyWithSelectionType( SelectionType selectionType )
    {
        switch ( selectionType )
        {
            case SelectionType.TrueSelection: return _selectedShapes.Any();
            case SelectionType.GroupSelection: return _relatedSelectedShapes.Any();
            default: throw new ArgumentOutOfRangeException( nameof( selectionType ), selectionType, null );
        }
    }

    public SelectionType GetSelectionType( CashedShape shape )
    {
        if ( _selectedShapes.Contains( shape ) )
        {
            return SelectionType.TrueSelection;
        }

        if ( _relatedSelectedShapes.Contains( shape ) )
        {
            return SelectionType.GroupSelection;
        }

        return SelectionType.NotSelected;
    }

    public List<CashedShape> GetAllSelectedShapes()
    {
        return _selectedShapes
            .Union( _relatedSelectedShapes )
            .ToList();
    }

    public List<CashedShape> GetSelectedShapes( SelectionType selectionType )
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

    public void Select( IEnumerable<CashedShape> shapes, SelectionType selectionType )
    {
        foreach ( CashedShape shape in shapes )
        {
            Select( shape, selectionType );
        }
    }

    public void Select( CashedShape shape, SelectionType selectionType )
    {
        if ( selectionType == SelectionType.TrueSelection )
        {
            _selectedShapes.Add( shape );
            return;
        }

        if ( selectionType == SelectionType.GroupSelection )
        {
            // Shape can't be pseudo selected if it's already true selected
            if ( _selectedShapes.Contains( shape ) )
            {
                return;
            }

            _relatedSelectedShapes.Add( shape );
            return;
        }

        throw new ArgumentOutOfRangeException( nameof( selectionType ) );
    }

    public void UnselectAll()
    {
        _selectedShapes.Clear();
        _relatedSelectedShapes.Clear();
    }

    public void Unselect( CashedShape shape )
    {
        _selectedShapes.Remove( shape );
        _relatedSelectedShapes.Remove( shape );
    }
}