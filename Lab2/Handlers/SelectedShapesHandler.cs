using Lab2.Models;
using Lab2.Models.Extensions;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2.Handlers;

public class SelectedShapesHandler
{
    private readonly ShapesGroup _shapesGroup = new();

    public bool IsSelected( Shape shape )
    {
        return _shapesGroup.Contains( shape );
    }

    public List<Shape> GetSelected()
    {
        return _shapesGroup.ToList();
    }

    public void OnMousePressed(
        IEnumerable<Shape> shapes,
        MouseButtonEventArgs mouseEventArgs )
    {
        if ( mouseEventArgs.Button != Mouse.Button.Left )
        {
            return;
        }

        Shape? clickedShape = FindSelected( shapes, mouseEventArgs.X, mouseEventArgs.Y );

        bool isMultipleSelectionAllowed = IsMultipleSelectionAllowed();

        if ( clickedShape is null )
        {
            // Если никакой объект не был нажат и мы не можем выбрать несколько объектов, убрать все выделения
            if ( !isMultipleSelectionAllowed )
            {
                _shapesGroup.Clear();
            }

            return;
        }

        if ( _shapesGroup.Contains( clickedShape ) )
        {
            // Если объект уже был выбран и мы выбираем несколько объектов, снимаем выделение
            if ( isMultipleSelectionAllowed )
            {
                _shapesGroup.Remove( clickedShape );
            }

            return;
        }

        // Если объект еще не был выбран и мы не можем выбрать несколько объектов, снимаем выделения
        if ( !isMultipleSelectionAllowed )
        {
            _shapesGroup.Clear();
            _shapesGroup.Add( clickedShape );
            return;
        }

        // Если объект еще не был выбран и при этом мы можем выбрать несколько объектов, выделяем новый объект
        _shapesGroup.Add( clickedShape );
    }
    
    public Shape BuildSelectionMark( Shape shape )
    {
        FloatRect bounds = shape.GetGlobalBounds();

        return new RectangleShape( new Vector2f( bounds.Width, bounds.Height ) )
            .FluentSetPosition( bounds.Left, bounds.Top )
            .FluentSetOutlineColor( Color.White )
            .FluentSetFillColor( Color.Transparent )
            .FluentSetOutlineThickness( 1 );
    }

    private static Shape? FindSelected( IEnumerable<Shape> shapes, float mouseX, float mouseY )
    {
        return shapes.LastOrDefault( shape => shape
            .GetGlobalBounds()
            .Contains( mouseX, mouseY ) );
    }

    private static bool IsMultipleSelectionAllowed()
    {
        return Keyboard.IsKeyPressed( Keyboard.Key.LShift ) || Keyboard.IsKeyPressed( Keyboard.Key.RShift );
    }
}