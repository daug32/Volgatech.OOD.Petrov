using System.Net;
using Lab2.Extensions;
using Lab2.Models;
using Lab2.Models.Dictionaries;
using Libs.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Lab2.Handlers;

public class SelectedShapesHandler
{
    private readonly HashSet<CashedShape> _selectedShapes = new();

    // Pseudo selected shapes are shapes that were selected by group relationships
    // These shapes are not counted as selected but they still get some handling such as marking at rendering and drag and droping 
    private readonly HashSet<CashedShape> _pseudoSelectedShapes = new();

    public bool IsSelected( CashedShape shape ) => 
        _selectedShapes.Contains( shape ) || 
        _pseudoSelectedShapes.Contains( shape );

    public List<CashedShape> GetSelected() => _selectedShapes
        .Union( _pseudoSelectedShapes )
        .ToList();

    public void OnMousePressed(
        CashedShape? clickedShape,
        List<CashedShape> relatedShapes,
        bool isMultipleSelectionAllowed )
    {
        // If clicked on the empty space
        if ( clickedShape is null )
        {
            // If can't multiselect, then clear all selections
            if ( !isMultipleSelectionAllowed )
            {
                _selectedShapes.Clear();
                _pseudoSelectedShapes.Clear();
            }

            return;
        }

        // If shape is already selected
        if ( _selectedShapes.Contains( clickedShape ) )
        {
            // Only when multiselection is allowed, mark shape as unselected 
            if ( isMultipleSelectionAllowed )
            {
                _selectedShapes.Remove( clickedShape );
            }

            return;
        }

        // If shape is not selected yet 

        // If mutliselection is not allowed, then clear selections 
        if ( !isMultipleSelectionAllowed )
        {
            _selectedShapes.Clear();
            _pseudoSelectedShapes.Clear();
        }

        // Mark shape as selected
        MarkAsTrueSelected( clickedShape );
        MarkAsPseudoSelectedIfNeed( relatedShapes );
    }

    public CashedShape? BuildSelectionMarkIfSelected( CashedShape cashedShape )
    {
        bool isTrueSelected = _selectedShapes.Contains( cashedShape );
        bool isPseudoSelected = _pseudoSelectedShapes.Contains( cashedShape );

        if ( !isTrueSelected && !isPseudoSelected )
        {
            return null;
        }
        
        FloatRect shapeBounds = cashedShape.GetGlobalBounds();

        var size = new Vector2f( shapeBounds.Width, shapeBounds.Height );

        Color outlineColor = isTrueSelected
            ? Color.White
            : CustomColors.LightGray;
        
        return CashedShape.Create( new RectangleShape( size )
            .FluentSetPosition( shapeBounds.Left, shapeBounds.Top )
            .FluentSetOutlineColor( outlineColor )
            .FluentSetFillColor( Color.Transparent )
            .FluentSetOutlineThickness( 1 ) );
    }

    private void MarkAsTrueSelected( CashedShape shape )
    {
        _selectedShapes.Add( shape );
    }

    private void MarkAsPseudoSelectedIfNeed( IEnumerable<CashedShape> shapes )
    {
        foreach ( CashedShape shape in shapes )
        {
            // Shape can't be pseudoselected if it's already true selected
            if ( _selectedShapes.Contains( shape ) )
            {
                continue;
            }

            _pseudoSelectedShapes.Add( shape );
        }
    }
}