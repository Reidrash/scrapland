using UnityEngine;

public class MirarCamara : MonoBehaviour
{
    private Camera camara;

    void Start()
    {
        // Encontramos la camara principal del juego
        camara = Camera.main;
    }

    void LateUpdate()
    {
        // Obligamos al dibujo a mirar en la misma direccion que la camara
        // LateUpdate asegura que la camara se mueva primero y el dibujo reaccione despues
        transform.forward = camara.transform.forward;
    }
}