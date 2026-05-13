using UnityEngine;
using UnityEngine.InputSystem; // Importamos el nuevo sistema

public class ControladorCamara : MonoBehaviour
{
    [Header("Ajustes de Camara")]
    public float velocidadMovimiento = 5f;
    public float velocidadRotacion = 100f;

    void Update()
    {
        // Evitamos errores si no hay teclado conectado
        if (Keyboard.current == null) return;

        float moverAdelante = 0f;
        float moverLado = 0f;

        // Leer teclas WASD o Flechas
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moverAdelante = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moverAdelante = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moverLado = 1f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moverLado = -1f;

        // Aplicar movimiento
        moverAdelante *= velocidadMovimiento * Time.deltaTime;
        moverLado *= velocidadMovimiento * Time.deltaTime;
        transform.Translate(moverLado, 0, moverAdelante);

        // Leer teclas Q y E para rotar
        if (Keyboard.current.qKey.isPressed)
        {
            transform.Rotate(Vector3.up, -velocidadRotacion * Time.deltaTime, Space.World);
        }
        if (Keyboard.current.eKey.isPressed)
        {
            transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime, Space.World);
        }
    }
}