using System;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRoomStorage
{
    private Dictionary<(int x, int y), GameObject> _minimapRoomDictionary;

    private Action<GameObject> ClearDelegate;

    public MinimapRoomStorage(Action<GameObject> clearDelegate)
    {
        ClearDelegate = clearDelegate;
        _minimapRoomDictionary = new Dictionary<(int, int), GameObject>();
    }

    public bool Add(GameObject room, MinimapRoomData data, out MinimapRoomController controller)
    {
        if (room.TryGetComponent(out controller))
        {
            controller.SetCoors(data.X, data.Y);
            controller.SetDoors(data.Connections);
            controller.SetMinimapRoomConfig(data.MinimapRoomConfig);

            _minimapRoomDictionary[data.Coors] = room;

            return true;
        }

        return false;
    }
    public GameObject Get(int x, int y) => _minimapRoomDictionary[(x, y)];
    public bool TryGet(int x, int y, out GameObject room) => _minimapRoomDictionary.TryGetValue((x, y), out room);
    public void Clear()
    {
        foreach (GameObject room in _minimapRoomDictionary.Values)
            ClearDelegate(room);

        _minimapRoomDictionary.Clear();
    }

    public struct MinimapRoomData
    {
        public MinimapRoomData((int x, int y) coors, int connections, MinimapRoomSO minimapRoomConfig)
        {
            Coors = coors;
            Connections = connections;
            MinimapRoomConfig = minimapRoomConfig;
        }

        public (int x, int y) Coors { get; private set; }
        public int X => Coors.x;
        public int Y => Coors.y;
        public int Connections { get; private set; }
        public MinimapRoomSO MinimapRoomConfig { get; private set; }
    }
}
