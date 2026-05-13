using UnityEngine;

public class AroContenedor : MonoBehaviour
{
    public string categoriaAceptada; // Ej: "Organico"

    // Esta funcion nativa de Unity se activa cuando una fisica (basura) entra en nuestro Trigger (bote)
    void OnTriggerEnter2D(Collider2D otroColisionador)
    {
        // Verificamos si lo que entro tiene el script de la basura
        DatosResiduo residuo = otroColisionador.GetComponent<DatosResiduo>();

        if (residuo != null)
        {
            if (residuo.tipoDeResiduo == categoriaAceptada)
            {
                Debug.Log("¡Punto ganado!");
                if (GestorLanzamiento.Instancia != null)
                {
                    GestorLanzamiento.Instancia.SumarPunto();
                }
            }
            else
            {
                Debug.Log("Error: Bote equivocado");
            }

            // Destruimos la basura para que no se acumule
            Destroy(otroColisionador.gameObject);

            // Le pedimos al gestor que mande la siguiente basura
            GestorColaBasura.Instancia.GenerarBasuraEnMesa();
        }
    }
}