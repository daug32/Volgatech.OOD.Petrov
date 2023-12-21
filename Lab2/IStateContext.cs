using Lab2.States;

namespace Lab2;

public interface IStateContext
{
    State CurrentState { get; }
}