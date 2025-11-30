using UnityEngine;

/// <summary>
/// a check if a player collides with any of these to play a sound clip
/// </summary>
public class PlayerSoundColliding : MonoBehaviour
{
    [SerializeField] AudioClip playerCollide;
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Wall")) || (collision.gameObject.CompareTag("Rock")) || (collision.gameObject.CompareTag("Enemy")))
        {
            AudioSource.PlayClipAtPoint(playerCollide, transform.position);
        }
    }
}
