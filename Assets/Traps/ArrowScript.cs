using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows_movement : Trap
{

    public GameObject Arrow;
    public float moveSpeed;
    public float distance;

    void Start()
    {
        StartCoroutine(Move_Arrows());
    }

    IEnumerator Move_Arrows()
    {

        Vector3 Starting_Position = new Vector3(Arrow.transform.position.x, Arrow.transform.position.y, Arrow.transform.position.z);

        while (true)
        {

            Vector3 newPosition = Starting_Position;

            if (Arrow.transform.rotation.y == 0)
            {
                newPosition = new Vector3(Arrow.transform.position.x, Arrow.transform.position.y, Arrow.transform.position.z + distance);
            }
            else if (Arrow.transform.rotation.eulerAngles.y == 90)
            {
                newPosition = new Vector3(Arrow.transform.position.x + distance, Arrow.transform.position.y, Arrow.transform.position.z);
            }
            else if (Arrow.transform.rotation.eulerAngles.y == 180)
            {
                newPosition = new Vector3(Arrow.transform.position.x, Arrow.transform.position.y, Arrow.transform.position.z - distance);
            }
            else
            {
                newPosition = new Vector3(Arrow.transform.position.x - distance, Arrow.transform.position.y, Arrow.transform.position.z);
            }

            while (Arrow.transform.position != newPosition)
            {
                Arrow.transform.position = Vector3.MoveTowards(Arrow.transform.position, newPosition, moveSpeed * Time.deltaTime);
                yield return 0;
            }

            Arrow.transform.position = Starting_Position;
            yield return new WaitForSeconds(2);
        }
    }
}