using Lab1.Models;

namespace Lab1.Tasks;

public class TaskInput
{
    public List<TriangleDecorator> Triangles { get; } = new();
    public List<CircleDecorator> Circles { get; } = new();
    public List<RectangleDecorator> Rectangles { get; } = new();

    public IEnumerable<ISurface> GetSurfaces() => Triangles
        .Cast<ISurface>()
        .Union( Circles )
        .Union( Rectangles );
}