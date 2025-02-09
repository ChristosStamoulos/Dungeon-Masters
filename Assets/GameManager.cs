using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager = null;

    public GameObject gameOverPanel;
    public GameObject mainMenuPanel;
    public GameObject gameCamera;
    public GameObject freeLookCamera;

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
        EnableCamera(false);
        CameraFollowObject(GameObject.FindGameObjectWithTag("MainDragon"));
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
        return GameObject.FindGameObjectsWithTag("Treasure").Length;
    }

    public void OnPickupTreasure()
    {
        int treasurseLeft = GetTreasuresLeft();

        UIManager.SetTreasureCount(treasurseLeft);

        if (treasurseLeft == 1)
        {
            GameObject.FindGameObjectWithTag("SecretWall").SetActive(false);
            GameObject.Find("SecretRoom").SetActive(true);
        }

        else if (treasurseLeft <= 0)
        {
            Win();
        }
    }

    public void Win()
    {

    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        EnableCamera(false);
        Pause();
    }

    #endregion

    #region Game Flow

    public void Begin()
    {
        Debug.Log("Begin()");

        mainMenuPanel.SetActive(false);
        CameraFollowObject(GameObject.Find("CameraFocus"));
        Resume();
        EnableControls(true);
        EnableCamera(true);
        UIManager.ShowGameUI(true);
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        EnableCamera(false);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        EnableCamera(true);
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
        EnableCamera(false);
        CameraFollowObject(GameObject.FindGameObjectWithTag("MainDragon"));
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion   
}