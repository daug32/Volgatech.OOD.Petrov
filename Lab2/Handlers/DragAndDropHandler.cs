using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2.Handlers;

public class DragAndDropHandler
{
    private bool _isDragAndDropping;
    private Vector2i _prevMousePosition;

    public void OnMousePressed( IEnumerable<Shape> shapes, MouseButtonEventArgs mouseEventArgs )
    {
        if ( mouseEventArgs.Button != Mouse.Button.Left )
        {
            return;
        }

        bool isAnyShapePressed = shapes.Any( shape => shape
            .GetGlobalBounds()
            .Contains( mouseEventArgs.X, mouseEventArgs.Y ) );

        _isDragAndDropping = isAnyShapePressed;
    }

    public void OnMouseReleased() => _isDragAndDropping = false;

    public void Update( IEnumerable<Shape> shapesToMove )
    {
        Vector2i currentMousePosition = Mouse.GetPosition();

        if ( _isDragAndDropping )
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