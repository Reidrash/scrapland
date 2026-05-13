using UnityEngine;
using TMPro; // Para el texto
using UnityEngine.SceneManagement; // Para cambiar de escena despues

public class GestorLanzamiento : MonoBehaviour
{
    public static GestorLanzamiento Instancia;

    [Header("Variables de Juego")]
    public float tiempoRestante = 30f;
    public int puntos = 0;
    public bool juegoActivo = true;

    [Header("Referencias UI")]
    public TextMeshProUGUI textoCronometro;
    public TextMeshProUGUI textoPuntaje;
    public GameObject panelFinal;
    public TextMeshProUGUI textoPuntajeFinal;

    void Awake()
    {
        Instancia = this;
    }

    void Update()
    {
        if (juegoActivo)
        {
            // Restamos el tiempo cada segundo
            tiempoRestante -= Time.deltaTime;

            // Actualizamos el reloj (Mathf.Max evita numeros negativos)
            textoCronometro.text = "Tiempo: " + Mathf.Ceil(Mathf.Max(0, tiempoRestante)).ToString();

            if (tiempoRestante <= 0)
            {
                FinalizarJuego();
            }
        }
    }

    public void SumarPunto()
    {
        if (!juegoActivo) return;
        puntos += 10;
        textoPuntaje.text = "Puntaje: " + puntos.ToString();
    }

    void FinalizarJuego()
    {
        juegoActivo = false;
        panelFinal.SetActive(true); // Mostramos el carton de resultados
        textoPuntajeFinal.text = "Puntos: " + puntos.ToString(); 
    }

    public void SalirAlTablero()
    {
        // Por ahora solo un log, luego cargaremos la escena de tu compañero
        Debug.Log("Cargando escena del tablero...");
    }
}