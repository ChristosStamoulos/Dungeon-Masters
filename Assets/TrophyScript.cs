using UnityEngine;

public class TrophyScript : MonoBehaviour
{
    public GameObject TargetTrophyObject = null;

    private bool _hasScored = false;

    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!_hasScored && other.CompareTag("Player"))
        {
            PlayerState playerState = other.GetComponent<PlayerState>();

            if (playerState != null)
            {
                TargetTrophyObject.SetActive(false);
                playerState.OnPickupTreasure();
            }
        }
    }
}
