using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.UI.Components.Buttons;

public class TextButton : IButton
{
    private readonly Text _text;
    private readonly Button _button;

    public float? MinHeight { get; set; }

    public Vector2f Size
    {
        get => _button.Size;
        set => SetSize( value );
    }

    public Vector2f Position
    {
        get => _button.Position;
        set => SetPosition( value );
    }

    private Vector2f _padding;
    public Vector2f Padding
    {
        get => _padding;
        set => SetPadding( value );
    }

    public Color Color
    {
        get => _text.FillColor;
        set => _text.FillColor = value;
    }

    public Color BackgroundColor
    {
        get => _button.BackgroundColor;
        set => _button.BackgroundColor = value;
    }

    public Color BorderColor
    {
        get => _button.BorderColor;
        set => _button.BorderColor = value;
    }

    public float BorderThickness
    {
        get => _button.BorderThickness;
        set => _button.BorderThickness = value;
    }

    public TextButton(
        Action<IButton> onClick,
        Text text,
        TextButtonViewParams? viewParams = null )
    {
        viewParams ??= new TextButtonViewParams();

        _text = new Text( text );
        _button = new Button( onClick, viewParams );

        Color = viewParams.Color;
        Padding = viewParams.Padding;
        MinHeight = viewParams.MinHeight;
        Position = viewParams.Position;

        FloatRect textBounds = _text.GetGlobalBounds();
        Size = new Vector2f( textBounds.Width, textBounds.Height );
    }

    public void Execute()
    {
        _button.Execute();
    }

    public FloatRect GetGlobalBounds()
    {
        return _button.GetGlobalBounds();
    }

    public void Draw( RenderTarget target, RenderStates states )
    {
        _button.Draw( target, states );
        _text.Draw( target, states );
    }
    
    private void SetSize( Vector2f value )
    {
        float height = value.Y + Padding.Y * 2;
        if ( MinHeight.HasValue && height < MinHeight )
        {
            height = MinHeight.Value;
        }

        float width = value.X + Padding.X * 2;

        _button.Size = new Vector2f( width, height );
    }

    private void SetPadding( Vector2f value )
    {
        _padding = value;
        Position = Position;
    }
    
    private void SetPosition( Vector2f value )
    {
        _text.Position = value + Padding;
        FloatRect textBounds = _text.GetGlobalBounds();
        _button.Position = new Vector2f( textBounds.Left, textBounds.Top ) - Padding;
    }
}