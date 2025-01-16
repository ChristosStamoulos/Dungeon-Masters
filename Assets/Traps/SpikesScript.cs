using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes_movement : Trap
{

    public GameObject Spikes;
    public float moveSpeed;
    public float wait_in;
    public float wait_out;

    void Start()
    {
        StartCoroutine(Move_Spikes());
    }

    IEnumerator Move_Spikes()
    {
        while (true)
        {

            Vector3 Starting_Position = new Vector3(Spikes.transform.position.x, Spikes.transform.position.y, Spikes.transform.position.z);
            Vector3 newPosition = Starting_Position;

            if (Spikes.transform.rotation.z == 0)
            {
                newPosition = new Vector3(Spikes.transform.position.x, Spikes.transform.position.y + 1, Spikes.transform.position.z);
            }

            if (Spikes.transform.rotation.eulerAngles.z == 90)
            {
                newPosition = new Vector3(Spikes.transform.position.x, Spikes.transform.position.y, Spikes.transform.position.z + 1);
            }

            if (Spikes.transform.rotation.eulerAngles.z == 180)
            {
                newPosition = new Vector3(Spikes.transform.position.x, Spikes.transform.position.y - 1, Spikes.transform.position.z);
            }

            if (Spikes.transform.rotation.eulerAngles.z == 270)
            {
                newPosition = new Vector3(Spikes.transform.position.x, Spikes.transform.position.y, Spikes.transform.position.z - 1);
            }

            if (Spikes.transform.rotation.eulerAngles.z == 90 && Spikes.transform.rotation.eulerAngles.y == 180)
            {
                newPosition = new Vector3(Spikes.transform.position.x + 1, Spikes.transform.position.y, Spikes.transform.position.z);
            }

            if (Spikes.transform.rotation.eulerAngles.z == 180 && Spikes.transform.rotation.eulerAngles.y == 180)
            {
                newPosition = new Vector3(Spikes.transform.position.x, Spikes.transform.position.y - 1, Spikes.transform.position.z);
            }

            if (Spikes.transform.rotation.eulerAngles.z == 270 && Spikes.transform.rotation.eulerAngles.y == 180)
            {
                newPosition = new Vector3(Spikes.transform.position.x - 1, Spikes.transform.position.y, Spikes.transform.position.z);
            }

            while (Spikes.transform.position != newPosition)
            {
                Spikes.transform.position = Vector3.MoveTowards(Spikes.transform.position, newPosition, moveSpeed * Time.deltaTime);
                yield return 0;
            }

            yield return new WaitForSeconds(wait_out);

            while (Spikes.transform.position != Starting_Position)
            {
                Spikes.transform.position = Vector3.MoveTowards(Spikes.transform.position, Starting_Position, moveSpeed * Time.deltaTime);
                yield return 0;
            }

            yield return new WaitForSeconds(wait_in);
        }
    }
}