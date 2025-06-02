using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour {
    [SerializeField] private RectTransform targetUIElement;
    [SerializeField] private Vector3 worldOffset = new Vector3(0, 1, 0);
    private Camera uiCamera;
    private Vector3 initialOffset;
    void Start()
    {
        if (targetUIElement == null)
        {
            Debug.LogError("Не назначен целевой UI элемент!");
        }
        Canvas canvas = targetUIElement.GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
            {
                uiCamera = canvas.worldCamera;
            }
            else
            {
                uiCamera = Camera.main;
            }
        }
        initialOffset = transform.position - targetUIElement.position;
    }

    void Update()
    {
        if (targetUIElement != null)
        {
            Vector3 targetWorldPos = targetUIElement.position;
            Vector3 newPos = targetWorldPos + initialOffset;
            transform.position = newPos + worldOffset;
        }
    }
}
