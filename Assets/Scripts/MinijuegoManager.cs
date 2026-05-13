using UnityEngine;
using TMPro;

public class MinijuegoManager : MonoBehaviour
{
    public static MinijuegoManager Instancia;

    [Header("UI Principal")]
    public TextMeshProUGUI textoPuntaje;
    public TextMeshProUGUI textoTiempo;

    [Header("UI Resultados")]
    public GameObject panelResultados; // Arrastra tu Panel aqui
    public TextMeshProUGUI textoPuntajeFinal; // Arrastra tu Texto de puntaje final aqui

    [Header("Configuracion")]
    public float tiempoRonda = 60f;
    private int puntaje = 0;
    public bool juegoActivo = false;

    void Awake() { Instancia = this; }

    void Start()
    {
        panelResultados.SetActive(false); // Asegurarnos de que inicie apagado
        juegoActivo = true;
        ActualizarUI();
    }

    void Update()
    {
        if (juegoActivo)
        {
            tiempoRonda -= Time.deltaTime;
            textoTiempo.text = "Tiempo: " + Mathf.Ceil(tiempoRonda).ToString();

            if (tiempoRonda <= 0)
            {
                TerminarJuego();
            }
        }
    }

    public void SumarPuntos(int puntos)
    {
        if (!juegoActivo) return;
        puntaje += puntos;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        textoPuntaje.text = "Puntos: " + puntaje.ToString();
    }

    void TerminarJuego()
    {
        juegoActivo = false;
        textoTiempo.text = "¡Tiempo Terminado!";

        // Encender el panel y mostrar el puntaje final
        panelResultados.SetActive(true);
        textoPuntajeFinal.text = "Puntaje Total: " + puntaje.ToString();
    }

    // Esta funcion la usara el boton para salir
    public void VolverAlTablero()
    {
        Debug.Log("Aqui cargariamos la escena del tablero 3D. ¡Si hubiera una!");
    }
}