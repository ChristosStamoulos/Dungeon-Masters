using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager = null;

    public GameObject gameOverPanel;
    public GameObject mainMenuPanel;
    public GameObject gameCamera;

    private UIManager UIManager
    {
        get
        {
            if (_uiManager == null)
            {
                _uiManager = FindAnyObjectByType<UIManager>();
            }

            return _uiManager;
        }
    }

    private void Start()
    {
        UIManager.SetTreasureCount(GetTreasuresLeft());
        EnableControls(false);
        SetCameraToInitialPosition();
    }

    public void SetCameraToInitialPosition()
    {
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();

        if (cameraFollow != null)
        {
            cameraFollow.target = GameObject.FindGameObjectWithTag("MainDragon").transform;
            cameraFollow.offset = new Vector3(0.0f, 1.5f, -9.5f);
        }
    }

    public void MakeCameraFollowPlayer()
    {
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();

        if (cameraFollow != null)
        {
            cameraFollow.target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void EnableControls(bool enable)
    {
        InputManager inputManager = FindAnyObjectByType<InputManager>();

        if (inputManager != null)
        {
            inputManager.Enable(enable);
        }
    }

    public int GetTreasuresLeft()
    {
        return GameObject.FindGameObjectsWithTag("Treasure").Length;
    }

    public void OnPickupTreasure()
    {
        int treasurseLeft = GetTreasuresLeft();

        UIManager.SetTreasureCount(treasurseLeft);

        if (GetTreasuresLeft() <= 0)
        {
            Win();
        }
    }

    public void Win()
    {

    }

    public void Begin()
    {
        mainMenuPanel.SetActive(false);
        MakeCameraFollowPlayer();
        Resume();
        EnableControls(true);
        UIManager.ShowGameUI(true);
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Pause();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void MainMenu()
    {
        Restart();
        gameOverPanel.SetActive(false);
        UIManager.ShowGameUI(false);
        mainMenuPanel.SetActive(true);
        EnableControls(false);
        SetCameraToInitialPosition();
    }

    public void Quit()
    {
        Application.Quit();
    }
}