using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _selectedVisuals;

    protected virtual void Awake()
    {
        Deselect();
    }

    protected virtual void PlayerController_OnSelectedInteractableChanged(IInteractable selected)
    {
        _selectedVisuals.SetActive(this.Equals(selected));
    }

    public virtual void Interact()
    {
        Debug.Log("Interact!");
    }

    public void Select() => _selectedVisuals.SetActive(true);

    public void Deselect() => _selectedVisuals.SetActive(false);
}
