using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int Damage { get; set; } = 1;

    private DateTime _lastCollisionTimestamp = DateTime.MinValue;

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
}
