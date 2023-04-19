using System;

public interface IRoom
{
    int X { get; }
    int Y { get; }
    void Enter();
    void Exit();

    /// <summary>
    /// Check if there is a connection on that direction.<br />
    /// Input:<br/>
    /// Up:     0x01
    /// Right:  0x02
    /// Down:   0x04
    /// Left:   0x08
    /// </summary>
    /// <param name="direction"></param>
    /// <returns>true if exist a connection</returns>
    bool HasConnectionTo(int direction);
}
