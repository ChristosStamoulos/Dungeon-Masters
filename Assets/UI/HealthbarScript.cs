using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class HealthbarScript : MonoBehaviour
{
    private int _currentHearts = 0;
    private static List<GameObject> instantiatedHeartPrefabs = new List<GameObject>();

    public GameObject healthBar;
    public GameObject heartPrefab;

    public void SetHearts(int newHearts)
    {
        if (newHearts < 0)
        {
            throw new ApplicationException("Hearts cannot be negative");
        }

        if (_currentHearts < newHearts)
        {
            for (int i = _currentHearts; i < newHearts; ++i)
            {
                GameObject createdHeart = Instantiate(heartPrefab, new Vector3(i * 64 + 32, 550.0f, 0), Quaternion.identity);
                createdHeart.transform.parent = healthBar.transform;
                instantiatedHeartPrefabs.Add(createdHeart);
            }
        }

        else if (_currentHearts > newHearts)
        {
            Debug.Log(newHearts);

            for (int i = newHearts; i < instantiatedHeartPrefabs.Count; ++i)
            {
                Destroy(instantiatedHeartPrefabs[i]);
            }

            if (newHearts == 0)
            {
                Destroy(instantiatedHeartPrefabs[0]);
                instantiatedHeartPrefabs.Clear();
            }

            else
            {
                instantiatedHeartPrefabs.RemoveRange(newHearts, _currentHearts - newHearts);
            }
            
        }

        _currentHearts = newHearts;
    }

    public void Show(bool shouldShow)
    {
        healthBar.SetActive(shouldShow);
    }
}