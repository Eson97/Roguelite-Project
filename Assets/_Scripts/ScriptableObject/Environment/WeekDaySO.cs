using UnityEngine;

[CreateAssetMenu(menuName = "Environment/WeekDay", fileName = "NewWeekDay")]
public class WeekDaySO : ScriptableObject
{
    [SerializeField] private Values _value;
    [SerializeField] private string _name;

    public Values Value => _value;
    public string Name => _name;

    public enum Values
    {
        L, Ma, Mi, J, V, S, D //TODO: cambiar nombres
    }
}
