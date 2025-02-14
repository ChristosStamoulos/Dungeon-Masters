using UnityEngine;

public class TrophyScript : MonoBehaviour
{
    public GameObject TargetTrophyObject = null;

    private bool _hasScored = false;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                audioSource.Play();
            }
        }
    }
}
