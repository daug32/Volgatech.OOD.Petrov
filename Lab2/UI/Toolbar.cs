using Lab2.Handlers.States;
using Lab2.Public;
using Libs.SFML.Colors;
using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using Libs.SFML.UI.Components.Buttons;
using Libs.SFML.UI.Components.Menus;
using SFML.Graphics;
using SFML.System;

namespace Lab2.UI;

public class Toolbar : Menu
{
    private readonly Vector2f _maxUiItemSize = new( 20, 20 );
    private const int SpaceBetweenUiItems = 20;

    // State description
    private const string StateDescriptionMenuKey = "StateDescription";
    private const string StateDescriptionBackgroundMenuKey = "StateDescriptionBackground";
    
    public event EventHandler<State>? StateSwitched;

    public Toolbar( Vector2f windowSize ) : base( new Vector2f( windowSize.X, 50 ) )
    {
        BackgroundColor = CustomColors.Purple;
        AddButtons( BuildButtons() );
    }

    public void SetStateDescription( ShapeDecorator? descriptionContent )
    {
        if ( descriptionContent is null )
        {
            RemoveItem( StateDescriptionMenuKey );
            RemoveItem( StateDescriptionBackgroundMenuKey );
            return;
        }
        
        FloatRect toolbarSize = GetGlobalBounds();

        var stateDescriptionPosition = new Vector2f( 
            toolbarSize.Width - _maxUiItemSize.X - SpaceBetweenUiItems,
            toolbarSize.Height / 2 - _maxUiItemSize.Y / 2 );
        
        // State description background
        ShapeDecorator descriptionBackground = BuildStateDescriptionBackground();
        descriptionBackground.SetPosition( stateDescriptionPosition );

        AddOrReplaceItem( StateDescriptionBackgroundMenuKey, descriptionBackground );

        // State description content
        descriptionContent
            .SetMaxSize( _maxUiItemSize )
            .SetPosition( stateDescriptionPosition )
            .SetFillColor( CustomColors.Purple );

        AddOrReplaceItem( StateDescriptionMenuKey, descriptionContent );
    }

    private List<IButton> BuildButtons()
    {
        var buttonViewParams = new TextButtonViewParams
        {
            Color = CustomColors.Purple,
            BackgroundColor = Color.White,
            Padding = new Vector2f( 5, 5 ),
            MinHeight = 24
        };

        var buttons = new List<IButton>
        {
            new TextButton(
                onClick: _ => SwitchState( State.AddShape ),
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
                onClick: _ => SwitchState( State.Default ),
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

    private void AlignButtonsInRow( List<IButton> buttons )
    {
        float positionXOffset = SpaceBetweenUiItems;
        FloatRect toolbarSize = GetGlobalBounds();

        foreach ( IButton button in buttons )
        {
            button.Position = new Vector2f(
                button.Position.X + positionXOffset, 
                toolbarSize.Height / 2 - button.Size.Y / 2 );
            positionXOffset += button.Size.X + SpaceBetweenUiItems;
        }
    }

    private void SwitchState( State state )
    {
        StateSwitched?.Invoke( this, state );
    }

    private ShapeDecorator BuildStateDescriptionBackground()
    {
        var background = new ShapeDecorator( new RectangleShape( _maxUiItemSize ) );

        return background
            .SetFillColor( Color.White )
            .SetOutlineColor( Color.White )
            .SetOutlineThickness( 3 );
    }
}