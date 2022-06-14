using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public ParticleSystem dustParticles;
    public GameObject door;
    public Transform openDoorPos;
    public Vector3 closedPos;
    public float overTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        closedPos = door.transform.position;
    }

    public IEnumerator MoveDoor()
    {
        dustParticles.Play();
        
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, openDoorPos.position, (Time.time - startTime) / overTime);
            yield return null;
        }
        door.transform.position = openDoorPos.position;
    }
}
