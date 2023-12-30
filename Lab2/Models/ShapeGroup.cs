using Libs.Linq.Extensions;
using Libs.SFML.Shapes;

namespace Lab2.Models;

public class ShapeGroup
{
    private readonly HashSet<IShape> _shapes;
    private readonly HashSet<ShapeGroup> _childGroups;

    public int Count { get; private set; }

    public ShapeGroup()
    {
        _shapes = new HashSet<IShape>();
        _childGroups = new HashSet<ShapeGroup>();
    }

    public ShapeGroup( IEnumerable<IShape> shapes )
    {
        _shapes = new HashSet<IShape>( shapes );
        _childGroups = new HashSet<ShapeGroup>();
    }

    public void AddToGroup( IShape baseShape )
    {
        if ( _shapes.Contains( baseShape ) )
        {
            return;
        }

        _shapes.Add( baseShape );
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

    public bool Contains( IShape baseShape )
    {
        return _shapes.Contains( baseShape ) || _childGroups.Any( x => x.Contains( baseShape ) );
    }

    public List<IShape> GetAllRelatedShapes()
    {
        var result = new HashSet<IShape>( Count );
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

    public IShape? FindFirstShapeOrDefault( Func<IShape, bool> predicate )
    {
        var groupsToVisit = new List<ShapeGroup>
        {
            this
        };

        while ( groupsToVisit.Any() )
        {
            ShapeGroup group = groupsToVisit.Last();

            IShape? shape = group._shapes.FirstOrDefault( predicate );
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