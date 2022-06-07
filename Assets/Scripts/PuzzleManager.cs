using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{

    public GameObject[] puzzleElements;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckCompleteConditions()
    {
        AllTorchesLit();

        if(AllTorchesLit() == true)
        {
            StartCoroutine(FinishPuzzleEvent());
        }
    }

    public bool AllTorchesLit()
    {
        bool[] lit = new bool[puzzleElements.Length];

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
        //unlock door
        //turn on lights in room
        //play complete sound
    }
}
