namespace Libs.Models;

public class ListIterator<T>
{
    private int _currentValueIndex;
    private readonly List<T> _values;

    public ListIterator( T[] values )
    {
        _values = values.ToList();
    }

    public T GetCurrentValue() => _values[_currentValueIndex];
    public void MoveToNext() => _currentValueIndex = ( _currentValueIndex + 1 ) % _values.Count;
}