using SFML.Graphics;

namespace Lab2.States.Handlers;

public interface IIterableStateHandler
{
    void MoveToNextValue();
    Drawable GetStateDescription();
}