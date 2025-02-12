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
        float heartSize = Screen.width * 0.07f;
        float startX = Screen.width * 0.03f;
        float startY = Screen.height * 0.95f;

        if (newHearts < 0)
        {
            throw new ApplicationException("Hearts cannot be negative");
        }

        if (_currentHearts < newHearts)
        {
            for (int i = _currentHearts; i < newHearts; ++i)
            {
                Vector3 position = new Vector3(startX + (i * heartSize*0.5f), startY, 0);
                GameObject createdHeart = Instantiate(heartPrefab, position, Quaternion.identity, healthBar.transform);
                createdHeart.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
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