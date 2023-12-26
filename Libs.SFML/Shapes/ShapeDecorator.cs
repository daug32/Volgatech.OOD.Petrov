using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.Shapes;

public class ShapeDecorator : Drawable
{
    protected bool HasChanges = true;

    protected readonly Shape OriginalShape;
    protected FloatRect OldGlobalBounds;
    protected FloatRect OldLocalBounds;

    public ShapeDecorator( Shape originalOriginalShape )
    {
        OriginalShape = originalOriginalShape;
    }

    public virtual Color FillColor
    {
        get => OriginalShape.FillColor;
        set
        {
            HasChanges = true;
            OriginalShape.FillColor = value;
        }
    }

    public virtual Color OutlineColor
    {
        get => OriginalShape.OutlineColor;
        set
        {
            HasChanges = true;
            OriginalShape.OutlineColor = value;
        }
    }

    public virtual float OutlineThickness
    {
        get => OriginalShape.OutlineThickness;
        set
        {
            HasChanges = true;
            OriginalShape.OutlineThickness = value;
        }
    }

    public virtual Vector2f Position
    {
        get => OriginalShape.Position;
        set
        {
            HasChanges = true;
            OriginalShape.Position = value;
        }
    }

    public virtual float Rotation
    {
        get => OriginalShape.Rotation;
        set
        {
            HasChanges = true;
            OriginalShape.Rotation = value;
        }
    }

    public virtual Vector2f Scale
    {
        get => OriginalShape.Scale;
        set
        {
            HasChanges = true;
            OriginalShape.Scale = value;
        }
    }

    public virtual Vector2f Origin
    {
        get => OriginalShape.Origin;
        set
        {
            HasChanges = true;
            OriginalShape.Origin = value;
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
        OriginalShape.Draw( target, states );
    }

    protected virtual void RecalculateIfNeed()
    {
        if ( !HasChanges )
        {
            return;
        }

        OldGlobalBounds = OriginalShape.GetGlobalBounds();
        OldLocalBounds = OriginalShape.GetLocalBounds();

        HasChanges = false;
    }
}