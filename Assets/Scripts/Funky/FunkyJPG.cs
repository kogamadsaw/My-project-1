using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FunkyJPG : MonoBehaviour
{
    [SerializeField] private GameObject _funny;
    [SerializeField] private GameObject _skibidi;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _afterHappy;


    private GameObject[] _birthdayGuy;

    private void Awake()
    {
        _canvas.enabled = false;
        _skibidi.SetActive(false);
        _afterHappy.SetActive(false);

        _birthdayGuy = GameObject.FindGameObjectsWithTag("Player");
    }

    void OnEnable()
    {
        _videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnDisable()
    {
        _videoPlayer.loopPointReached -= OnVideoFinished;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerScreamer();
        }
    }

    private void TriggerScreamer()
    {
        _canvas.enabled = true;

        if (_videoPlayer.targetTexture != null)
        {
            _videoPlayer.targetTexture.Release();
        }

        _skibidi.SetActive(true);
        _videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (_funny != null) Destroy(_funny);
        Destroy(_skibidi);

        _afterHappy.SetActive(true);

        foreach (GameObject happyBirthday in _birthdayGuy)
        {
            Destroy(happyBirthday);
        }
    }

    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Tired()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

