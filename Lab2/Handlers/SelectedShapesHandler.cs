using Lab2.Extensions;
using Libs.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Lab2.Handlers;

public class SelectedShapesHandler
{
    private readonly HashSet<Shape> _shapes = new();

    public bool IsSelected( Shape shape ) => _shapes.Contains( shape );

    public List<Shape> GetSelected() => _shapes.ToList();

    public void OnMousePressed(
        Shape? clickedShape,
        List<Shape> relatedShapes,
        bool isMultipleSelectionAllowed )
    {
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
        }

        // Если объект еще не был выбран и при этом мы можем выбрать несколько объектов, выделяем новый объект
        _shapes.Add( clickedShape );
        _shapes.AddRange( relatedShapes );
    }

    public Shape BuildSelectionMark( FloatRect shapeBounds )
    {
        return new RectangleShape( new Vector2f( shapeBounds.Width, shapeBounds.Height ) )
            .FluentSetPosition( shapeBounds.Left, shapeBounds.Top )
            .FluentSetOutlineColor( Color.White )
            .FluentSetFillColor( Color.Transparent )
            .FluentSetOutlineThickness( 1 );
    }
}