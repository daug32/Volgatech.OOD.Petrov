using Lab2.Public;
using Lab2.States;
using Libs.SFML.Colors;
using Libs.SFML.UI.Components.Buttons;
using Libs.SFML.UI.Components.Menus;
using SFML.Graphics;
using SFML.System;

namespace Lab2.UI;

public class Toolbar : Menu
{
    public event EventHandler<State>? StateSwitched;
    
    public Toolbar( Vector2f windowSize ) : base( new Vector2f( windowSize.X, 50 ) )
    {
        BackgroundColor = CustomColors.Purple;
        var buttons = BuildButtons();
        AddButtons( buttons );
    }

    private List<IButton> BuildButtons()
    {
        var buttonViewParams = new TextButtonViewParams
        {
            Color = CustomColors.Purple,
            BackgroundColor = Color.White,
            Padding = new Vector2f( 5, 5 ),
            MinHeight = 25
        };

        var buttons = new List<IButton>
        {
            new TextButton(
                onClick: _ => StateSwitched?.Invoke( this, State.AddShape ),
                text: new Text( "Add shape", Resources.Fonts.Roboto, 14 ),
                viewParams: buttonViewParams ),
            new TextButton(
                onClick: button => Console.WriteLine( $"Change fill color, {button.GetGlobalBounds()}" ),
                text: new Text( "Fill color", Resources.Fonts.Roboto, 14 ),
                viewParams: buttonViewParams ),
            new TextButton(
                onClick: button => Console.WriteLine( $"Change border color, {button.GetGlobalBounds()}" ),
                text: new Text( "Border color", Resources.Fonts.Roboto, 14 ),
                viewParams: buttonViewParams ),
            new TextButton(
                onClick: button => Console.WriteLine( $"Change border size, {button.GetGlobalBounds()}" ),
                text: new Text( "Border size", Resources.Fonts.Roboto, 14 ),
                viewParams: buttonViewParams ),
            new TextButton(
                onClick: _ => StateSwitched?.Invoke( this, State.Default ),
                text: new Text( "D&D", Resources.Fonts.Roboto, 14 ),
                viewParams: buttonViewParams ),
            new TextButton(
                onClick: button => Console.WriteLine( $"Undo, {button.GetGlobalBounds()}" ),
                text: new Text( "Undo", Resources.Fonts.Roboto, 14 ),
                viewParams: buttonViewParams ),
            new TextButton(
                onClick: button => Console.WriteLine( $"Redo, {button.GetGlobalBounds()}" ),
                text: new Text( "Redo", Resources.Fonts.Roboto, 14 ),
                viewParams: buttonViewParams )
        };
        
        AlignButtonsInRow( buttons );

        return buttons;
    }

    private static void AlignButtonsInRow( List<IButton> buttons )
    {
        int spaceBetweenButtons = 20;
        float positionY = 10;
        float positionXOffset = spaceBetweenButtons;

        foreach ( IButton button in buttons )
        {
            button.Position = new Vector2f( button.Position.X + positionXOffset, positionY );
            positionXOffset += button.Size.X + spaceBetweenButtons;
        }
    }
}