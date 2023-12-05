using Lab2.Handlers;
using Lab2.Models;
using Lab2.Models.Extensions;
using Lab2.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

/*
Добавить функциональность, позволяющую выделить геометрическую фигуру при клике мышки на ее внутреннюю область. При 
выделении вокруг фигуры должна появляться прямоугольная рамка с маркерами. 
Реализовать функцию выделения нескольких фигур (Shift + Left Click), группировки(Ctrl+G)  в одну фигуру и разгруппировки (Ctrl+U). 
Для хранения всех фигур на области рисования и сгруппированной фигуры использовать паттерн Composite («Компоновшик»).
Реализовать функцию перемещения выделенной фигуры с помощью мыши («drag-and-drop»).
 */

namespace Lab2.Applications;

public class Application : BaseApplication
{
    private readonly ShapesGroup _shapeGroups = new();
    private readonly SelectedShapesHandler _selectedShapesHandler = new();
    private readonly DragAndDropHandler _dragAndDropHandler = new();

    public Application()
        : base( new VideoMode( 800, 600 ), "Lab2 - Composite.DragAndDrop" )
    {
        _shapeGroups.Add(
            new RectangleShape( new Vector2f( 20, 20 ) )
                .FluentSetFillColor( Color.Black )
                .FluentSetPosition( Vector2FUtils.GetRandomInBounds( Window.Size ) ) );

        _shapeGroups.Add(
            new RectangleShape( new Vector2f( 20, 20 ) )
                .FluentSetFillColor( Color.Black )
                .FluentSetPosition( Vector2FUtils.GetRandomInBounds( Window.Size ) ) );

        _shapeGroups.Add(
            new RectangleShape( new Vector2f( 20, 20 ) )
                .FluentSetFillColor( Color.Black )
                .FluentSetPosition( Vector2FUtils.GetRandomInBounds( Window.Size ) ) );

        _shapeGroups.Add(
            new CircleShape( 35 )
                .FluentSetFillColor( Color.Black )
                .FluentSetPosition( Vector2FUtils.GetRandomInBounds( Window.Size ) ) );

        Window.MouseButtonPressed += OnMouseButtonPressed;
        Window.MouseButtonReleased += OnMouseButtonReleased;
    }

    protected override void Draw()
    {
        Window.Clear( CustomColors.Gray );

        _dragAndDropHandler.Update( _selectedShapesHandler.GetSelected() );

        foreach ( Shape shape in _shapeGroups.ToList() )
        {
            Window.Draw( shape );

            if ( _selectedShapesHandler.IsSelected( shape ) )
            {
                Window.Draw( _selectedShapesHandler.BuildSelectionMark( shape ) );
            }
        }
    }

    private void OnMouseButtonPressed( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        var shapes = _shapeGroups.ToList();
        
        _selectedShapesHandler.OnMousePressed( shapes, mouseEventArgs );
        _dragAndDropHandler.OnMousePressed( shapes, mouseEventArgs );
    }

    private void OnMouseButtonReleased( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        _dragAndDropHandler.OnMouseReleased();
    }
}