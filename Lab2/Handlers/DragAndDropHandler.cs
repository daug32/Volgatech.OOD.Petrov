using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2.Handlers;

public class DragAndDropHandler
{
    private Vector2i _prevMousePosition = new();
    public bool IsDragAndDropping { get; private set; } = false;

    public void OnMousePressed( IEnumerable<Shape> shapes, MouseButtonEventArgs mouseEventArgs )
    {
        if ( mouseEventArgs.Button != Mouse.Button.Left )
        {
            return;
        }

        bool isAnyShapePressed = shapes.Any( shape => shape
            .GetGlobalBounds()
            .Contains( mouseEventArgs.X, mouseEventArgs.Y ) );

        IsDragAndDropping = isAnyShapePressed;
    }

    public void OnMouseReleased()
    {
        Vector2i currentMousePosition = Mouse.GetPosition();
        if ( currentMousePosition == _prevMousePosition )
        {
            IsDragAndDropping = false;   
        }
    }

    public void Update( IEnumerable<Shape> shapesToMove )
    {
        Vector2i currentMousePosition = Mouse.GetPosition();

        if ( IsDragAndDropping )
        {
            var mouseDeltaPosition = ( Vector2f )( currentMousePosition - _prevMousePosition );

            foreach ( Shape shape in shapesToMove )
            {
                shape.Position += mouseDeltaPosition;
            }
        }
        
        _prevMousePosition = currentMousePosition;
    }
}