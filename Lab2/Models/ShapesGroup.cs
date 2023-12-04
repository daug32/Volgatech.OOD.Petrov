using SFML.Graphics;

namespace Lab2.Models;

public class ShapesGroup
{
    private readonly HashSet<Shape> _shapes = new();
    private readonly List<ShapesGroup> _childGroups = new();

    public int Count { get; private set; } = 0;

    public ShapesGroup()
    {
    }
    
    public ShapesGroup( Shape shape )
    {
        _shapes = new HashSet<Shape>
        {
            shape
        };

        Count++;
    }

    public ShapesGroup( IEnumerable<Shape> shapes )
    {
        _shapes = shapes.ToHashSet();
        Count = _shapes.Count;
    }

    public bool Contains( Shape shape )
    {
        if ( _shapes.Contains( shape ) )
        {
            return true;
        }

        foreach ( ShapesGroup childGroup in _childGroups )
        {
            childGroup.Contains( shape );
        }

        return false;
    }

    public void Add( Shape shape )
    {
        _shapes.Add( shape );
        Count++;
    }

    public void AddGroup( ShapesGroup group )
    {
        _childGroups.Add( group );
        Count += group.Count;
    }

    public ShapesGroup? GetGroup( Shape shape )
    {
        return _childGroups.FirstOrDefault( x => x._shapes.Contains( shape ) );
    }

    public List<Shape> ToList()
    {
        var result = new List<Shape>( Count );
        
        var toVisit = new List<ShapesGroup>
        {
            this
        };

        while ( toVisit.Any() )
        {
            ShapesGroup item = toVisit.Last();
            toVisit.RemoveAt( toVisit.Count - 1 );
            result.AddRange( item._shapes );
            toVisit.AddRange( item._childGroups );
        }

        return result;
    }

    public void Clear()
    {
        _shapes.Clear();
        _childGroups.Clear();
        Count = 0;
    }

    public void Remove( Shape shape )
    {
        _shapes.Remove( shape );
        Count--;
    }
}