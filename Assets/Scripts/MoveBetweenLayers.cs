using UnityEngine;

public class MoveBetweenLayers : MonoBehaviour
{
    [Header("Position on z axis for background and foreground")]
    [SerializeField] private float _zForeground = 0f;
    [SerializeField] private float _zBackground = 2f;

    [Header("Fade for foreground")]
    [Range(0f, 1f)][SerializeField] private float _fadeAlpha = 0.2f;
    private bool _isAtForeground = true;
    public bool canChangeLayer;

    [Header("Player's sprite")]
    [SerializeField] private SpriteRenderer _playerSprite;
    private int _playerLayer;

    SpriteRenderer[] _allSprites;

    void Awake()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            gameObject.layer = LayerMask.NameToLayer("Foreground");
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Foreground");
            }
        }

        if (gameObject.layer == LayerMask.NameToLayer("Foreground")) _isAtForeground = true;
        else _isAtForeground = false;

        if (_playerSprite == null) Debug.LogError("Add player visual");

        _playerLayer = _playerSprite.sortingOrder;
    }

    void Start()
    {
        _allSprites = FindObjectsByType<SpriteRenderer>();

        SetForegroundVisible(_isAtForeground);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !_isAtForeground && canChangeLayer)
        {
            _isAtForeground = true;
            UpdateLayerAndPosition();
        }
        else if (Input.GetKeyDown(KeyCode.W) && _isAtForeground && canChangeLayer)
        {
            _isAtForeground = false;
            UpdateLayerAndPosition();
        }
    }
    private void UpdateLayerAndPosition()
    {
        float targetZ = _isAtForeground ? _zForeground : _zBackground;
        transform.position = new Vector3(transform.position.x, transform.position.y, targetZ);

        if (_isAtForeground)
        {
            gameObject.layer = LayerMask.NameToLayer("Foreground");
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Foreground");
            }
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Background");
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Background");
            }
        }

        SetForegroundVisible(_isAtForeground);
    }

    private void SetForegroundVisible(bool isVisible)
    {
        float targetAlpha = isVisible ? 1f : _fadeAlpha;
        int targetLayer = isVisible ? _playerLayer - 1 : _playerLayer + 1;

        foreach (SpriteRenderer sprite in _allSprites)
        {
            GameObject obj = sprite.gameObject;

            if (obj.layer == LayerMask.NameToLayer("Foreground") || obj.layer == LayerMask.NameToLayer("Door"))
            {
                Transform parent = obj.transform.parent;

                if (obj.CompareTag("Player") || parent.CompareTag("Player")) sprite.sortingOrder = targetLayer + 1;
                else if (obj.CompareTag("Background") || parent.CompareTag("Background")) sprite.sortingOrder = targetLayer - 1;
                else sprite.sortingOrder = targetLayer;

                Color currentColor = sprite.color;
                sprite.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);

                if (obj.layer == LayerMask.NameToLayer("Door")) sprite.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
                else if (obj.layer == LayerMask.NameToLayer("Foreground")) sprite.enabled = isVisible;
            }
        }
    }
}
