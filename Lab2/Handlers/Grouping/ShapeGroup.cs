using Lab2.Models;

namespace Lab2.Handlers.Grouping;

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

    public List<ShapeGroup> GetChildGroups() => _childGroups.ToList();

    public bool IsValid()
    {
        bool isWrapping = _childGroups.Count == 1 && _shapes.Count == 0;
        if ( isWrapping )
        {
            return false;
        }

        return Count > 1;
    }

    public bool Contains( CashedShape shape )
    {
        return
            _shapes.Contains( shape ) || 
            _childGroups.Any( x => x.Contains( shape ) );
    }

    public List<CashedShape> GetAllRelatedShapes()
    {
        var result = new List<CashedShape>();
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

        return result;
    }

    public ShapeGroup? GetGroup( CashedShape shape )
    {
        if ( _shapes.Contains( shape ) )
        {
            return this;
        }

        foreach ( ShapeGroup childGroup in _childGroups )
        {
            if ( !childGroup.Contains( shape ) )
            {
                continue;
            }

            return childGroup.GetGroup( shape );
        }

        return null;
    }
}