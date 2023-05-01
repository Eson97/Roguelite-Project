using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Environment/DayTime", fileName = "NewDayTime")]
public class DayTimeSO : ScriptableObject
{
    [SerializeField] private  Values _value;
    [SerializeField] private Sprite _image;
    [SerializeField] private Color _background;
    public Values Value => _value;
    public Sprite Image => _image;
    public Color Background => _background;

    public enum Values
    {
        Morning,
        Midday,
        Evening,
        Night,
    }
}
