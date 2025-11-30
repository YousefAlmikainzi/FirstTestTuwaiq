using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// button to start the next scene
/// </summary>
public class StartButton : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
