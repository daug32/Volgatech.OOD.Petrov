using Libs.SFML.Shapes;

namespace Lab2.Data;

public class ShapesRepository
{
    private readonly HashSet<CashedShape> _shapes = new( DataLoader.LoadData() );

    public IEnumerable<CashedShape> GetAll()
    {
        return _shapes;
    }

    public CashedShape? LastOrDefault( Func<CashedShape, bool> predicate )
    {
        return _shapes.LastOrDefault( predicate );
    } 
}