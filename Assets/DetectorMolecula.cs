using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorMolecula : MonoBehaviour
{
    public GameObject molecula;
    public GameObject atomoPrincipal;
    public GameObject atomoSecundario1;
    public GameObject atomoSecundario2;
    public GameObject atomoSecundario3;
    public GameObject atomoSecundario4;
    public GameObject[] atomos;
    private float distanciaMaximaAtomos = 18f;


    void Start()
    {
        atomos = new GameObject[] { atomoPrincipal, atomoSecundario1, atomoSecundario2, atomoSecundario3, atomoSecundario4 };
        List<GameObject> listaAtomos = new List<GameObject>();

        // Agregar los GameObject que no son nulos a la lista
        foreach (GameObject atom in atomos)
        {
            if (atom != null)
            {
                listaAtomos.Add(atom);
            }
        }

        atomos = listaAtomos.ToArray();
    }

    void Update()
    {
        MostrarMolecula();
    }
    /** Metodo que comprueba que todos los atomos de la molecula están activos **/
    public bool ActivoMolecula()
    {
        bool activos = true;
        ControladorAR aux;
        for (int i = 0; i < atomos.Length; i++)
        {
            aux = atomos[i].GetComponent<ControladorAR>();
            if (!aux.GetPresenteElemento())
            {
                activos = false;
            }
        }

        return activos;
    }

    void MostrarMolecula()
    {
        if (ActivoMolecula() && PocaDistanciaAtomos())
        {
            Debug.Log("Se ha encontrado la molecula");
            ControladorAR aux;
            for (int i = 0; i < atomos.Length; i++)
            {
                aux = atomos[i].GetComponent<ControladorAR>();
                aux.SetPresenteMolecula(true);
            }
            molecula.transform.position = CalcularPosicionMolecula();
            OcultarAtomos();
        }
        else
        {
            ControladorAR aux;
            for (int i = 0; i < atomos.Length; i++)
            {
                aux = atomos[i].GetComponent<ControladorAR>();
                aux.SetPresenteMolecula(false);
            }
        }
    }

    void OcultarAtomos()
    {
        foreach (GameObject obj in atomos)
        {
            obj.GetComponent<GeneradorNucleo>().eliminarElementosNucleo();
            obj.GetComponent<GeneradorNucleo>().SetNucleoGenerado(false);
            obj.GetComponent<GeneradorCapas>().EliminarElementosCapas();
            obj.GetComponent<GeneradorCapas>().SetCapasGeneradas(false);
        }
    }
    Vector3 CalcularPosicionMolecula()
    {
        Vector3 posicion = new Vector3(0f, 0f, 0f);
        int i = 0;
        foreach (GameObject obj in atomos)
        {
            if (obj != null)
            {
                i++;
                posicion = posicion + obj.transform.position;
            }
        }
        return posicion;
    }

    public bool PocaDistanciaAtomos()
    {
        bool distanciaAceptable = true;
        for (int i = 0; i < atomos.Length; i++)
        {
            for (int j = i + 1; j < atomos.Length; j++)
            {
                if (Vector3.Distance(atomos[i].transform.position, atomos[j].transform.position) > distanciaMaximaAtomos)
                    distanciaAceptable = false;
            }
        }
        return distanciaAceptable;
    }
}
