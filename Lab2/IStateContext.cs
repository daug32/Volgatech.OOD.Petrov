using Lab2.Handlers.States;

namespace Lab2;

public interface IStateContext
{
    State CurrentState { get; }
}