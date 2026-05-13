using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LineRenderer))] // Esto asegura que Unity no borre nuestro componente de linea
public class LanzadorBasura : MonoBehaviour
{
    [Header("Ajustes de Fuerza")]
    public float multiplicadorFuerza = 3f;
    public float fuerzaMaxima = 15f;

    [Header("Ajustes de Trayectoria")]
    public int puntosDeLinea = 15; // Cuantos segmentos tendra nuestra curva
    public float separacionPuntos = 0.1f; // Que tan lejos calculamos el futuro

    private Vector2 puntoInicioRaton;
    private Rigidbody2D rb;
    private Collider2D colisionador;
    private LineRenderer linea; // Referencia a la linea dibujada

    private bool estaSiendoArrastrado = false;
    private bool yaFueLanzado = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colisionador = GetComponent<Collider2D>();
        linea = GetComponent<LineRenderer>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        linea.enabled = false; // Mantenemos la linea apagada hasta que hagamos clic
    }

    void Update()
    {
        // Si el gestor dice que el juego ya no esta activo, bloqueamos el script
        if (GestorLanzamiento.Instancia != null && !GestorLanzamiento.Instancia.juegoActivo)
        {
            linea.enabled = false; // Apagamos la guia visual
            return;
        }

        if (yaFueLanzado || Mouse.current == null) return;

        // 1. Al presionar el clic
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 posicionPixel = Mouse.current.position.ReadValue();
            Vector2 posicionRaton = Camera.main.ScreenToWorldPoint(posicionPixel);

            if (colisionador.OverlapPoint(posicionRaton))
            {
                estaSiendoArrastrado = true;
                puntoInicioRaton = posicionRaton;
                linea.enabled = true; // Prendemos la linea
            }
        }

        // 2. MIENTRAS mantenemos presionado (AQUI DIBUJAMOS LA LINEA)
        if (Mouse.current.leftButton.isPressed && estaSiendoArrastrado)
        {
            Vector2 posicionPixel = Mouse.current.position.ReadValue();
            Vector2 puntoFinalRaton = Camera.main.ScreenToWorldPoint(posicionPixel);

            Vector2 direccionLanzamiento = puntoInicioRaton - puntoFinalRaton;
            direccionLanzamiento = Vector2.ClampMagnitude(direccionLanzamiento, fuerzaMaxima);

            // Llamamos a nuestra nueva funcion matematica
            DibujarTrayectoria(direccionLanzamiento * multiplicadorFuerza);
        }

        // 3. Al soltar el clic
        if (Mouse.current.leftButton.wasReleasedThisFrame && estaSiendoArrastrado)
        {
            estaSiendoArrastrado = false;
            linea.enabled = false; // Apagamos la linea porque ya disparamos

            Vector2 posicionPixel = Mouse.current.position.ReadValue();
            Vector2 puntoFinalRaton = Camera.main.ScreenToWorldPoint(posicionPixel);

            Vector2 direccionLanzamiento = puntoInicioRaton - puntoFinalRaton;
            direccionLanzamiento = Vector2.ClampMagnitude(direccionLanzamiento, fuerzaMaxima);

            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(direccionLanzamiento * multiplicadorFuerza, ForceMode2D.Impulse);
            rb.AddTorque(-direccionLanzamiento.x * 0.5f, ForceMode2D.Impulse);

            yaFueLanzado = true;
        }
    }

    // Funcion encargada de predecir la fisica
    void DibujarTrayectoria(Vector2 fuerzaInicial)
    {
        Vector2[] puntos = new Vector2[puntosDeLinea];
        linea.positionCount = puntosDeLinea; // Le decimos a la linea cuantos nodos tendra

        Vector2 posicionInicialObj = transform.position;
        // La velocidad inicial real es la fuerza aplicada dividida entre la masa del objeto
        Vector2 velocidadInicial = fuerzaInicial / rb.mass;

        for (int i = 0; i < puntosDeLinea; i++)
        {
            // Calculamos el tiempo en el futuro para este punto en especifico
            float tiempo = i * separacionPuntos;

            // Formula clasica de caida parabolica
            puntos[i] = posicionInicialObj + (velocidadInicial * tiempo) + 0.5f * (Physics2D.gravity * rb.gravityScale) * (tiempo * tiempo);
        }

        // Convertimos nuestras matematicas 2D a las coordenadas 3D que usa el LineRenderer
        for (int i = 0; i < puntosDeLinea; i++)
        {
            linea.SetPosition(i, new Vector3(puntos[i].x, puntos[i].y, 0));
        }
    }
}