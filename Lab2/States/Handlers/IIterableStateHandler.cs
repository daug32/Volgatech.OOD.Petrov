using SFML.Graphics;

namespace Lab2.States.Handlers;

public interface IIterableStateHandler : IStateHandler
{
    void MoveToNextValue();
    Drawable GetStateDescription();
}