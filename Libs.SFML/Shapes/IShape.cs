using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.Shapes;

public interface IShape : Drawable
{
    Color FillColor { get; set; }
    
    Color OutlineColor { get; set; }
    float OutlineThickness { get; set; }
    
    Vector2f Position { get; set; }
    float Rotation { get; set; }
    Vector2f Scale { get; set; }
    Vector2f Origin { get; set; }
    
    FloatRect GetLocalBounds();
    FloatRect GetGlobalBounds();

    void AcceptVisitor( IShapeVisitor visitor );
}