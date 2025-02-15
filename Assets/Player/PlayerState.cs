using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static int MaximumHealth = 10;

    private GameManager _gameManager = null;
    private UIManager _uiManager = null;

    public int Health { get; set; } = MaximumHealth;

    private GameManager GameManager
    {
        get
        {
            if (_gameManager == null)
            {
                _gameManager = FindAnyObjectByType<GameManager>();
            }

            return _gameManager;
        }
    }

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

    public void Damage(int damage)
    {
        Health -= damage;
        UIManager.SetHealth(Health);

        GameObject hurtObject = GameObject.Find("Hurt");

        if (hurtObject != null)
        {
            AudioSource hurtAudioSource = hurtObject.GetComponent<AudioSource>();

            if (hurtAudioSource != null)
            {
                hurtAudioSource.Play();
            }
        }

        if (Health <= 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                AudioSource runningAudioSource = player.GetComponent<AudioSource>();

                if (runningAudioSource != null)
                {
                    runningAudioSource.Stop();
                }
            }

            GameManager.GameOver();
        }
    }

    public void OnPickupTreasure()
    {
        GameManager.OnPickupTreasure();
    }
}