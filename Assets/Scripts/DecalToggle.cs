using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalToggle : MonoBehaviour
{
    Material mat;
    bool showDecal = false;

    void OnMouseDown()
    {
        showDecal = !showDecal;
        if (showDecal)
            mat.SetFloat("_ShowDecal", 1);
        else
            mat.SetFloat("_ShowDecal", 0);
    }
    void Start()
    {
        mat = this.GetComponent<Renderer>().sharedMaterial;
    }

   
}
