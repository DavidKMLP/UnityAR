using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorNucleo : MonoBehaviour
{
    public GameObject esferaPrefab; // Prefab de la esfera m�s peque�a
    public Material materialProtones;
    public Material materialNeutrones;
    public int protones;
    public int neutrones;
    public GameObject contenedorImagen;
    private float multiplicadorRadioInterno = 0.5f; // Multiplicador del radio para las esferas m�s peque�as
    private ControladorAR controladorAR; //Script para detectar si un elemento est� presente
    private bool presenteElemento; //Booleano que indica si est� presente elemento
    private bool nucleoGenerado;    //Booleano que indica si est� generado el nucleo
    private bool presenteMolecula; //Booleano que indica si esta presente molecula
    void Start()
    {
        controladorAR = contenedorImagen.GetComponent<ControladorAR>();
        presenteElemento = false;
        nucleoGenerado = false;
        presenteMolecula = false;
    }

    public void SetNucleoGenerado(bool estado)
    {
        this.nucleoGenerado = estado;
    }
    public bool GetNucleoGenerado()
    {
        return nucleoGenerado;
    }

    private void Update()
    {
        presenteMolecula = controladorAR.GetPresenteMolecula();
        if (!presenteMolecula)
        {
            presenteElemento = controladorAR.GetPresenteElemento();

            if (presenteElemento && !nucleoGenerado)
            {
                generarElementosNucleo();
            }

            if (!presenteElemento && nucleoGenerado)
            {
                eliminarElementosNucleo();
            }
        }
    }

    void GenerarEsferasDentroDeEsferaGrande(Vector3 centro, float radioGrande, float radioPequenio)
    {
        for (int i = 0; i < protones + neutrones; i++)
        {
            // Calcular posici�n aleatoria dentro de la esfera grande
            Vector3 posicionAleatoria = Random.insideUnitSphere * radioGrande;

            // Instanciar una esfera peque�a en la posici�n aleatoria
            GameObject esferaPequenia = Instantiate(esferaPrefab, centro + posicionAleatoria, Quaternion.identity);

            // Escalar la esfera peque�a seg�n el radio especificado
            esferaPequenia.transform.localScale = Vector3.one * radioPequenio * 2f;

            // Asignar el material a la esfera peque�a
            Renderer renderer = esferaPequenia.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (i < neutrones)
                {
                    renderer.material = materialNeutrones;
                }
                else
                {
                    renderer.material = materialProtones;
                }
            }
            // Asignar la imagen como padre de la esfera peque�a
            esferaPequenia.transform.SetParent(contenedorImagen.transform);
        }
    }

    public void generarElementosNucleo()
    {
        // Obtener la escala de la esfera
        Vector3 scale = transform.localScale;

        // Suponemos que la escala est� uniformemente aplicada (es una esfera)
        float radioEsferaGrande = scale.x * 0.5f;

        // Calcular el radio de las esferas peque�as
        float radioEsferaPequenia = radioEsferaGrande * multiplicadorRadioInterno;

        // Generar esferas peque�as dentro de la esfera grande
        GenerarEsferasDentroDeEsferaGrande(transform.position, radioEsferaGrande, radioEsferaPequenia);

        nucleoGenerado = true; //Nucleo generado
    }

    public void eliminarElementosNucleo()
    {
        foreach (Transform child in contenedorImagen.transform)
        {
            Destroy(child.gameObject);
        }
        nucleoGenerado = false; //Se ha eliminado el nucleo
    }
}
