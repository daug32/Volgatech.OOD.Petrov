using Lab2.Extensions;
using Lab2.Models;
using Lab2.Public;
using SFML.Graphics;

namespace Lab2.Handlers;

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
            _groups.Add( newGroup );
        }
    }

    public void Ungroup( IEnumerable<Shape> shapes )
    {
        shapes.Any();
    }

    public IEnumerable<Shape> GetShapesInGroup( Shape shape )
    {
        ShapeGroup? group = _groups.FirstOrDefault( x => x.Contains( shape ) );
        if ( group is null )
        {
            return Array.Empty<Shape>();
        }

        return group.GetShapes();
    }

    public Drawable BuildGroupMark( FloatRect shapeBounds )
    {
        uint textSize = 12;
        return new Text( "Group", Resources.Fonts.Roboto )
            .FluentSetPosition( shapeBounds.Left - textSize, shapeBounds.Top - textSize )
            .FluentSetCharacterSize( textSize );
    }
}