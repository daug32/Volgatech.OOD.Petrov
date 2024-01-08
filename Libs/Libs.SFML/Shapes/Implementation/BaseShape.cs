using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.Shapes.Implementation;

public abstract class BaseShape : IShape
{
    private FloatRect _oldGlobalBounds;
    private FloatRect _oldLocalBounds;
    
    protected readonly Shape Shape;
    protected bool HasChanges = true;

    protected BaseShape( Shape shape )
    {
        Shape = shape;
        Shape.OutlineColor = Color.Black;
        Shape.OutlineThickness = 0;
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
        return _oldLocalBounds;
    }

    public virtual FloatRect GetGlobalBounds()
    {
        RecalculateIfNeed();
        return _oldGlobalBounds;
    }

    public abstract void AcceptVisitor( IShapeVisitor visitor );

    public virtual void Draw( RenderTarget target, RenderStates states )
    {
        Shape.Draw( target, states );
    }

    protected virtual void RecalculateIfNeed()
    {
        if ( !HasChanges )
        {
            return;
        }

        _oldGlobalBounds = Shape.GetGlobalBounds();
        _oldLocalBounds = Shape.GetLocalBounds();

        HasChanges = false;
    }
}