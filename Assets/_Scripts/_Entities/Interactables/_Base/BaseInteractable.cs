using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> _selectedVisuals;

    protected virtual void Awake()
    {
        Deselect();
    }

    public virtual void Interact()
    {
        Debug.Log("Interact!");
    }

    public void Select() => _selectedVisuals.ForEach(x => x.SetActive(true));

    public void Deselect() => _selectedVisuals.ForEach(x => x.SetActive(false));
}
