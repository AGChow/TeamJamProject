using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{

    public GameObject[] puzzleElements;

    [SerializeField]
    private GameObject mainLight;
    

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
        Debug.Log("You did it");
        yield return new WaitForSeconds(1f);

        //turn on light and maybe lerp intensity to 1
        mainLight.SetActive(true);
        mainLight.GetComponent<Light>().intensity = 1;

        //unlock door
        //play complete sound
    }
}
