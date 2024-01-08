using SFML.Graphics;

namespace Libs.SFML.Shapes;

public interface IShape : Drawable, ITransformable
{
    Color FillColor { get; set; }
    
    Color OutlineColor { get; set; }
    float OutlineThickness { get; set; }
    
    FloatRect GetLocalBounds();
    FloatRect GetGlobalBounds();

    void AcceptVisitor( IShapeVisitor visitor );
}