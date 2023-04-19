using System.Collections.Generic;
using UnityEngine;
using static MinimapRoomController;
using static MinimapRoomStorage;

public class DungeonManager : Singleton<DungeonManager>
{
    [SerializeField] private List<MinimapRoomSO> _minimapRoomsSO;
    [SerializeField] private GameObject _minimapRoomPrefab;

    [Header("Debug")]
    [SerializeField] private bool _moveTest;

    private Dungeon _dungeon;
    private RandomDungeonGenerator _dungeonGenerator;
    private MinimapRoomStorage _minimapRoomStorage;

    public IRoom CurrentMinimapRoom { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _minimapRoomStorage = new((gameObject) => Destroy(gameObject));

        _dungeon = new();
        _dungeon.iteratorDelegate = Dungeon_IteratorDelegate;

        _dungeonGenerator = new(_dungeon);
    }

    private void Start()
    {
        GenerateNewDungeon();
    }
    private void Update()
    {
        if (!_moveTest) return;
        if(Input.GetKeyDown(KeyCode.UpArrow)) MoveToUpRoom();
        if(Input.GetKeyDown(KeyCode.DownArrow)) MoveToDownRoom();
        if(Input.GetKeyDown(KeyCode.LeftArrow)) MoveToLeftRoom();
        if(Input.GetKeyDown(KeyCode.RightArrow)) MoveToRightRoom();
    }

    #region Navegation
    public void MoveToUpRoom()
    {
        var x = CurrentMinimapRoom.X;
        var y = CurrentMinimapRoom.Y + 1;
        if (_minimapRoomStorage.TryGet(x, y, out var room))
        {
            if (!CurrentMinimapRoom.HasConnectionTo(0x01)) return;

            CurrentMinimapRoom.Exit();

            var oldRoom = CurrentMinimapRoom;
            CurrentMinimapRoom = room.GetComponent<MinimapRoomController>();

            CurrentMinimapRoom.Enter();
        }
    }
    public void MoveToRightRoom()
    {
        var x = CurrentMinimapRoom.X + 1;
        var y = CurrentMinimapRoom.Y;
        if (_minimapRoomStorage.TryGet(x, y, out var room))
        {
            if (!CurrentMinimapRoom.HasConnectionTo(0x02)) return;

            CurrentMinimapRoom.Exit();

            var oldRoom = CurrentMinimapRoom;
            CurrentMinimapRoom = room.GetComponent<MinimapRoomController>();

            CurrentMinimapRoom.Enter();
        }
    }
    public void MoveToDownRoom()
    {
        var x = CurrentMinimapRoom.X;
        var y = CurrentMinimapRoom.Y - 1;
        if (_minimapRoomStorage.TryGet(x, y, out var room))
        {
            if (!CurrentMinimapRoom.HasConnectionTo(0x04)) return;

            CurrentMinimapRoom.Exit();

            var oldRoom = CurrentMinimapRoom;
            CurrentMinimapRoom = room.GetComponent<MinimapRoomController>();

            CurrentMinimapRoom.Enter();
        }
    }
    public void MoveToLeftRoom()
    {
        var x = CurrentMinimapRoom.X - 1;
        var y = CurrentMinimapRoom.Y;
        if (_minimapRoomStorage.TryGet(x, y, out var room))
        {
            if (!CurrentMinimapRoom.HasConnectionTo(0x08)) return;

            CurrentMinimapRoom.Exit();

            var oldRoom = CurrentMinimapRoom;
            CurrentMinimapRoom = room.GetComponent<MinimapRoomController>();

            CurrentMinimapRoom.Enter();
        }
    }
    #endregion

    private void Dungeon_IteratorDelegate(int x, int y, int value)
    {
        //Get connections from first group of bits
        var connections = value & 0x0F;
        if (connections == 0) return;

        //Get room type from second group of bits
        var roomType = (value >> 4) & 0x0F;
        var connectedNeighbors = _dungeon.GetNumberOfConnectedNeighborsAt(x, y);

        //If room has only one neighbor and is 'default', convert it into special room
        if (connectedNeighbors == 1) 
            roomType = (roomType == 0) ? 0x03 : roomType;

        //If room is special, get special room type and save changes into dungeon data
        if ((RoomType)roomType == RoomType.Special)
        {
            roomType = (int)GetRandomSpecialRoomType();
            _dungeon.SetRoomType(x, y, roomType);
        }

        //Instantiate Room
        GenerateMinimapRoom(x, y, connections, (RoomType)roomType);
    }
    public void GenerateNewDungeon()
    {
        ClearStorage();
        _dungeonGenerator.Start();
    }
    private void GenerateMinimapRoom(int x, int y, int connections, RoomType roomType)
    {
        //Create MinimapRoom GameObject
        var position = new Vector3(x, y, 0);
        var minimapRoom = Instantiate(_minimapRoomPrefab, position, Quaternion.identity);

        //Add MinimapRoom GameObject to storage
        var coors = (x, y);
        var minimapRoomConfig = GetMinimapRoomConfig(roomType);
        var minimapRoomData = new MinimapRoomData(coors, connections, minimapRoomConfig);

        if (_minimapRoomStorage.Add(minimapRoom, minimapRoomData, out var controller))
        {
            if (roomType == RoomType.Start)
            {
                CurrentMinimapRoom = controller;
                CurrentMinimapRoom.Enter();
            }
        }
        else
            Debug.LogError($"MinimapRoom <Color=Cyan> GameObject </Color> doesn't have <Color=Red> MinimapRoomController </Color>");
    }
    private void ClearStorage()
    {
        _minimapRoomStorage.Clear();
    }
    private MinimapRoomSO GetMinimapRoomConfig(RoomType roomType)
    {
        foreach (var roomConfig in _minimapRoomsSO)
            if(roomType == roomConfig.Type) return roomConfig;

        return null;
    }
    private RoomType GetRandomSpecialRoomType() => Random.value switch
    {
        < 0.3f => RoomType.Treasure,
        > 0.8f => RoomType.Secret,
        _ => RoomType.Camp,
    };
}
