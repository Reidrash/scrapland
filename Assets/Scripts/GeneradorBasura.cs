using UnityEngine;

public class GeneradorBasura : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject[] prefabsDeBasura; // Aquí pondrás todos tus prefabs (manzanas, botellas, etc)
    public Transform zonaDeJuego; // El Canvas para que aparezcan dentro de la interfaz
    public float tiempoEntreApariciones = 1.5f; // Cada segundo y medio sale uno nuevo

    private float temporizador;

    void Update()
    {
        // Solo generar basura si el tiempo del juego no se ha acabado
        if (MinijuegoManager.Instancia != null && MinijuegoManager.Instancia.tiempoRonda <= 0)
            return;

        temporizador -= Time.deltaTime;
        if (temporizador <= 0)
        {
            GenerarObjeto();
            temporizador = tiempoEntreApariciones; // Reiniciar el reloj
        }
    }

    void GenerarObjeto()
    {
        if (prefabsDeBasura.Length == 0) return; // Evitar errores si olvidaste poner los prefabs

        // 1. Elegir un prefab al azar de tu lista
        int indiceAleatorio = Random.Range(0, prefabsDeBasura.Length);
        GameObject nuevaBasura = Instantiate(prefabsDeBasura[indiceAleatorio], zonaDeJuego);

        // 2. Colocarlo arriba de la pantalla en una posición horizontal (X) aleatoria
        RectTransform rect = nuevaBasura.GetComponent<RectTransform>();

        // Nuestro canvas mide 1920 de ancho. Lo ponemos entre -800 y 800 para que no salga pegado a los bordes.
        // Y lo ponemos en Y=600 para que nazca justo arriba del borde superior de la pantalla.
        float posXAleatoria = Random.Range(-800f, 800f);
        rect.anchoredPosition = new Vector2(posXAleatoria, 600f);
    }
}