using System;
using UnityEngine;

[SelectionBase]
public class MinimapRoomController : MonoBehaviour, IRoom
{
    [SerializeField] private SpriteRenderer _roomRenderer;
    [SerializeField] private GameObject _icon;
    [SerializeField] private GameObject _doorUp;
    [SerializeField] private GameObject _doorRight;
    [SerializeField] private GameObject _doorDown;
    [SerializeField] private GameObject _doorLeft;

    private int _x, _y;
    private MinimapRoomSO _minimapRoomSO;

    public int X => _x;
    public int Y => _y;
    public int Connections { get; private set; }

    public virtual void Enter()
    {
        _roomRenderer.color = Color.cyan;
    }
    public virtual void Exit()
    {
        _roomRenderer.color = _minimapRoomSO.Color;
    }

    public bool HasConnectionTo(int direction) => (Connections & direction) != 0;

    public void SetCoors(int x,int y)
    {
        _x = x;
        _y = y;
    }
    public void SetDoors(int connections)
    {
        if (connections == 0) Destroy(gameObject);

        if ((connections & 0x01) != 0) _doorUp.SetActive(true);
        if ((connections & 0x02) != 0) _doorRight.SetActive(true);
        if ((connections & 0x04) != 0) _doorDown.SetActive(true);
        if ((connections & 0x08) != 0) _doorLeft.SetActive(true);

        Connections = connections;
    }
    public void SetMinimapRoomConfig(MinimapRoomSO minimapRoomSO)
    {
        _minimapRoomSO = minimapRoomSO;
        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
        if (_minimapRoomSO == null) return;

        //Color
        _roomRenderer.color = _minimapRoomSO.Color;

        //Icon
        if (_minimapRoomSO.Icon == null)
            _icon.SetActive(false);
        else
            _icon.GetComponent<SpriteRenderer>().sprite = _minimapRoomSO.Icon;
    }

    public enum RoomType
    {
        Default = 0x00,
        Start = 0x01,
        Boss = 0x02,
        Special = 0x03,

        //Special types
        Treasure = 0x10,
        Camp = 0x11,
        Secret = 0x12
    }
}
