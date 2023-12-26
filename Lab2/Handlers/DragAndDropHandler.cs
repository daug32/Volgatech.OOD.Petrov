using Libs.SFML.Shapes;
using Libs.SFML.Vertices;
using SFML.System;
using SFML.Window;

namespace Lab2.Handlers;

public class DragAndDropHandler
{
    private Vector2i _prevMousePosition;
    private bool _isAnyShapePressed;

    public bool IsDragAndDropping { get; private set; }

    public void OnMousePressed( ShapeDecorator? clickedShape )
    {
        _isAnyShapePressed = clickedShape != null;
    }

    public void OnMouseReleased()
    {
        _isAnyShapePressed = false;
    }

    public void Update( IEnumerable<ShapeDecorator> shapesToMove )
    {
        Vector2i currentMousePosition = Mouse.GetPosition();

        var mouseDeltaPosition = ( Vector2f )( currentMousePosition - _prevMousePosition );
        float mouseMovement = currentMousePosition.GetSquareDistance( _prevMousePosition );

        IsDragAndDropping = mouseMovement > 0 && _isAnyShapePressed;

        _prevMousePosition = currentMousePosition;

        if ( IsDragAndDropping )
        {
            foreach ( ShapeDecorator shape in shapesToMove )
            {
                shape.Position += mouseDeltaPosition;
            }
        }
    }
}