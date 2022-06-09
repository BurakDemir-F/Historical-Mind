using Patterns;
using UnityEngine;

namespace UI
{
    public class CursorManager : Singleton<CursorManager>
    {
        [SerializeField]private Texture2D cursorTexture;
        [SerializeField]private CursorMode cursorMode = CursorMode.Auto;
        [SerializeField]private Vector2 hotSpot = Vector2.zero;

        private void Start()
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }
}
