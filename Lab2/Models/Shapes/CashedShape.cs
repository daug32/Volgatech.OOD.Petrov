using SFML.Graphics;
using SFML.System;

namespace Lab2.Models;

public class CashedShape : Drawable
{
    private readonly Shape _shape;
    private bool _hasChanges = true;
    private FloatRect _oldGlobalBounds;
    private FloatRect _oldLocalBounds;

    public Shape ToShape() => _shape;
    
    public static CashedShape Create( Shape shape ) => new( shape );

    private CashedShape( Shape shape ) => _shape = shape;

    public Color FillColor
    {
        get => _shape.FillColor;
        set
        {
            _hasChanges = true;
            _shape.FillColor = value;
        }
    }

    public Color OutlineColor
    {
        get => _shape.OutlineColor;
        set
        {
            _hasChanges = true;
            _shape.OutlineColor = value;
        }
    }

    public float OutlineThickness
    {
        get => _shape.OutlineThickness;
        set
        {
            _hasChanges = true;
            _shape.OutlineThickness = value;
        }
    }
    
    public Vector2f Position
    {
        get => _shape.Position;
        set
        {
            _hasChanges = true;
            _shape.Position = value;
        }
    }

    public float Rotation
    {
        get => _shape.Rotation;
        set
        {
            _hasChanges = true;
            _shape.Rotation = value;
        }
    }

    public Vector2f Scale
    {
        get => _shape.Scale;
        set
        {
            _hasChanges = true;
            _shape.Scale = value;
        }
    }

    public Vector2f Origin
    {
        get => _shape.Origin;
        set
        {
            _hasChanges = true;
            _shape.Origin = value;
        }
    }

    public FloatRect GetLocalBounds()
    {
        RecalculateIfNeed();
        return _oldLocalBounds;
    }

    public FloatRect GetGlobalBounds()
    {
        RecalculateIfNeed();
        return _oldGlobalBounds;
    }

    public void Draw( RenderTarget target, RenderStates states ) => _shape.Draw( target, states );

    private void RecalculateIfNeed()
    {
        if ( !_hasChanges )
        {
            return;
        }
        
        _oldGlobalBounds = _shape.GetGlobalBounds();
        _oldLocalBounds = _shape.GetLocalBounds();
        
        _hasChanges = false;
    }
}