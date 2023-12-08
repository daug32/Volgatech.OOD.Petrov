using Lab2.Public;
using Libs.SFML.Shapes;
using SFML.Graphics;
using SFML.System;

namespace Lab2.Handlers.Grouping;

public class ShapeGroupsHandler
{
    private readonly List<ShapeGroup> _groups = new();

    public bool HasGroup( CashedShape shape ) => _groups.Any( x => x.Contains( shape ) );

    public void Group( IEnumerable<CashedShape> shapes )
    {
        var newGroup = new ShapeGroup();
        
        var groupsToVisit = _groups.ToList();
        
        foreach ( CashedShape shape in shapes )
        {
            bool groupAdded = false;

            foreach ( ShapeGroup group in groupsToVisit )
            {
                if ( group.Contains( shape ) )
                {
                    newGroup.AddToGroup( group );
                    groupsToVisit.Remove( group );
                    groupAdded = true;
                    break;
                }
            }

            if ( !groupAdded && !newGroup.Contains( shape ) )
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

    public void Ungroup( IEnumerable<CashedShape> shapes )
    {
        foreach ( CashedShape shape in shapes )
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

    public List<CashedShape> GetRelatedShapes( CashedShape shape )
    {
        ShapeGroup? shapeGroup = _groups.FirstOrDefault( x => x.Contains( shape ) );
        if ( shapeGroup is null )
        {
            return new List<CashedShape>();
        }

        var shapes = shapeGroup.GetAllRelatedShapes();
        shapes.Remove( shape );
        
        return shapes;
    }

    public Drawable? BuildGroupMarkIfHasGroup( CashedShape shape )
    {
        if ( !HasGroup( shape ) )
        {
            return null;
        }
        
        uint textSize = 10;
        FloatRect shapeBounds = shape.GetGlobalBounds();
        
        var text = new Text( "Group", Resources.Fonts.Roboto );
        text.Position = new Vector2f( shapeBounds.Left - textSize, shapeBounds.Top - textSize );
        text.CharacterSize = textSize;

        return text;
    }
}