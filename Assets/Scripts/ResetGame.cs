using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// a script that checks if either the players has made contact with something kills them or if they have falling, so that the game can reset.
/// </summary>
public class ResetGame : MonoBehaviour
{
    float deathFall;
    [SerializeField] float fallenPlayer = 0;
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("FireBall")) || (other.gameObject.CompareTag("Enemy")))
        {
            SceneManager.LoadScene(0);
        }
    }
    void Update()
    {
        deathFall = transform.position.y;
        if(deathFall <= fallenPlayer)
        {
            SceneManager.LoadScene(0);
        }
    }
}
