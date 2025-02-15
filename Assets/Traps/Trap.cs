using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private DateTime _lastCollisionTimestamp = DateTime.MinValue;
    private GameManager _gameManager;

    public int Damage { get; set; } = 1;

    public GameManager GameManager
    { 
        get
        {
            if (_gameManager == null)
            {
                _gameManager = GameObject.FindAnyObjectByType<GameManager>();
            }

            return _gameManager;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DateTime currentTimestamp = DateTime.UtcNow;
            TimeSpan previousCollisionInterval = currentTimestamp - _lastCollisionTimestamp;

            if (previousCollisionInterval.Seconds >= 3)
            {
                _lastCollisionTimestamp = currentTimestamp;
                DamagePlayer(other);
            }
        }
    }

    private void DamagePlayer(Collider playerCollider)
    {
        PlayerState playerState = playerCollider.GetComponent<PlayerState>();

        if (playerState != null)
        {
            playerState.Damage(Damage);
        }
    }

    protected bool IsWithinPlayerRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            return false;
        }

        return Vector3.Distance(player.transform.position, transform.position) < 10.0f;
    }

    protected bool AllowSounds()
    {
        return GameManager != null && GameManager.IsGameRunning() && IsWithinPlayerRange();
    }
}
