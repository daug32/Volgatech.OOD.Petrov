using Lab2.Public;
using Libs.Extensions;
using Libs.SFML.Colors;
using Libs.SFML.Shapes;
using SFML.Graphics;
using SFML.System;

namespace Lab2.Handlers.Grouping;

public class GroupingHandler
{
    public static readonly Color MarkColor = CustomColors.Gray;
    
    private readonly List<ShapeGroup> _groups = new();

    public void Group( IEnumerable<CashedShape> shapes )
    {
        var newGroup = new ShapeGroup();

        var groupsToVisit = _groups.ToList();

        foreach ( CashedShape shape in shapes )
        {
            var groupAdded = false;

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
            var queue = new LinkedList<ShapeGroup>( _groups );
            var toSave = new LinkedList<ShapeGroup>();

            while ( queue.Any() )
            {
                ShapeGroup group = queue.Last();
                if ( !group.Contains( shape ) )
                {
                    toSave.AddLast( group );
                    queue.RemoveLast();
                    continue;
                }

                queue.RemoveLast();
                toSave.AddRange( queue );
                queue.Clear();
                queue.AddRange( group.GetChildGroups() );
            }

            _groups.Clear();
            _groups.AddRange( toSave );
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
        bool hasGroup = _groups.Any( x => x.Contains( shape ) );
        if ( !hasGroup )
        {
            return null;
        }

        uint textSize = 10;
        FloatRect shapeBounds = shape.GetGlobalBounds();

        var text = new Text( "Group", Resources.Fonts.Roboto );
        text.Position = new Vector2f( shapeBounds.Left - textSize, shapeBounds.Top - textSize );
        text.CharacterSize = textSize;
        text.FillColor = MarkColor;

        return text;
    }
}