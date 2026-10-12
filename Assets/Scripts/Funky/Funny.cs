using UnityEngine;
using UnityEngine.Video;

public class Funny : MonoBehaviour
{
    [SerializeField] private GameObject _funny;
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _funny.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FunnyOn();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FunnyOff();
        }
    }

    private void FunnyOn()
    {
        if (_videoPlayer.targetTexture != null)
        {
            _videoPlayer.targetTexture.Release();
        }

        _funny.SetActive(true);
        _videoPlayer.Play();
    }

    private void FunnyOff()
    {
        _funny.SetActive(false);
        _videoPlayer.Stop();
    }
}
