using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{

    public GameObject[] puzzleElements;

    [SerializeField]
    private GameObject mainLight;
    private float overTime = 20;

    public GameObject Exit;

    

    public void CheckCompleteConditions()
    {
        AllTorchesLit();

        if(AllTorchesLit() == true)
        {
            Debug.Log("CheckCheckCheck");

            StartCoroutine(FinishPuzzleEvent());
        }
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


    private IEnumerator FinishPuzzleEvent()
    {
        //play complete sound
        Debug.Log("You did it");

        //turn on light and maybe lerp intensity to 1
        StartCoroutine(DimTheLightsOn());
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
}
