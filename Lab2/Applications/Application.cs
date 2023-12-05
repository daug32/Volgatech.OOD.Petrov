using Lab2.Dictionaries;
using Lab2.Extensions;
using Lab2.Handlers;
using Lab2.Handlers.Grouping;
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
    private readonly HashSet<Shape> _shapes = new();

    private readonly DragAndDropHandler _dragAndDropHandler = new();
    private readonly ShapeGroupsHandler _shapeGroupsHandler = new();
    private readonly SelectedShapesHandler _selectedShapesHandler = new();

    public Application() : base( new VideoMode( 800, 600 ), "Lab2 - Composite.DragAndDrop" )
    {
        _shapes.Add( new RectangleShape( new Vector2f( 20, 20 ) )
            .FluentSetFillColor( Color.Black )
            .FluentSetPosition( Vector2FUtils.GetRandomInBounds( Window.Size ) ) );

        _shapes.Add( new RectangleShape( new Vector2f( 20, 20 ) )
            .FluentSetFillColor( Color.Black )
            .FluentSetPosition( Vector2FUtils.GetRandomInBounds( Window.Size ) ) );

        _shapes.Add( new RectangleShape( new Vector2f( 20, 20 ) )
            .FluentSetFillColor( Color.Black )
            .FluentSetPosition( Vector2FUtils.GetRandomInBounds( Window.Size ) ) );

        _shapes.Add( new CircleShape( 35 )
            .FluentSetFillColor( Color.Black )
            .FluentSetPosition( Vector2FUtils.GetRandomInBounds( Window.Size ) ) );

        Window.KeyPressed += OnKeyPressed;
        Window.MouseButtonPressed += OnMouseButtonPressed;
        Window.MouseButtonReleased += OnMouseButtonReleased;
    }

    protected override void Draw()
    {
        Window.Clear( CustomColors.Gray );

        _dragAndDropHandler.Update( _selectedShapesHandler.GetSelected() );

        foreach ( Shape shape in _shapes.ToList() )
        {
            Window.Draw( shape );

            FloatRect shapeBounds = shape.GetGlobalBounds();

            if ( _selectedShapesHandler.IsSelected( shape ) )
            {
                Window.Draw( _selectedShapesHandler.BuildSelectionMark( shapeBounds ) );
            }

            if ( _shapeGroupsHandler.HasGroup( shape ) )
            {
                Window.Draw( _shapeGroupsHandler.BuildGroupMark( shapeBounds ) );
            }
        } 
    }

    private void OnKeyPressed( object? sender, KeyEventArgs keyEventArgs )
    {
        switch ( keyEventArgs.Code )
        {
            case Keyboard.Key.G when Keyboard.IsKeyPressed( Keyboard.Key.LControl ):
            {
                var selectedItems = _selectedShapesHandler.GetSelected();
                _shapeGroupsHandler.Group( selectedItems );
                break;
            }
            case Keyboard.Key.U when Keyboard.IsKeyPressed( Keyboard.Key.LControl ):
            {
                var selectedItems = _selectedShapesHandler.GetSelected();
                _shapeGroupsHandler.Ungroup( selectedItems );
                break;
            }
        }
    }

    private void OnMouseButtonPressed( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        var shapes = _shapes.ToList();

        _selectedShapesHandler.OnMousePressed( shapes, mouseEventArgs, IsMultipleSelectionAllowed() );
        _dragAndDropHandler.OnMousePressed( shapes, mouseEventArgs );
    }

    private void OnMouseButtonReleased( object? sender, MouseButtonEventArgs mouseEventArgs )
    {
        _dragAndDropHandler.OnMouseReleased();
    }

    private bool IsMultipleSelectionAllowed() => 
        Keyboard.IsKeyPressed( Keyboard.Key.LShift ) || 
        Keyboard.IsKeyPressed( Keyboard.Key.RShift );
}