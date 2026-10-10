using UnityEngine;

public class FindGround : MonoBehaviour
{
    private PlayerWalking _player;

    void Start()
    {
        _player = GetComponentInParent<PlayerWalking>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && collision.gameObject.layer == gameObject.layer) _player.onGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && collision.gameObject.layer == gameObject.layer) _player.onGround = false;
    }
}
