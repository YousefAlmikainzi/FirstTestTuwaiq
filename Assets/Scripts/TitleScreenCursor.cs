using UnityEngine;

/// <summary>
/// safety script so that the mouse doesnt get stuck on the first screen
/// </summary>
public class TitleScreenCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }
}
