using SFML.System;

namespace Libs.SFML.Shapes;

public interface ITransformable
{
    Vector2f Position { get; set; }
    float Rotation { get; set; }
    Vector2f Scale { get; set; }
    Vector2f Origin { get; set; }
}