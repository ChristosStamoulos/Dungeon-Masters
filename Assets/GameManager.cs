using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager = null;
    private MusicManager _musicManager = null;
    private bool _isGameRunning = false;

    public GameObject gameOverPanel;
    public GameObject WinPanel;
    public GameObject mainMenuPanel;
    public GameObject gameCamera;
    public GameObject freeLookCamera;
    public GameObject secretRoom;

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

    private MusicManager MusicManager
    {
        get
        {
            if (_musicManager == null)
            {
                _musicManager = FindAnyObjectByType<MusicManager>();   
            }

            return _musicManager;
        }
    }

    private void Start()
    {
        UIManager.SetTreasureCount(GetTreasuresLeft());
        EnableControls(false);
        EnableCamera(false);
        CameraFollowObject(GameObject.FindGameObjectWithTag("MainDragon"));
        MusicManager.Play();
    }

    #region Camera

    public void CameraFollowObject(GameObject go)
    {
        if (freeLookCamera != null)
        {
            Vector3 targetPosition = go.transform.position;
            targetPosition.z -= 8;
            targetPosition.y += 1.5f;

            Transform target = go.transform;
            CinemachineFreeLook freeLook = freeLookCamera.GetComponent<CinemachineFreeLook>();

            Camera.main.transform.position = targetPosition;

            freeLook.Follow = target;
            freeLook.LookAt = target;
        }
    }

    public void EnableCamera(bool enable)
    {
        if (freeLookCamera != null)
        {
            freeLookCamera.SetActive(enable);
        }
    }

    #endregion

    #region Controls

    public void EnableControls(bool enable)
    {
        InputManager inputManager = FindAnyObjectByType<InputManager>();

        if (inputManager != null)
        {
            inputManager.Enable(enable);
        }
    }

    #endregion

    #region Game

    public int GetTreasuresLeft()
    {
        int treasurseLeft = GameObject.FindGameObjectsWithTag("Treasure").Length;

        if (!secretRoom.activeSelf)
        {
             treasurseLeft+= 1;
        }

        return treasurseLeft;
    }

    public void OnPickupTreasure()
    {
        int treasurseLeft = GetTreasuresLeft();

        UIManager.SetTreasureCount(treasurseLeft);

        if (treasurseLeft == 1)
        {
            GameObject secretWall = GameObject.FindGameObjectWithTag("SecretWall");

            if (secretWall != null)
            {
                secretWall.SetActive(false);
                secretRoom.SetActive(true);
            }
        }

        else if (treasurseLeft <= 0)
        {
            Win();
        }
    }

    public void Win()
    {
        WinPanel.SetActive(true);
        EnableCamera(false);
        Pause();
    }

    public void GameOver()
    {
        MusicManager.Stop();
        gameOverPanel.SetActive(true);
        EnableCamera(false);
        Pause();
    }

    #endregion

    #region Game Flow

    public void Begin()
    {
        mainMenuPanel.SetActive(false);
        CameraFollowObject(GameObject.Find("CameraFocus"));
        Resume();
        EnableControls(true);
        EnableCamera(true);
        UIManager.ShowGameUI(true);
        _isGameRunning = true;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        EnableCamera(false);
        _isGameRunning = false;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        EnableCamera(true);
        _isGameRunning = true;
    }

    public void Restart()
    {
        _isGameRunning = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void MainMenu()
    {
        _isGameRunning = false;
        Restart();
        gameOverPanel.SetActive(false);
        WinPanel.SetActive(false);
        UIManager.ShowGameUI(false);
        mainMenuPanel.SetActive(true);
        EnableControls(false);
        EnableCamera(false);
        CameraFollowObject(GameObject.FindGameObjectWithTag("MainDragon"));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public bool IsGameRunning()
    {
        return _isGameRunning;
    }

    #endregion   
}