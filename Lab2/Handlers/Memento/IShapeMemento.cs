using Libs.Memento;
using Libs.SFML.Shapes;
using SFML.Graphics;
using SFML.System;

namespace Lab2.Handlers.Memento;

public class ShapeMemento : IMemento 
{
    private readonly IShape _entityReference;

    public Vector2f? Position = null;
    public Color? FillColor = null;
    public Color? BorderColor = null;
    public float? BorderSize = null;

    public ShapeMemento( IShape shapeReference )
    {
        _entityReference = shapeReference;
    }

    public void Restore()
    {
        if ( Position is not null )
        {
            _entityReference.Position = Position.Value;
        }
        
        if ( FillColor is not null ) 
        {
            _entityReference.FillColor = FillColor.Value;
        }

        if ( BorderColor is not null ) 
        {
            _entityReference.OutlineColor = BorderColor.Value;
        }

        if ( BorderSize is not null ) 
        {
            _entityReference.OutlineThickness = BorderSize.Value;
        }
    }
}
