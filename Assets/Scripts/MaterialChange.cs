using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public Material[] material;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        
    }

    public IEnumerator FlashWhite()
    {
        rend.sharedMaterial = material[1];
        yield return new WaitForSeconds(.2f);
        rend.sharedMaterial = material[0];
    }

    public void ChangeToAltMaterial()
    {
        rend.sharedMaterial = material[2];
    }
    public void ChangeBackToOrigingalMaterial()
    {
        rend.sharedMaterial = material[0];

    }

}
