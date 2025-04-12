using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAR : MonoBehaviour
{
    private bool presenteElemento;
    private bool presenteMolecula;

    void Start()
    {
        presenteElemento = false;
        presenteMolecula = false;
    }

    public bool GetPresenteElemento()
    {
        return presenteElemento;
    }
    public bool GetPresenteMolecula()
    {
        return presenteMolecula;
    }

    public void SetPresenteMolecula(bool presente)
    {
        this.presenteMolecula = presente;
    }
    public void ElementoPresente(bool presente) //OnTargetFound() y OnTargetLost()
    {
        if (presente)
        {
            Debug.Log("Se ha encontrado el elemento");
            presenteElemento = true;
        }
        else if (!presente)
        {
            presenteElemento = false;
        }
    }
}
