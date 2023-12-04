using Lab2.Models;
using Lab2.Models.Extensions;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2.Handlers;

public class SelectedShapesHandler
{
    private readonly ShapesGroup _shapesGroup = new();
    
    public bool IsSelected( Shape shape ) => _shapesGroup.Contains( shape );
    
    public void UpdateSelections( 
        IEnumerable<Shape> shapes,
        MouseButtonEventArgs mouseEventArgs )
    {
        if ( mouseEventArgs.Button != Mouse.Button.Left )
        {
            return;
        }

        Shape? clickedShape = shapes.LastOrDefault( shape => shape
            .GetGlobalBounds()
            .Contains( mouseEventArgs.X, mouseEventArgs.Y ) );

        // Если никакой объект не был нажат, убрать все выделения
        if ( clickedShape is null )
        {
            _shapesGroup.Clear();
            return;
        }
        
        bool isMultipleSelectionAllowed = 
            Keyboard.IsKeyPressed( Keyboard.Key.LShift ) || 
            Keyboard.IsKeyPressed( Keyboard.Key.RShift );

        // Если уже выделенный объект нажат второй раз, убрать выделение
        if ( _shapesGroup.Contains( clickedShape ) )
        { 
            bool needToUnselectAllExceptForCurrent = _shapesGroup.Count > 1;
            _shapesGroup.Clear();
            if ( needToUnselectAllExceptForCurrent )
            {
                _shapesGroup.Add( clickedShape );
            }

            return;
        }

        // Если нельзя выделить несколько объектов за раз, нужно убрать предыдущие выделения
        if ( !isMultipleSelectionAllowed )
        {
            _shapesGroup.Clear();
        }

        _shapesGroup.Add( clickedShape );
    }

    public List<Shape> GetSelected() => _shapesGroup.ToList();

    public Shape BuildSelectionMark( Shape shape )
    {
        FloatRect bounds = shape.GetGlobalBounds();

        return new RectangleShape( new Vector2f( bounds.Width, bounds.Height ) )
            .FluentSetPosition( bounds.Left, bounds.Top )
            .FluentSetOutlineColor( Color.White )
            .FluentSetFillColor( Color.Transparent )
            .FluentSetOutlineThickness( 1 );
    }
}