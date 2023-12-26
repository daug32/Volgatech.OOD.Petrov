using Lab2.Handlers.Selection;
using Lab2.Models;
using Lab2.Public;
using Libs.SFML.Colors;
using Libs.SFML.Shapes;
using Libs.SFML.Shapes.Extensions;
using Libs.SFML.Shapes.Implementation;
using SFML.Graphics;
using SFML.System;

namespace Lab2.UI;

public class ShapeMarksBuilder
{
    private static readonly Color _selectionMark = CustomColors.Gray;

    private readonly ShapesContainer _shapesContainer;
    private readonly SelectionHandler _selectionHandler;

    public ShapeMarksBuilder( ShapesContainer shapesContainer, SelectionHandler selectionHandler )
    {
        _shapesContainer = shapesContainer;
        _selectionHandler = selectionHandler;
    }

    public List<Drawable> GetMarks( IShape baseShape )
    {
        FloatRect shapeBounds = baseShape.GetGlobalBounds();

        var result = new List<Drawable>();

        SelectionType selectionType = _selectionHandler.GetSelectionType( baseShape ); 
        if ( selectionType != SelectionType.NotSelected )
        {
            result.Add( BuildSelectionMark( selectionType, shapeBounds ) );
        }

        if ( _shapesContainer.HasGroup( baseShape ) )
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

        return new Rectangle( markSize )
            .SetPosition( shapeBounds.Left, shapeBounds.Top )
            .SetOutlineColor( markOutlineColor )
            .SetFillColor( Color.Transparent )
            .SetOutlineThickness( 1 );
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