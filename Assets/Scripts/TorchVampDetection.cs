using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchVampDetection : MonoBehaviour
{
    public List<VampireToadAI> frozenVampires = new List<VampireToadAI>();
    void OnTriggerEnter(Collider collider) {
        if(collider.name.Contains("VampireToad")) {
            print("Collided with vampire!");
            if(GetComponentInParent<Torch>().isLit) {
                print("Collided and torch was lit!");
                StartCoroutine(collider.GetComponent<VampireToadAI>().Freeze(true));
            }
        }
    }

    void OnTriggerStay(Collider collider) {
        if(collider.name.Contains("VampireToad")) {
            print("Collided with vampire!");
            if(GetComponentInParent<Torch>().isLit) {
                frozenVampires.Add(collider.GetComponent<VampireToadAI>());
                StartCoroutine(collider.GetComponent<VampireToadAI>().Freeze(true));
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        if(collider.name.Contains("VampireToad")) {
            collider.GetComponent<VampireToadAI>().frozen = false;
        }
    }
}
