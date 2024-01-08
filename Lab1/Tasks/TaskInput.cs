using Lab1.Models;

namespace Lab1.Tasks;

public class TaskInput
{
    public List<Triangle> Triangles { get; } = new();
    public List<Circle> Circles { get; } = new();
    public List<Rectangle> Rectangles { get; } = new();

    public IEnumerable<ISurface> GetSurfaces() => Triangles
        .Cast<ISurface>()
        .Union( Circles )
        .Union( Rectangles );
}