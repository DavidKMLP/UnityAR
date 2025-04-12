using UnityEngine;
using UnityEngine.UI;

public class MostrarInformacionElemento : MonoBehaviour
{
    public GameObject controlador; // Referencia al GameObject que contiene los scripts GeneradorNucleo y GeneradorCapas
    public GameObject texto3D; // Referencia al objeto de texto UI
    public string simboloAtomo, nombreAtomo, masaAtomica;
    private ControladorAR controladorAR;
    private bool presente = false;
    private bool presenteMolecula = false;

    void Start()
    {
        controladorAR = controlador.GetComponent<ControladorAR>();
    }

    void Update()
    {
        presente = controladorAR.GetPresenteElemento();
        presenteMolecula = controladorAR.GetPresenteMolecula();
        if (presente && !presenteMolecula)
        {
            MostrarInformacion();
        }
        else
        {
            //deshabilita los textos por cada atomo presente
            TextMesh textoUI = texto3D.GetComponent<TextMesh>();
            textoUI.gameObject.SetActive(false);
        }
    }

    public void MostrarInformacion()
    {
        TextMesh textoUI = texto3D.GetComponent<TextMesh>();

        GeneradorNucleo generadorNucleo = controlador.GetComponent<GeneradorNucleo>();
        GeneradorCapas generadorCapas = controlador.GetComponent<GeneradorCapas>();

        int protones = generadorNucleo.protones;
        int neutrones = generadorNucleo.neutrones;
        int maxSpheresK = generadorCapas.maxEsferasK;
        int maxSpheresL = generadorCapas.maxEsferasL;
        int maxSpheresM = generadorCapas.maxEsferasM;

        int totalElectrones = maxSpheresK + maxSpheresL + maxSpheresM;

        string informacion = "Símbolo: " + simboloAtomo + "\n" +
                                     "Átomo: " + nombreAtomo + "\n" +
                                     "Protones: " + protones + "\n" +
                                     "Neutrones: " + neutrones + "\n" +
                                     "Total de Electrones: " + totalElectrones + "\n" +
                                     "Electrones en el nivel K: " + maxSpheresK + "\n" +
                                     "Electrones en el nivel L: " + maxSpheresL + "\n" +
                                     "Electrones en el nivel M: " + maxSpheresM + "\n" +
                                     "Masa atómica: " + masaAtomica;

        // Actualizar el texto del objeto de texto
        textoUI.text = informacion;
    }
}
