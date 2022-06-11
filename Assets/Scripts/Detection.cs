using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    //enemy behavior needs trigger collider as child to detect
    public bool agro;
    public bool torchReaction;
    public GameObject torchInRange;
    // Start is called before the first frame update
    void Update()
    {
        if(torchInRange == null)
        {
            return;
        }
        else
        {
            torchReaction = torchInRange.GetComponent<Torch>().isLit;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerDetect();
        }
        else if(other.CompareTag("Torch"))
        {
            HandleTorchDetect(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            HandlePlayerLoss();
        }
        else if (other.gameObject.tag == "Player")
        {
            HandlePlayerLoss();
        }
    }

    public void HandlePlayerDetect()
    {
        agro = true;
    }
    public void HandlePlayerLoss()
    {
        agro = false;
    }
    public void HandleTorchDetect(GameObject torch)
    {
        torchInRange = torch;
        
    }
    public void HandleTorchLoss()
    {
        torchInRange = null;
    }
}
