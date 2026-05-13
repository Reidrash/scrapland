using UnityEngine;
using UnityEngine.UI; // Obligatorio para poder cambiar la imagen de la UI

public class GestorColaBasura : MonoBehaviour
{
    // Esto es el Singleton. Nos permite llamar a este script desde cualquier lado
    public static GestorColaBasura Instancia;

    [Header("Configuracion de Basura")]
    public GameObject[] prefabsBasura; // Tu lista de manzanas, botellas, etc.
    public Transform puntoDeLanzamiento; // El punto en el espacio donde aparece la basura

    [Header("Interfaz (UI)")]
    public Image iconoProximaBasura; // La imagen en blanco que creamos en el Canvas

    private GameObject siguientePrefab; // Guarda en memoria cual es el que sigue
    private GameObject basuraActual; // La basura que el jugador tiene ahorita en la resortera

    void Awake()
    {
        Instancia = this; // Nos asignamos a nosotros mismos como el jefe
    }

    void Start()
    {
        PrepararSiguienteBasura(); // Elegimos la primera basura oculta
        GenerarBasuraEnMesa();     // La escupimos a la mesa
    }

    // Esta funcion elige al azar que sigue y actualiza la fotito en la esquina
    void PrepararSiguienteBasura()
    {
        if (prefabsBasura.Length == 0) return; // Por si olvidas poner los prefabs

        int indiceAleatorio = Random.Range(0, prefabsBasura.Length);
        siguientePrefab = prefabsBasura[indiceAleatorio];

        // Le robamos el dibujo al prefab para mostrarlo en la UI
        Sprite dibujo = siguientePrefab.GetComponent<SpriteRenderer>().sprite;
        iconoProximaBasura.sprite = dibujo;
    }

    // Esta funcion es la que llaman los botes cuando destruyen una basura
    public void GenerarBasuraEnMesa()
    {
        // 1. Creamos la basura que estaba en "Siguiente" en el punto de lanzamiento
        basuraActual = Instantiate(siguientePrefab, puntoDeLanzamiento.position, Quaternion.identity);

        // 2. Inmediatamente despues, preparamos la que sigue para actualizar la UI
        PrepararSiguienteBasura();
    }
}