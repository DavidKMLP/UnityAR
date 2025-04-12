using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarMolecula : MonoBehaviour
{
    public GameObject molecula;
    public Transform moleculaPosicion;
    public Material materialPrincipal;
    public Material materialSecundario;
    public float radioPrincipal;
    public float radioSecundario;
    public int numeroEsferasExternas;
    private bool presenteMolecula;
    private bool moleculaGenerada;
    private DetectorMolecula detectorMolecula;

    void Start()
    {
        presenteMolecula = false;
        moleculaGenerada = false;
        detectorMolecula = molecula.GetComponent<DetectorMolecula>();
    }

    void Update()
    {
        presenteMolecula = detectorMolecula.ActivoMolecula();
        if (presenteMolecula && !moleculaGenerada && detectorMolecula.PocaDistanciaAtomos())
        {
            GenerarMoleculaMetodo();
        }
        if (!presenteMolecula && moleculaGenerada || !detectorMolecula.PocaDistanciaAtomos() && moleculaGenerada) //Se destruye si desparece un elemento de la escena o si se alejan
        {
            EliminarMolecula();
        }
    }

    void GenerarMoleculaMetodo()
    {
        Vector3 posicionCentral = moleculaPosicion.position;
        GameObject esferaPrincipal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        esferaPrincipal.transform.position = posicionCentral;
        esferaPrincipal.transform.localScale = new Vector3(radioPrincipal, radioPrincipal, radioPrincipal);
        esferaPrincipal.transform.SetParent(molecula.transform);
        Renderer renderer = esferaPrincipal.GetComponent<Renderer>();
        renderer.material = materialPrincipal;
        float distanciaMinima = 2 * Mathf.PI * radioPrincipal / numeroEsferasExternas;
        for (int i = 0; i < numeroEsferasExternas; i++)
        {
            Vector3 direcionAleatoria = Random.onUnitSphere;
            float angulo = i * Mathf.PI * 2 / numeroEsferasExternas;
            Vector3 posicionAleatoria = posicionCentral + new Vector3(Mathf.Cos(angulo), 0f, Mathf.Sin(angulo)) * radioSecundario;
            GameObject esferaSecundaria = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            esferaSecundaria.transform.position = posicionAleatoria;
            esferaSecundaria.transform.localScale = new Vector3(radioSecundario, radioSecundario, radioSecundario);
            esferaSecundaria.transform.SetParent(molecula.transform);
            Renderer renderer2 = esferaSecundaria.GetComponent<Renderer>();
            renderer2.material = materialSecundario;
        }
        moleculaGenerada = true;
    }
    public void EliminarMolecula()
    {
        // Destruir todos los hijos de la molécula
        foreach (Transform child in molecula.transform)
        {
            Destroy(child.gameObject);
        }
        moleculaGenerada = false;
    }
}

