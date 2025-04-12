using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorNucleo : MonoBehaviour
{
    public GameObject esferaPrefab; // Prefab de la esfera más pequeña
    public Material materialProtones;
    public Material materialNeutrones;
    public int protones;
    public int neutrones;
    public GameObject contenedorImagen;
    private float multiplicadorRadioInterno = 0.5f; // Multiplicador del radio para las esferas más pequeñas
    private ControladorAR controladorAR; //Script para detectar si un elemento está presente
    private bool presenteElemento; //Booleano que indica si está presente elemento
    private bool nucleoGenerado;    //Booleano que indica si está generado el nucleo
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
            // Calcular posición aleatoria dentro de la esfera grande
            Vector3 posicionAleatoria = Random.insideUnitSphere * radioGrande;

            // Instanciar una esfera pequeña en la posición aleatoria
            GameObject esferaPequenia = Instantiate(esferaPrefab, centro + posicionAleatoria, Quaternion.identity);

            // Escalar la esfera pequeña según el radio especificado
            esferaPequenia.transform.localScale = Vector3.one * radioPequenio * 2f;

            // Asignar el material a la esfera pequeña
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
            // Asignar la imagen como padre de la esfera pequeña
            esferaPequenia.transform.SetParent(contenedorImagen.transform);
        }
    }

    public void generarElementosNucleo()
    {
        // Obtener la escala de la esfera
        Vector3 scale = transform.localScale;

        // Suponemos que la escala está uniformemente aplicada (es una esfera)
        float radioEsferaGrande = scale.x * 0.5f;

        // Calcular el radio de las esferas pequeñas
        float radioEsferaPequenia = radioEsferaGrande * multiplicadorRadioInterno;

        // Generar esferas pequeñas dentro de la esfera grande
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
