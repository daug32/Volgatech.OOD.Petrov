using Lab2.Data;
using Libs.Extensions;
using Libs.SFML.Shapes;

namespace Lab2.Models;

public class ShapesContainer
{
    private readonly ShapeGroup _mainGroup = new( DataLoader.LoadData() );

    public IEnumerable<ShapeDecorator> GetAll()
    {
        return _mainGroup.GetAllRelatedShapes();
    }

    public ShapeDecorator? FirstOrDefault( Func<ShapeDecorator, bool> predicate )
    {
        return _mainGroup.FindFirstShapeOrDefault( predicate );
    } 
    
    public bool HasGroup( ShapeDecorator shapeDecorator )
    {
        return _mainGroup.FindFirstGroupOrDefault( x => x.Contains( shapeDecorator ) ) != null;
    }

    public void Add( ShapeDecorator shapeDecorator )
    {
        _mainGroup.AddToGroup( shapeDecorator );
    }
    
    public void Group( IEnumerable<ShapeDecorator> shapes )
    {
        var newGroup = new ShapeGroup();

        var groupsToVisit = new List<ShapeGroup>( _mainGroup.GetChildGroups() );

        foreach ( ShapeDecorator shape in shapes )
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
    
    public void Ungroup( IEnumerable<ShapeDecorator> shapes )
    {
        foreach ( ShapeDecorator shape in shapes )
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
    
    public List<ShapeDecorator> GetRelatedShapes( ShapeDecorator? shape )
    {
        if ( shape is null )
        {
            return new List<ShapeDecorator>();
        }
        
        ShapeGroup? shapeGroup = _mainGroup.FindFirstGroupOrDefault( x => x.Contains( shape ) );
        if ( shapeGroup is null )
        {
            return new List<ShapeDecorator>();
        }

        var shapes = shapeGroup.GetAllRelatedShapes();
        shapes.Remove( shape );

        return shapes;
    }
}