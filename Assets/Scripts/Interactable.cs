using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType {
    Sword
}

public class Interactable : MonoBehaviour
{
    public InteractableType interactableType;
    public List<Watcher> watchers = new();
    private bool _isActivated = false;
    public bool isActivated {
        get { return _isActivated; }
        set {
            // Switch is activated
            if(_isActivated == false && value == true)
            {
                foreach(Watcher watcher in watchers)
                {
                    watcher.Activate();
                }
            }
            // Switch is deactivated
            else if(_isActivated == true && value == false)
            {
                foreach(Watcher watcher in watchers)
                {
                    watcher.Deactivate();
                }
            }
            _isActivated = value;
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag(interactableType.ToString())) {
            ToggleIsActivated();
        }
    }

    void ToggleIsActivated() {
        isActivated = !isActivated;
    }
}
