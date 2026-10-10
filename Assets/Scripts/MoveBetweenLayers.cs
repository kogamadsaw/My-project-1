using UnityEngine;

public class MoveBetweenLayers : MonoBehaviour
{
    [SerializeField] private float _zForeground = 0f;
    [SerializeField] private float _zBackground = 2f;
    private bool _isAtForeground = true;
    public bool canChangeLayer;

    [SerializeField] private GameObject _foregroundLayerObject;
    [Range(0f, 1f)][SerializeField] private float _fadeAlpha = 0.2f;

    private SpriteRenderer[] _foregroundSprites;

    [SerializeField] private SpriteRenderer _playerSprite;

    private PlayerWalking _player;

    void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Foreground");
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Foreground");
        }

        if (_playerSprite == null) Debug.LogError("Add player visual");

        _player = GetComponentInParent<PlayerWalking>();
    }

    void Start()
    {
        if (_foregroundLayerObject != null)
        {
            _foregroundSprites = _foregroundLayerObject.GetComponentsInChildren<SpriteRenderer>();
        }
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
        float targetZ = _isAtForeground ?  _zForeground : _zBackground;
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
        if (_foregroundSprites == null) return;

        float targetAlpha = isVisible ? 1f : _fadeAlpha;
        int targetLayer = isVisible ? _playerSprite.sortingOrder - 1 : _playerSprite.sortingOrder + 1;

        foreach (var sprite in _foregroundSprites)
        {
            if (sprite != null)
            {
                Color currentColor = sprite.color;
                sprite.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);

                sprite.sortingOrder = targetLayer;
            }
        }
    }
}
