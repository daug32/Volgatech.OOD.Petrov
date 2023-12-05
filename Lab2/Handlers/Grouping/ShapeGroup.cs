using SFML.Graphics;

namespace Lab2.Handlers.Grouping;

public class ShapeGroup
{
    public int Count { get; private set; }
    public HashSet<Shape> Shapes { get; private set; }
    public HashSet<ShapeGroup> ChildGroups { get; private set; }

    public ShapeGroup()
    {
        Shapes = new HashSet<Shape>();
        ChildGroups = new HashSet<ShapeGroup>();
    }

    public ShapeGroup( IEnumerable<Shape> shapes )
    {
        Shapes = shapes.ToHashSet();
        ChildGroups = new HashSet<ShapeGroup>();
    }

    public void AddToGroup( Shape shape )
    {
        if ( Shapes.Contains( shape ) )
        {
            return;
        }

        Shapes.Add( shape );
        Count++;
    }

    public void AddToGroup( ShapeGroup group )
    {
        if ( ChildGroups.Contains( group ) )
        {
            return;
        }
        
        ChildGroups.Add( group );
        Count += group.Count;
    }

    public void RemoveFromGroup( Shape shape )
    {
        Shapes.Remove( shape );
        Count--;
    }

    public void RemoveFromGroup( ShapeGroup group )
    {
        if ( !ChildGroups.Contains( group ) )
        {
            return;
        }

        ChildGroups.Remove( group );
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
            result.AddRange( group.Shapes );
            groupsToVisit.AddRange( group.ChildGroups );
        }

        return result;
    }

    public bool IsValid()
    {
        bool isWrapping = ChildGroups.Count == 1 && Shapes.Count == 0;
        if ( isWrapping )
        {
            return false;
        }

        return Count > 1;
    }

    public bool Contains( Shape shape )
    {
        return
            Shapes.Contains( shape ) || 
            ChildGroups.Any( x => x.Contains( shape ) );
    }

    public ShapeGroup? GetGroup( Shape shape )
    {
        if ( Shapes.Contains( shape ) )
        {
            return this;
        }

        foreach ( ShapeGroup childGroup in ChildGroups )
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