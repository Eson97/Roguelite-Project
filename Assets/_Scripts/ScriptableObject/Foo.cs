using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Foo")]
public class Foo : ScriptableObject
{
    [Header("Foo Info")]
    [SerializeField] private Class _class;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Stats _baseStats;
    [Space]
    [SerializeReference] private Passive _passiveEffect;

    #region Menu Items

    [ContextMenu(nameof(AddCultistPassive))] void AddCultistPassive() { _passiveEffect = new Cultist(); }
    [ContextMenu(nameof(AddSorcererPassive))] void AddSorcererPassive() { _passiveEffect = new Sorcerer(); }
    [ContextMenu(nameof(AddBlackSmithPassive))] void AddBlackSmithPassive() { _passiveEffect = new BlackSmith(); }
    [ContextMenu(nameof(AddMerchPassive))] void AddMerchPassive() { _passiveEffect = new Merch(); }

    #endregion

    #region temp
    [Serializable]
    public struct Stats
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _defense;
        [SerializeField] private int _health;

        public int MaxHealth => _maxHealth;
        public int Defense => _defense;
        public int Health => _health;
    }
    public enum Class { healer,attacker,merchant,farmer, }
    #endregion
}

#region Passives

[Serializable]
public abstract class Passive
{
    [SerializeField] private string _name;
    [Multiline(4)]
    [SerializeField] private string _description;

    public string Name { get => _name; protected set => _name = value; }
    public string Description { get => _description; protected set => _description = value; }

    public Passive()
    {
        _name = ToString();
        _description = $"{ToString()} passive description";
    }

    public abstract void Apply();

}

[Serializable]
public class Cultist : Passive
{
    public override void Apply()
    {
        Debug.Log("Cultist Effect");
    }
}

[Serializable]
public class Sorcerer : Passive
{
    public override void Apply()
    {
        Debug.Log("Sorcerer Effect");
    }
}

[Serializable]
public class BlackSmith : Passive
{
    public override void Apply()
    {
        Debug.Log("BlackSmith Effect");
    }
}

[Serializable]
public class Merch : Passive
{
    public override void Apply()
    {
        Debug.Log("MerchEffect");
    }
}

#endregion


