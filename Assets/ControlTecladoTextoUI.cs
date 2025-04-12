using UnityEngine;
using UnityEngine.UI;

public class ControlTecladoTextoUI : MonoBehaviour
{
    public GameObject texto1; 
    public GameObject texto2;
    public GameObject texto3;
    public GameObject texto4;
    public GameObject texto5;
    public GameObject texto6;
    public GameObject texto7;
    public GameObject texto8;
    public GameObject texto9;
    public GameObject texto10;
    public GameObject[] textos;
    public GameObject plane;

    private bool textoActivo = false; // Indica si el texto está activo

    void Start()
    {
        textos  = new GameObject[] { texto1,texto2,texto3,texto4,texto5,texto6,texto7,texto8,texto9,texto10};
        // Desactivar el texto al inicio
        TextMesh textoUI; 
        for(int i = 0; i < textos.Length; i++)
        {
            textoUI =textos[i].GetComponent<TextMesh>();
            textoUI.gameObject.SetActive(textoActivo);
        }        
    }

    void Update()
    {
        TextMesh textoUI;
        if (Input.GetKeyDown(KeyCode.I))
        {
            textoActivo = !textoActivo;
            for (int i =0; i< textos.Length; i++)
            {
                textoUI = textos[i].GetComponent<TextMesh>();
                textoUI.gameObject.SetActive(textoActivo);
            }
        }
    }
}

