using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    //enemy behavior needs trigger collider as child to detect
    public bool agro;
    public bool torchReaction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerDetect();
        }
        else if(other.CompareTag("Torch"))
        {
            HandleTorchDetect(other.gameObject.GetComponent<Torch>());
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
    public void HandleTorchDetect(Torch torch)
    {
        if (torch.isLit == true)
        {
            torchReaction = true;
        }
        
    }
    public void HandleTorchLoss()
    {
        torchReaction = false;
    }
}
