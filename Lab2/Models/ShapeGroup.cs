using Libs.Extensions;
using Libs.SFML.Shapes;

namespace Lab2.Models;

public class ShapeGroup
{
    private readonly HashSet<CashedShape> _shapes;
    private readonly HashSet<ShapeGroup> _childGroups;

    public int Count { get; private set; }

    public ShapeGroup()
    {
        _shapes = new HashSet<CashedShape>();
        _childGroups = new HashSet<ShapeGroup>();
    }

    public ShapeGroup( IEnumerable<CashedShape> cashedShapes )
    {
        _shapes = new HashSet<CashedShape>( cashedShapes );
        _childGroups = new HashSet<ShapeGroup>();
    }

    public void AddToGroup( CashedShape shape )
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
        if ( _childGroups.Contains( group ) )
        {
            return;
        }

        _childGroups.Add( group );
        Count += group.Count;
    }

    public void AddGroups( IEnumerable<ShapeGroup> groups )
    {
        _childGroups.AddRange( groups );
    }

    public void AddGroup( ShapeGroup group )
    {
        _childGroups.Add( group );
    }

    public void ClearGroups()
    {
        _childGroups.Clear();
    }

    public List<ShapeGroup> GetChildGroups()
    {
        return _childGroups.ToList();
    }

    public bool IsValid()
    {
        bool isWrapping = 
            ( _childGroups.Count == 1 && _shapes.Count == 0 ) || 
            ( _childGroups.Count == 0 && _shapes.Count == 1 );

        if ( isWrapping )
        {
            return false;
        }

        return Count > 1;
    }

    public bool Contains( CashedShape shape )
    {
        return _shapes.Contains( shape ) || _childGroups.Any( x => x.Contains( shape ) );
    }

    public List<CashedShape> GetAllRelatedShapes()
    {
        var result = new HashSet<CashedShape>();
        var groupsToVisit = new List<ShapeGroup>
        {
            this
        };

        while ( groupsToVisit.Any() )
        {
            ShapeGroup group = groupsToVisit.Last();

            groupsToVisit.RemoveAt( groupsToVisit.Count - 1 );
            groupsToVisit.AddRange( group._childGroups );

            result.AddRange( group._shapes );
        }

        return result.ToList();
    }

    public ShapeGroup? FindFirstGroupOrDefault( Func<ShapeGroup, bool> predicate )
    {
        return _childGroups.FirstOrDefault( predicate );
    }

    public CashedShape? FindFirstShapeOrDefault( Func<CashedShape, bool> predicate )
    {
        var groupsToVisit = new List<ShapeGroup>
        {
            this
        };

        while ( groupsToVisit.Any() )
        {
            ShapeGroup group = groupsToVisit.Last();

            CashedShape? shape = group._shapes.FirstOrDefault( predicate );
            if ( shape != null )
            {
                return shape;
            }

            groupsToVisit.RemoveAt( groupsToVisit.Count - 1 );
            groupsToVisit.AddRange( group._childGroups );
        }

        return null;
    }
}