using UnityEngine;
using UnityEngine.EventSystems;

public class ResiduoDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 posicionInicial;
    private Canvas canvasPrincipal; // Agregamos una referencia al Canvas

    public string tipoDeResiduo;
    public float velocidadCaida = 250f;
    private bool estaSiendoArrastrado = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        // Buscamos el Canvas en los objetos padres para saber su escala
        canvasPrincipal = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        if (!estaSiendoArrastrado)
        {
            rectTransform.anchoredPosition += Vector2.down * velocidadCaida * Time.deltaTime;

            if (rectTransform.anchoredPosition.y < -700f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        estaSiendoArrastrado = true;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.8f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // EL TRUCO: Dividimos el delta del raton entre el factor de escala del Canvas
        rectTransform.anchoredPosition += eventData.delta / canvasPrincipal.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        estaSiendoArrastrado = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}