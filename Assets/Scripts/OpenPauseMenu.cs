using UnityEngine;

public class OpenPauseMenu : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _pauseMenu.canOpenMenu)
        {
            _pauseMenu.openPause();
        }
    }
}
