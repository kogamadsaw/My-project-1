using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.GetComponent<PlayerWalking>().isCurrentPlayer)
        {
            collision.GetComponent<MoveBetweenLayers>().canChangeLayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<PlayerWalking>().isCurrentPlayer)
        {
            collision.GetComponent<MoveBetweenLayers>().canChangeLayer = false;
        }
    }
}
