using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.UI.Components.Buttons;

public class Button : IButton
{
    private readonly Action<IButton> _onClick;
    private readonly RectangleShape _background;

    public Vector2f Size
    {
        get => _background.Size;
        set => _background.Size = value;
    }

    public Vector2f Position
    {
        get => _background.Position;
        set => _background.Position = value;
    }

    public Color BackgroundColor
    {
        get => _background.FillColor;
        set => _background.FillColor = value;
    }

    public Color BorderColor
    {
        get => _background.OutlineColor;
        set => _background.OutlineColor = value;
    }
    
    public float BorderThickness
    {
        get => _background.OutlineThickness;
        set => _background.OutlineThickness = value;
    }

    public Button(
        Action<IButton> onClick,
        ButtonViewParams? viewParams = null )
    {
        viewParams ??= new ButtonViewParams();

        _onClick = onClick;
        _background = new RectangleShape( viewParams.Size );

        Position = viewParams.Position;
        BackgroundColor = viewParams.BackgroundColor;
        BorderColor = viewParams.BorderColor;
        BorderThickness = viewParams.BorderThickness;
    }

    public void Execute() => _onClick.Invoke( this );

    public FloatRect GetGlobalBounds()
    {
        return _background.GetGlobalBounds();
    }

    public void Draw( RenderTarget target, RenderStates states ) => _background.Draw( target, states );
}