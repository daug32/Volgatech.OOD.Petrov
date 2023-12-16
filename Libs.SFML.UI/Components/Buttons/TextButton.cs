using SFML.Graphics;
using SFML.System;

namespace Libs.SFML.UI.Components.Buttons;

public class TextButton : IButton
{
    private readonly Text _text;
    private readonly IButton _button;

    public int CharacterSize
    {
        get => ( int )_text.CharacterSize;
        set => _text.CharacterSize = ( uint )value;
    }

    public Vector2f Size
    {
        get => _button.Size;
        set => _button.Size = value + Padding * 2;
    }

    public Vector2f Position
    {
        get => _button.Position;
        set
        {
            _text.Position = value + Padding;
            FloatRect textBounds = _text.GetGlobalBounds();
            _button.Position = new Vector2f( textBounds.Left, textBounds.Top ) - Padding;
        }
    }

    public Vector2f Padding { get; set; }

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

    public TextButton( Vector2f position, Text text, Action onClick )
    {
        _text = new Text( text );
        _button = new Button( position, new Vector2f(), onClick );
        
        Color = Color.White;
        BackgroundColor = Color.Black;
        BorderColor = Color.Transparent;
        BorderThickness = 0;
        Padding = new Vector2f(5, 5);

        Position = position;
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
}