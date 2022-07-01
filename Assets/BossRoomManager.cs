using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    public float torchTimer;
    public GameObject[] puzzleElements;

    [SerializeField]
    private GameObject mainLight;
    private float overTime = 20;

    public GameObject Exit;
    public GameObject Boss;
    


    public void CheckCompleteConditions()
    {

        AllTorchesLit();

        if (AllTorchesLit() == true)
        {
            StartCoroutine(Boss.GetComponent<BossEvent>().Freeze());
            StartCoroutine(DimLightsAfterTime());
            StartCoroutine(DimLightsOnBoss());
        }

    }

    public void RoomCleared()
    {
        StartCoroutine(FinishRoomEvent());

    }

    public bool AllTorchesLit()
    {
        bool[] lit = new bool[puzzleElements.Length];

        //iterates through array of torches to make sure they are on. Fails if one is false
        for (int i = 0; i < puzzleElements.Length; i++)
        {
            if (puzzleElements[i].GetComponent<Torch>().isLit == false)
            {
                return false;
            }
        }

        return true;
    }


    private IEnumerator FinishRoomEvent()
    {
        yield return new WaitForSeconds(.5f);
        //play complete sound
        FindObjectOfType<AudioManager>().Play("PuzzleComplete");

        //turn on light and maybe lerp intensity to 1
        StartCoroutine(DimTheLightsOn());
        StopCoroutine(DimLightsOffBoss());
        StopCoroutine(DimLightsAfterTime());
        yield return new WaitForSeconds(1f);
        //unlock door
        StartCoroutine(Exit.GetComponent<MainDoor>().MoveDoor());

        //keep timed torches on

    }

    IEnumerator DimTheLightsOn()
    {
        mainLight.SetActive(true);
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            mainLight.GetComponent<Light>().intensity = Mathf.Lerp(mainLight.GetComponent<Light>().intensity, 1, (Time.time - startTime) / overTime);
            yield return null;
        }
        mainLight.GetComponent<Light>().intensity = 1;
    }

    IEnumerator DimLightsOnBoss()
    {
        //mainLight.SetActive(true);
        float startTime = Time.time;
        while (Time.time < startTime + 2)
        {
            mainLight.GetComponent<Light>().intensity = Mathf.Lerp(mainLight.GetComponent<Light>().intensity, .3f, (Time.time - startTime) / overTime);
            yield return null;
        }
        mainLight.GetComponent<Light>().intensity = .3f;


    }
    public IEnumerator DimLightsOffBoss()
    {
        //mainLight.SetActive(true);
        float startTime = Time.time;
        while (Time.time < startTime + 3)
        {
            mainLight.GetComponent<Light>().intensity = Mathf.Lerp(mainLight.GetComponent<Light>().intensity, .3f, (Time.time - startTime) / overTime);
            yield return null;
        }
        mainLight.GetComponent<Light>().intensity = .015f;

    }
    public IEnumerator DimLightsAfterTime()
    {
        yield return new WaitForSeconds(7);
        StartCoroutine(DimLightsOffBoss());
    }
}

