namespace Libs.Memento;

public class MementoHandler
{
    private readonly LinkedList<IMemento> _items = new();
    private int _currentItemIndex = 0;

    public readonly int MaxStepsToRecord;
    public int SavedStatesCount => _items.Count;

    public MementoHandler( int maxStepsToRecord )
    {
        MaxStepsToRecord = maxStepsToRecord;
    }

    public void Undo()
    {
        if ( _items.Count == 0 )
        {
            return;
        }

        if ( _currentItemIndex - 1 > -1 )
        {
            _currentItemIndex--;
        }

        GetItemByIndex( _currentItemIndex )?.Restore();
    }

    public void Redo()
    {
        if ( _items.Count == 0 )
        {
            return;
        }

        if ( _currentItemIndex < _items.Count - 1 )
        {
            _currentItemIndex++;
        }

        GetItemByIndex( _currentItemIndex )?.Restore();
    }
    
    public void Save( IMemento memento )
    {
        // Remove tokens after current memento
        if ( _items.Count > 0 && _currentItemIndex != _items.Count - 1 )
        {
            while ( _currentItemIndex + 1 != _items.Count )
            {
                _items.RemoveLast();
            }
        }

        // Save this memento
        _items.AddLast( memento );

        // Remove items that are out of supported range
        if ( MaxStepsToRecord < _items.Count )
        {
            _items.RemoveFirst();
        }
        
        // Set current item index to the last 
        _currentItemIndex = _items.Count - 1;
    }

    internal IMemento? GetItemByIndex( int index )
    {
        var currentIndex = 0;
        foreach ( IMemento memento in _items )
        {
            if ( currentIndex == index )
            {
                return memento;
            }

            currentIndex++;
        }

        return null;
    }
}