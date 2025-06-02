using UnityEngine;

public class BG : MonoBehaviour {
    [SerializeField] private SpriteRenderer backgroundSprite;

    private void Start()
    {
        ScaleBackground();
    }

    private void ScaleBackground()
    {
        if (backgroundSprite == null)
        {
            Debug.LogError("фона нет");
            return;
        }

        backgroundSprite.transform.localScale = Vector3.one;

        float spriteWidth = backgroundSprite.bounds.size.x;
        float spriteHeight = backgroundSprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * Camera.main.aspect;

        Vector3 newScale = backgroundSprite.transform.localScale;
        newScale.x = worldScreenWidth / spriteWidth;
        newScale.y = worldScreenHeight / spriteHeight;

        backgroundSprite.transform.localScale = newScale;
    }
}