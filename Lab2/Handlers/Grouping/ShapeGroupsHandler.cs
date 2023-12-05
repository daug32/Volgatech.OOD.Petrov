using Lab2.Extensions;
using Lab2.Public;
using SFML.Graphics;

namespace Lab2.Handlers.Grouping;

public class ShapeGroupsHandler
{
    private readonly List<ShapeGroup> _groups = new();

    public bool HasGroup( Shape shape ) => _groups.Any( x => x.Contains( shape ) );

    public void Group( IEnumerable<Shape> shapes )
    {
        var newGroup = new ShapeGroup();
        
        var groupsToVisit = _groups.ToList();
        
        foreach ( Shape shape in shapes )
        {
            bool hasGroup = false;

            foreach ( ShapeGroup group in groupsToVisit )
            {
                if ( group.Contains( shape ) )
                {
                    newGroup.AddToGroup( group );
                    groupsToVisit.Remove( group );
                    hasGroup = true;
                    break;
                }
            }

            if ( !hasGroup && !newGroup.Contains( shape ) )
            {
                newGroup.AddToGroup( shape );
            }
        }

        if ( newGroup.IsValid() )
        {
            _groups.Clear();
            _groups.AddRange( groupsToVisit );
            _groups.Add( newGroup );
        }
    }

    public void Ungroup( IEnumerable<Shape> shapes )
    {
        foreach ( Shape shape in shapes )
        {
            ShapeGroup? groupToSearch = _groups.FirstOrDefault( x => x.Contains( shape ) );
            if ( groupToSearch is null )
            {
                continue;
            }

            ShapeGroup shapeGroup = groupToSearch.GetGroup( shape )!;
            _groups.Remove( shapeGroup );
            _groups.AddRange( shapeGroup.GetChildGroups() );
        }
    }

    public List<Shape> GetRelatedShapes( Shape shape )
    {
        ShapeGroup? shapeGroup = _groups.FirstOrDefault( x => x.Contains( shape ) );
        if ( shapeGroup is null )
        {
            return new List<Shape>();
        }

        return shapeGroup.GetAllRelatedShapes();
    }

    public Drawable BuildGroupMark( FloatRect shapeBounds )
    {
        uint textSize = 10;
        return new Text( "Group", Resources.Fonts.Roboto )
            .FluentSetPosition(
                shapeBounds.Left - textSize,
                shapeBounds.Top - textSize )
            .FluentSetCharacterSize( textSize );
    }
}