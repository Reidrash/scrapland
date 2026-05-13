using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovimientoFicha : MonoBehaviour
{
    [Header("Configuracion del Camino")]
    public List<Transform> nodosCamino; // Arrastra tus Casilla_0, Casilla_1... aqui
    public float velocidadSalto = 5f;

    private int indiceCasillaActual = 0;
    private bool estaMoviendose = false;

    // Esta funcion es la que llamaremos desde el Dado
    public void OrdenDeMover(int pasos)
    {
        if (!estaMoviendose)
        {
            StartCoroutine(MoverPorCasillas(pasos));
        }
    }

    IEnumerator MoverPorCasillas(int pasos)
    {
        estaMoviendose = true;

        for (int i = 0; i < pasos; i++)
        {
            // Verificamos no salirnos del tablero
            if (indiceCasillaActual + 1 < nodosCamino.Count)
            {
                indiceCasillaActual++;
                Vector3 destino = nodosCamino[indiceCasillaActual].position;

                // Interpolacion: Movemos suavemente hasta la posicion destino
                while (Vector3.Distance(transform.position, destino) > 0.05f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, destino, velocidadSalto * Time.deltaTime);
                    yield return null; // Espera al siguiente frame
                }
            }
        }

        estaMoviendose = false;
        Debug.Log("Movimiento terminado en casilla: " + indiceCasillaActual);
    }
}