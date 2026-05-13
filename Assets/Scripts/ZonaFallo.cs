using UnityEngine;

public class ZonaFallo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otroColisionador)
    {
        // Si choca con cualquier cosa que tenga el script DatosResiduo (cualquier basura)
        if (otroColisionador.GetComponent<DatosResiduo>() != null)
        {
            Debug.Log("Tiro fallado, salio del mapa.");
            Destroy(otroColisionador.gameObject);

            // Le pedimos al jefe que mande la siguiente basura
            GestorColaBasura.Instancia.GenerarBasuraEnMesa();
        }
    }
}