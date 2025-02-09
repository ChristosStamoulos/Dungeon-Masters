using System.Collections;
using TMPro;
using UnityEngine;

public class ElevatorTriggerScript : MonoBehaviour
{
    private GameObject elevatorObject;
    private bool hasMoved = false;

    public void Start()
    {
        elevatorObject = GameObject.Find("ElevatorFloor");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!hasMoved && other.CompareTag("Player") && elevatorObject != null)
        {
            hasMoved = true;
            MoveElevator();
        }
    }

    public void MoveElevator()
    {
        StartCoroutine(MoveElevatorCoroutine());
    }

    private IEnumerator MoveElevatorCoroutine()
    {
        Vector3 targetPosition = new Vector3(-20.0f, -9.0f, -4.0f);

        while (Vector3.Distance(elevatorObject.transform.position, targetPosition) > 0.01f)
        {
            elevatorObject.transform.position = Vector3.MoveTowards(
                elevatorObject.transform.position,
                targetPosition,
                Time.deltaTime
            );

            yield return null;
        }

        elevatorObject.transform.position = targetPosition;
    }
}
