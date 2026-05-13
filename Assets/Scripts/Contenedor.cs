using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Contenedor : MonoBehaviour, IDropHandler
{
    // Define que acepta este bote en el Inspector (ej. "Organico")
    public string tipoAceptado;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ResiduoDraggable residuo = eventData.pointerDrag.GetComponent<ResiduoDraggable>();

            if (residuo != null)
            {
                if (residuo.tipoDeResiduo == tipoAceptado)
                {
                    // ¡Correcto!
                    MinijuegoManager.Instancia.SumarPuntos(10);
                    Destroy(residuo.gameObject); // Destruye la basura
                }
                else
                {
                    // ¡Incorrecto! Podrias restar puntos aqui si quieres
                    MinijuegoManager.Instancia.SumarPuntos(-5);
                    // Opcional: El residuo regresara a su lugar automaticamente gracias a OnEndDrag
                }
            }
        }
    }
}