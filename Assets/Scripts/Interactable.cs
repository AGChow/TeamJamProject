using System.Collections.Generic;
using UnityEngine;

public enum InteractableType {
    Sword,
    Player,
    Enemy
}

public class Interactable : MonoBehaviour
{
    public List<InteractableType> interactableTypes = new();
    public List<Watcher> watchers = new();
    private List<string> interactableNames = new();
    private bool _isActivated = false;

    public bool isActivated {
        get
        {
            return _isActivated;
        }
        set
        {
            // Switch is activated
            if(_isActivated == false && value == true)
            {
                foreach(Watcher watcher in watchers)
                {
                    if(!watcher.GetComponent<BatAI>())
                        watcher.Activate();
                }
            }
            // Switch is deactivated
            else if(_isActivated == true && value == false)
            {
                foreach(Watcher watcher in watchers)
                {
                    if(watcher.canToggle)
                    {
                        if(watcher.canToggle)
                            watcher.Deactivate();
                    }
                }
            }
            _isActivated = value;
        }
    }

    void Awake()
    {
        interactableNames = interactableTypes.ConvertAll(f => f.ToString());
    }

    void OnTriggerEnter(Collider other)
    {
        if (interactableNames.Contains(other.tag))
        {
            FindObjectOfType<AudioManager>().Play("ButtonandSwitch");
            ToggleIsActivated();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(interactableNames.Contains(other.tag))
        {
            FindObjectOfType<AudioManager>().Play("ButtonandSwitch");

            ToggleIsActivated();
        }
    }

    void ToggleIsActivated()
    {
        isActivated = !isActivated;
    }
}
