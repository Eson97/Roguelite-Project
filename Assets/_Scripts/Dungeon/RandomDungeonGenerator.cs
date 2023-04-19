using UnityEngine;
using static MinimapRoomController;

public class RandomDungeonGenerator
{
    private const int MAX_STEPS = 3;
    private const int MAX_X = 5;
    private const int MAX_Y = 5;
    private const int EMPTY_CELLS = 10;


    private DungeonBuilder _builder;
    private Dungeon _dungeon;

    public RandomDungeonGenerator(Dungeon dungeon)
    {
        _dungeon = dungeon;

        _builder = new DungeonBuilder();
        _builder.SetClamp(MAX_X, MAX_Y);

        _builder.forwardDelegate = (pos, lastPos, turn, invTurn) =>
            _dungeon.Add(pos,lastPos,turn, invTurn);
    }

    private void Restart()
    {
        _dungeon.Clear();
        _dungeon.Fill(MAX_X, MAX_Y);
        _dungeon.SetRoomType(_builder.Pos, (int)RoomType.Start);
    }

    private void UpdateVisuals() => _dungeon.IterateRect();

    public void Start()
    {
        Restart();
        while (_dungeon.GetEmptyCount() > EMPTY_CELLS)
        {
            //puede que repita giro:
            _builder.TurnTo(Random.Range(0, 4));
            //no repetir giro:
            //turtle.AddTurn(Random.Range(1, 4));
            //girar siempre:
            //turtle.AddTurn(1 + 2 * Random.Range(1, 3));

            _builder.Forward(Random.Range(1, MAX_STEPS + 1));
        }
        _dungeon.SetRoomType(_builder.Pos, (int)RoomType.Boss);
        UpdateVisuals();
    }
}
