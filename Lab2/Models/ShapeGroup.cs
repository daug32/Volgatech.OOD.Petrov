using System.Collections;
using SFML.Graphics;

namespace Lab2.Models;

public class ShapeGroup
{
    private readonly HashSet<Shape> _shapes;
    private readonly HashSet<ShapeGroup> _groups;
    
    public int Count { get; private set; }

    public ShapeGroup()
    {
        _shapes = new HashSet<Shape>();
        _groups = new HashSet<ShapeGroup>();
    }

    public ShapeGroup( IEnumerable<Shape> shapes )
    {
        _shapes = shapes.ToHashSet();
        _groups = new HashSet<ShapeGroup>();
    }

    public void AddToGroup( Shape shape )
    {
        if ( _shapes.Contains( shape ) )
        {
            return;
        }

        _shapes.Add( shape );
        Count++;
    }

    public void AddToGroup( ShapeGroup group )
    {
        if ( _groups.Contains( group ) )
        {
            return;
        }
        
        _groups.Add( group );
        Count += group.Count;
    }

    public void RemoveFromGroup( Shape shape )
    {
        _shapes.Remove( shape );
        Count--;
    }

    public void RemoveFromGroup( ShapeGroup group )
    {
        if ( !_groups.Contains( group ) )
        {
            return;
        }

        _groups.Remove( group );
        Count -= group.Count;
    }

    public List<Shape> GetShapes()
    {
        var result = new List<Shape>();

        var groupsToVisit = new List<ShapeGroup>
        {
            this
        };

        while ( groupsToVisit.Any() )
        {
            ShapeGroup group = groupsToVisit.Last();
            groupsToVisit.RemoveAt( groupsToVisit.Count - 1 );
            result.AddRange( group._shapes );
            groupsToVisit.AddRange( group._groups );
        }

        return result;
    }

    public bool IsValid()
    {
        bool isWrapping = _groups.Count == 1 && _shapes.Count == 0;
        if ( isWrapping )
        {
            return false;
        }

        return Count > 1;
    }

    public bool Contains( Shape shape )
    {
        return
            _shapes.Contains( shape ) || 
            _groups.Any( x => x.Contains( shape ) );
    }
}