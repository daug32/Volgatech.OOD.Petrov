using Lab2.Data;
using Libs.Linq.Extensions;
using Libs.SFML.Shapes;

namespace Lab2.Models;

public class ShapesContainer
{
    private readonly ShapeGroup _mainGroup = new( DataLoader.LoadData() );

    public IEnumerable<IShape> GetAll()
    {
        return _mainGroup.GetAllRelatedShapes();
    }

    public IShape? FirstOrDefault( Func<IShape, bool> predicate )
    {
        return _mainGroup.FindFirstShapeOrDefault( predicate );
    } 
    
    public bool HasGroup( IShape baseShape )
    {
        return _mainGroup.FindFirstGroupOrDefault( x => x.Contains( baseShape ) ) != null;
    }

    public void Add( IShape baseShape )
    {
        _mainGroup.AddToGroup( baseShape );
    }
    
    public void Group( IEnumerable<IShape> shapes )
    {
        var newGroup = new ShapeGroup();

        var groupsToVisit = new List<ShapeGroup>( _mainGroup.GetChildGroups() );

        foreach ( IShape shape in shapes )
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
            _mainGroup.ClearGroups();
            _mainGroup.AddGroups( groupsToVisit );
            _mainGroup.AddGroup( newGroup );
        }
    }
    
    public void Ungroup( IEnumerable<IShape> shapes )
    {
        foreach ( IShape shape in shapes )
        {
            var queue = new LinkedList<ShapeGroup>( _mainGroup.GetChildGroups() );
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

            _mainGroup.ClearGroups();
            _mainGroup.AddGroups( toSave );
        }
    }
    
    public List<IShape> GetRelatedShapes( IShape? shape )
    {
        if ( shape is null )
        {
            return new List<IShape>();
        }
        
        ShapeGroup? shapeGroup = _mainGroup.FindFirstGroupOrDefault( x => x.Contains( shape ) );
        if ( shapeGroup is null )
        {
            return new List<IShape>();
        }

        var shapes = shapeGroup.GetAllRelatedShapes();
        shapes.Remove( shape );

        return shapes;
    }
}