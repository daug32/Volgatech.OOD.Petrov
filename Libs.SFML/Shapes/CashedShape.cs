using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.Shapes;

public class CashedShape : Drawable
{
    protected bool HasChanges = true;

    protected readonly Shape Shape;
    protected FloatRect OldGlobalBounds;
    protected FloatRect OldLocalBounds;

    public static CashedShape Create( Shape shape )
    {
        return new( shape );
    }

    private CashedShape( Shape shape )
    {
        Shape = shape;
    }

    public virtual Color FillColor
    {
        get => Shape.FillColor;
        set
        {
            HasChanges = true;
            Shape.FillColor = value;
        }
    }

    public virtual Color OutlineColor
    {
        get => Shape.OutlineColor;
        set
        {
            HasChanges = true;
            Shape.OutlineColor = value;
        }
    }

    public virtual float OutlineThickness
    {
        get => Shape.OutlineThickness;
        set
        {
            HasChanges = true;
            Shape.OutlineThickness = value;
        }
    }

    public virtual Vector2f Position
    {
        get => Shape.Position;
        set
        {
            HasChanges = true;
            Shape.Position = value;
        }
    }

    public virtual float Rotation
    {
        get => Shape.Rotation;
        set
        {
            HasChanges = true;
            Shape.Rotation = value;
        }
    }

    public virtual Vector2f Scale
    {
        get => Shape.Scale;
        set
        {
            HasChanges = true;
            Shape.Scale = value;
        }
    }

    public virtual Vector2f Origin
    {
        get => Shape.Origin;
        set
        {
            HasChanges = true;
            Shape.Origin = value;
        }
    }

    public virtual FloatRect GetLocalBounds()
    {
        RecalculateIfNeed();
        return OldLocalBounds;
    }

    public virtual FloatRect GetGlobalBounds()
    {
        RecalculateIfNeed();
        return OldGlobalBounds;
    }

    public virtual void Draw( RenderTarget target, RenderStates states )
    {
        Shape.Draw( target, states );
    }

    public virtual Shape ToShape()
    {
        return Shape;
    }

    protected virtual void RecalculateIfNeed()
    {
        if ( !HasChanges )
        {
            return;
        }

        OldGlobalBounds = Shape.GetGlobalBounds();
        OldLocalBounds = Shape.GetLocalBounds();

        HasChanges = false;
    }
}