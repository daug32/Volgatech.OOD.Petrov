using Lab2.Models;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Lab2.Handlers;

public class DragAndDropHandler
{
    private bool _isDragAndDropping;
    private Vector2i _prevMousePosition;

    public void OnMousePressed( CashedShape? clickedShape )
    {
        bool isAnyShapePressed = clickedShape != null;
        _isDragAndDropping = isAnyShapePressed;
    }

    public void OnMouseReleased() => _isDragAndDropping = false;

    public void Update( IEnumerable<CashedShape> shapesToMove )
    {
        Vector2i currentMousePosition = Mouse.GetPosition();

        if ( _isDragAndDropping )
        {
            var mouseDeltaPosition = ( Vector2f )( currentMousePosition - _prevMousePosition );

            foreach ( CashedShape shape in shapesToMove )
            {
                shape.Position += mouseDeltaPosition;
            }
        }
        
        _prevMousePosition = currentMousePosition;
    }
}