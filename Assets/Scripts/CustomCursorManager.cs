using UnityEngine;

public class CustomCursorManager : MonoBehaviour
{
    public bool isCustomCursorActive = true;

    private static CustomCursorManager _instance;
    public static CustomCursorManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CustomCursorManager", typeof(CustomCursorManager)).GetComponent<CustomCursorManager>();
            }

            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCursor(Texture2D cursorTexture)
    {
        if (cursorTexture == null)
        {
            isCustomCursorActive = false;
        }
        else
        {
            isCustomCursorActive = true;
        }
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
