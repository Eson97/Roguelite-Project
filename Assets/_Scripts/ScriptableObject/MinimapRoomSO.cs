using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MinimapRoomController;

[CreateAssetMenu(menuName = "MinimapRoom")]
public class MinimapRoomSO : ScriptableObject
{
    [SerializeField] private RoomType _type;
    [SerializeField] private Color _color;
    [SerializeField] private Sprite _icon;

    public RoomType Type => _type;
    public Color Color => _color;
    public Sprite Icon => _icon;
}
