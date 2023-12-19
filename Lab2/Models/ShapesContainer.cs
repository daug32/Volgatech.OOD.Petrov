using Lab2.Data;
using Libs.Extensions;
using Libs.SFML.Shapes;

namespace Lab2.Models;

public class ShapesContainer
{
    private readonly ShapeGroup _mainGroup = new( DataLoader.LoadData() );

    public IEnumerable<CashedShape> GetAll()
    {
        return _mainGroup.GetAllRelatedShapes();
    }

    public CashedShape? FirstOrDefault( Func<CashedShape, bool> predicate )
    {
        return _mainGroup.FindFirstShapeOrDefault( predicate );
    } 
    
    public bool HasGroup( CashedShape shape )
    {
        return _mainGroup.FindFirstGroupOrDefault( x => x.Contains( shape ) ) != null;
    }

    public void Add( CashedShape shape )
    {
        _mainGroup.AddToGroup( shape );
    }
    
    public void Group( IEnumerable<CashedShape> shapes )
    {
        var newGroup = new ShapeGroup();

        var groupsToVisit = new List<ShapeGroup>( _mainGroup.GetChildGroups() );

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
            _mainGroup.ClearGroups();
            _mainGroup.AddGroups( groupsToVisit );
            _mainGroup.AddGroup( newGroup );
        }
    }
    
    public void Ungroup( IEnumerable<CashedShape> shapes )
    {
        foreach ( CashedShape shape in shapes )
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
    
    public List<CashedShape> GetRelatedShapes( CashedShape? shape )
    {
        if ( shape is null )
        {
            return new List<CashedShape>();
        }
        
        ShapeGroup? shapeGroup = _mainGroup.FindFirstGroupOrDefault( x => x.Contains( shape ) );
        if ( shapeGroup is null )
        {
            return new List<CashedShape>();
        }

        var shapes = shapeGroup.GetAllRelatedShapes();
        shapes.Remove( shape );

        return shapes;
    }
}