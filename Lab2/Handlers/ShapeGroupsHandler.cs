using Lab2.Extensions;
using Lab2.Public;
using SFML.Graphics;

namespace Lab2.Handlers;

public class ShapeGroupsHandler
{
    private readonly List<ShapeGroup> _groups = new();

    public bool HasGroup( Shape shape ) => _groups.Any( x => x.Contains( shape ) );

    public void Group( IEnumerable<Shape> shapes )
    {
        var shapesWithoutGroup = new HashSet<Shape>();
        var existentGroupsToAdd = new HashSet<ShapeGroup>();

        foreach ( Shape shape in shapes )
        {
            var hasGroup = false;

            foreach ( ShapeGroup group in _groups )
            {
                if ( group.Contains( shape ) )
                {
                    existentGroupsToAdd.Add( group );
                    _groups.Remove( group );
                    hasGroup = true;
                    break;
                }
            }

            if ( !hasGroup )
            {
                shapesWithoutGroup.Add( shape );
            }
        }

        // Пытаемся повторно сгруппировать объекты из одной группы
        if ( !shapesWithoutGroup.Any() && existentGroupsToAdd.Count == 1 )
        {
            return;
        }

        var newGroup = new ShapeGroup();
        newGroup.Add( shapesWithoutGroup );
        newGroup.AddGroup( existentGroupsToAdd );
        
        _groups.Add( newGroup );
    }

    public void Ungroup( IEnumerable<Shape> shapes )
    {
        foreach ( Shape shape in shapes )
        {
            foreach ( ShapeGroup group in _groups )
            {
                if ( group.Contains( shape ) )
                {
                    _groups.Remove( group );
                    break;
                }
            }
        }
    }

    public Drawable BuildGroupMark( FloatRect shapeBounds )
    {
        uint textSize = 12;
        return new Text( "Group", Resources.Fonts.Roboto )
            .FluentSetPosition( shapeBounds.Left - textSize, shapeBounds.Top - textSize )
            .FluentSetCharacterSize( textSize );
    }
}

public class ShapeGroup
{
    private readonly HashSet<Shape> _shapes = new();
    private readonly HashSet<ShapeGroup> _childGroups = new();

    public int Count { get; private set; }

    public bool Contains( Shape shape )
    {
        if ( _shapes.Contains( shape ) )
        {
            return true;
        }

        foreach ( ShapeGroup childGroup in _childGroups )
        {
            if ( childGroup.Contains( shape ) )
            {
                return true;
            }
        }

        return false;
    }

    public void AddGroup( IEnumerable<ShapeGroup> groups )
    {
        foreach ( ShapeGroup shapeGroup in groups )
        {
            AddGroup( shapeGroup );
        }
    }

    public void AddGroup( ShapeGroup group )
    {
        if ( group.Count < 2 || _childGroups.Contains( group ) )
        {
            throw new InvalidOperationException();
        }

        _childGroups.Add( group );
        Count += group.Count;
    }

    public void Add( IEnumerable<Shape> shapes )
    {
        foreach ( Shape shape in shapes )
        {
            Add( shape );
        }
    }

    public void Add( Shape shape )
    {
        if ( _childGroups.Any( x => x.Contains( shape ) ) )
        {
            throw new InvalidOperationException();
        }

        _shapes.Add( shape );
        Count++;
    }
}