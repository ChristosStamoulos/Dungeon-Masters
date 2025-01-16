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

        if (Health <= 0)
        {
            GameManager.GameOver();
        }

        Debug.Log("Updated health: " + Health.ToString());
    }

    public void OnPickupTreasure()
    {
        GameManager.OnPickupTreasure();
    }
}