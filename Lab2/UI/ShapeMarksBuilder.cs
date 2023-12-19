using Lab2.Handlers.Selection;
using Lab2.Public;
using Libs.SFML.Colors;
using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Lab2.UI;

public static class ShapeMarksBuilder
{
    private static readonly Color _selectionMark = CustomColors.Gray; 
    
    public static List<Drawable> Build( 
        SelectionType selectionType,
        bool isGrouped,
        FloatRect shapeBounds )
    {
        var result = new List<Drawable>();
        
        if ( selectionType != SelectionType.NotSelected )
        {
            result.Add( BuildSelectionMark( selectionType, shapeBounds ) );
        }

        if ( isGrouped )
        {
            result.Add( BuildGroupMark( shapeBounds ) );
        }

        return result;
    }
    
    private static Drawable BuildSelectionMark( SelectionType selectionType, FloatRect shapeBounds )
    {
        var markSize = new Vector2f( shapeBounds.Width, shapeBounds.Height );
        Color markOutlineColor = selectionType == SelectionType.TrueSelection
            ? _selectionMark
            : _selectionMark.SetAlpha( 80 );

        return CashedShape.Create( new RectangleShape( markSize ) )
            .FluentSetPosition( shapeBounds.Left, shapeBounds.Top )
            .FluentSetOutlineColor( markOutlineColor )
            .FluentSetFillColor( Color.Transparent )
            .FluentSetOutlineThickness( 1 );
    }
    
    private static Drawable BuildGroupMark( FloatRect shapeBounds )
    {
        uint textSize = 10;
        
        var text = new Text( "Group", Resources.Fonts.Roboto );
        text.Position = new Vector2f( shapeBounds.Left - textSize, shapeBounds.Top - textSize );
        text.CharacterSize = textSize;
        text.FillColor = CustomColors.Gray;

        return text;
    }
}