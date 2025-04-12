using System.Collections.Generic;
using UnityEngine;

public class GeneradorCapas : MonoBehaviour
{
    public GameObject esferaPrefab; // Prefab de la esfera más pequeña
    public Transform objetoCentral;
    public float velocidadOrbita = 4000f;
    public float radioOrbitaK;
    public float radioOrbitaL;
    public float radioOrbitaM;
    public int maxEsferasK;
    public int maxEsferasL;
    public int maxEsferasM;
    public GameObject contenedorImagen;
    public Material electronMaterial; // Material para las esferas

    private List<GameObject> esferasK = new List<GameObject>();
    private List<GameObject> esferasL = new List<GameObject>();
    private List<GameObject> esferasM = new List<GameObject>();

    private ControladorAR controladorAR; //Script para detectar si un elemento está presente
    private bool presenteElemento; //Booleano que indica si está presente
    private bool capasGeneradas; //Booleano que indica si estan generadas las capas
    private bool presenteMolecula; //Booleano que indica si esta presente molecula

    void Start()
    {
        controladorAR = contenedorImagen.GetComponent<ControladorAR>();
        presenteElemento = false;
        capasGeneradas = false;
        presenteMolecula = false;
    }

    public void SetCapasGeneradas(bool estado)
    {
        this.capasGeneradas = estado;
    }

    public bool GetCapasGeneradas()
    {
        return capasGeneradas;
    }
    void Generar(int maxEsferas, float radioOrbita, List<GameObject> listaEsferas)
    {
        Vector3 posicionCentral = objetoCentral.position;

        // Calcular la rotación para todas las esferas en la capa actual
        Quaternion rotacion = Quaternion.Euler(0f, velocidadOrbita * Time.deltaTime, 0f);

        // Instanciar y configurar elementos en la órbita
        for (int i = 0; i < maxEsferas; i++)
        {
            // Calcular el ángulo actual
            float angulo = i * (360f / maxEsferas);
            float anguloRadianes = angulo * Mathf.Deg2Rad;
            float x = Mathf.Cos(anguloRadianes) * radioOrbita;
            float z = Mathf.Sin(anguloRadianes) * radioOrbita;

            // Sumar la posición del objeto central para obtener la posición final del elemento
            Vector3 posicionElemento = posicionCentral + new Vector3(x, 0, z);

            // Instanciar y configurar la esfera
            GameObject esfera = Instantiate(esferaPrefab, posicionElemento, Quaternion.identity);
            esfera.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            esfera.transform.SetParent(contenedorImagen.transform);
            listaEsferas.Add(esfera);

            // Aplicar la rotación a la esfera
            esfera.transform.rotation = rotacion;

            // Asignar el material a la esfera
            MeshRenderer renderer = esfera.GetComponent<MeshRenderer>();
            renderer.material = electronMaterial;
        }
    }

    void Update()
    {
        presenteMolecula = controladorAR.GetPresenteMolecula();
        if (!presenteMolecula)
        {
            presenteElemento = controladorAR.GetPresenteElemento();

            if (presenteElemento && !capasGeneradas)
            {
                GenerarElementosCapas();
            }

            if (!presenteElemento && capasGeneradas)
            {
                EliminarElementosCapas();
            }

            if (presenteElemento && capasGeneradas)
            {
                // Hacer que las esferas giren alrededor del objeto central
                RotarElementos(esferasK, velocidadOrbita);
                RotarElementos(esferasL, velocidadOrbita);
                RotarElementos(esferasM, velocidadOrbita);
            }
        }
    }

    void RotarElementos(List<GameObject> listaElementos, float velocidad)
    {
        foreach (GameObject element in listaElementos)
        {
            // Rotar la esfera alrededor del objeto central
            element.transform.RotateAround(objetoCentral.position, Vector3.up, velocidad * Time.deltaTime);
        }
    }

    public void GenerarElementosCapas()
    {
        // Generar aros y esferas en las capas
        Generar(maxEsferasK, radioOrbitaK, esferasK);
        Generar(maxEsferasL, radioOrbitaL, esferasL);
        Generar(maxEsferasM, radioOrbitaM, esferasM);

        capasGeneradas = true;
    }

    public void EliminarElementosCapas()
    {
        foreach (GameObject sphere in esferasK)
        {
            Destroy(sphere);
        }
        foreach (GameObject sphere in esferasL)
        {
            Destroy(sphere);
        }
        foreach (GameObject sphere in esferasM)
        {
            Destroy(sphere);
        }

        esferasK.Clear();
        esferasL.Clear();
        esferasM.Clear();

        capasGeneradas = false;
    }
}
