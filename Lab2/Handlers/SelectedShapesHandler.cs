using Lab2.Extensions;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2.Handlers;

public class SelectedShapesHandler
{
    private readonly HashSet<Shape> _shapes = new();

    public bool IsSelected( Shape shape ) => _shapes.Contains( shape );

    public List<Shape> GetSelected() => _shapes.ToList();

    public void OnMousePressed(
        IEnumerable<Shape> shapes,
        MouseButtonEventArgs mouseEventArgs,
        bool isMultipleSelectionAllowed )
    {
        if ( mouseEventArgs.Button != Mouse.Button.Left )
        {
            return;
        }

        Shape? clickedShape = FindSelected( shapes, mouseEventArgs.X, mouseEventArgs.Y );

        if ( clickedShape is null )
        {
            // Если никакой объект не был нажат и мы не можем выбрать несколько объектов, убрать все выделения
            if ( !isMultipleSelectionAllowed )
            {
                _shapes.Clear();
            }

            return;
        }

        if ( _shapes.Contains( clickedShape ) )
        {
            // Если объект уже был выбран и мы выбираем несколько объектов, снимаем выделение
            if ( isMultipleSelectionAllowed )
            {
                _shapes.Remove( clickedShape );
            }

            return;
        }

        // Если объект еще не был выбран и мы не можем выбрать несколько объектов, снимаем выделения
        if ( !isMultipleSelectionAllowed )
        {
            _shapes.Clear();
            _shapes.Add( clickedShape );
            return;
        }

        // Если объект еще не был выбран и при этом мы можем выбрать несколько объектов, выделяем новый объект
        _shapes.Add( clickedShape );
    }

    public Shape BuildSelectionMark( FloatRect shapeBounds )
    {
        return new RectangleShape( new Vector2f( shapeBounds.Width, shapeBounds.Height ) )
            .FluentSetPosition( shapeBounds.Left, shapeBounds.Top )
            .FluentSetOutlineColor( Color.White )
            .FluentSetFillColor( Color.Transparent )
            .FluentSetOutlineThickness( 1 );
    }

    private static Shape? FindSelected( IEnumerable<Shape> shapes, float mouseX, float mouseY ) => 
        shapes.LastOrDefault( shape => shape
            .GetGlobalBounds()
            .Contains( mouseX, mouseY ) );
}