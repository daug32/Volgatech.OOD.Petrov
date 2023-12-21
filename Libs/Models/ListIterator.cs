namespace Libs.Models;

public class ListIterator<T>
{
    private int _currentValueIndex;
    private readonly List<T> _values;

    public ListIterator( T[] values )
    {
        if ( values.Length < 1 )
        {
            throw new ArgumentException();
        }

        _values = values.ToList();
    }

    public T GetCurrentValue() => _values[_currentValueIndex];
    public void MoveToNextValue() => _currentValueIndex = ( _currentValueIndex + 1 ) % _values.Count;
}