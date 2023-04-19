using ExtensionMethods;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidFunc3Int(int x, int y, int value);
public class Dungeon
{
    private Dictionary<(int, int), int> maze;

    private int maxX = 0;
    private int maxY = 0;

    public Dictionary<(int, int), int> Maze => maze;

    public VoidFunc3Int iteratorDelegate;

    public Dungeon()
    {
        maze = new Dictionary<(int, int), int>();
        iteratorDelegate = (x, y, v) => { };
    }

    public void Clear() => 
        maze.Clear();
    
    public int GetValueAt(int x, int y) => 
        maze[(x, y)];
    public int GetValueAt(float x, float y) => 
        maze[((int)x, (int)y)];
    public int GetValueAt(Vector3 v) => 
        maze[((int)v.x, (int)v.y)];

    public int SetValueAt(int x, int y, int value) => 
        maze[(x, y)] = value;
    public int SetValueAt(float x, float y, int value) => 
        maze[((int)x, (int)y)] = value;
    public int SetValueAt(Vector3 v, int value) => 
        maze[((int)v.x, (int)v.y)] = value;

    public int CombineValueAt(int x, int y, int value) =>
        maze[(x, y)] |= value;
    public int CombineValueAt(float x, float y, int value) =>
        maze[((int)x, (int)y)] |= value;
    public int CombineValueAt(Vector3 v, int value) =>
        maze[((int)v.x, (int)v.y)] |= value;

    public void Fill(int maxX,int maxY,int value = 0)
    {
        this.maxX = maxX;
        this.maxY = maxY;
        
        for(int y = 0; y < maxY; y++)
            for(int x = 0; x < maxX; x++)
                maze[(x, y)] = value;

        //mark walls and shift 4 bits
        //top: 1, right: 2, bottom: 4, left: 8
        for(int y = -1; y < maxY; y++)
        {
            maze[(-1, y)] = 15;//left
            maze[(maxX, y)] = 15;//right
        }
        for(int x = -1; x < maxX; x++)
        {
            maze[(x, maxY)] = 15;//top
            maze[(x, -1)] = 15;//bottom
        }
    }

    public void Add(Vector3 pos, Vector3 lastPos, int turn, int invTurn)
    {
        CombineValueAt(pos, 1 << invTurn);
        CombineValueAt(lastPos, 1 << turn);
    }

    /// <summary>
    /// Save room type in the second group of bits (0000_0000 => roomTyipe_connections)
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="RoomId"></param>
    public void SetRoomType(Vector3 pos, int RoomId)
    {
        int value = GetValueAt(pos) & 0x0F;
        SetValueAt(pos, (RoomId << 4) | value);
    }
    /// <summary>
    /// Save room type in the second group of bits (0000_0000 => roomTyipe_connections)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="RoomId"></param>
    public void SetRoomType(int x, int y, int RoomId)
    {
        int value = GetValueAt(x,y) & 0x0F;
        SetValueAt(x, y, (RoomId << 4) | value);
    }

    public void Iterate()
    {
        foreach (var m in maze)
            iteratorDelegate(m.Key.Item1, m.Key.Item2, m.Value);
    }

    public void IterateRect()
    {
        for (int y = 0; y < maxY; y++)
            for (int x = 0; x < maxX; x++)
                iteratorDelegate(x, y, maze[(x, y)]);
    }

    public int GetEmptyCount()
    {
        int count = 0;
        foreach (var m in maze)
            if ((m.Value & 0x0F) == 0) count++;

        return count;
    }

    public bool isFull() => GetEmptyCount() == 0;

    public int GetNeighborsAt(int x, int y)
    {
        int result = 0;
        result |= maze[(x    , y + 1)].bit();       //top
        result |= maze[(x + 1, y    )].bit() << 1;  //right
        result |= maze[(x    , y - 1)].bit() << 2;  //bottom
        result |= maze[(x - 1, y    )].bit() << 3;  //left

        return result;
    }

    /// <summary>
    ///  Number of connected neighbors of this room
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>Total of neighbors connected</returns>
    public int GetNumberOfConnectedNeighborsAt(int x,int y) 
    {
        var totalNeighbors = 0;
        var neighbors = maze[(x, y)];

        if ((neighbors & 0x01) != 0) totalNeighbors++; //top
        if ((neighbors & 0x02) != 0) totalNeighbors++; //right
        if ((neighbors & 0x04) != 0) totalNeighbors++; //down
        if ((neighbors & 0x08) != 0) totalNeighbors++; //left

        return totalNeighbors;
    }

    public int GetNeighborsAt(float x, float y) => GetNeighborsAt((int)x, (int)y);
    public int GetNeighborsAt(Vector3 v) => GetNeighborsAt((int)v.x, (int)v.y);

}
