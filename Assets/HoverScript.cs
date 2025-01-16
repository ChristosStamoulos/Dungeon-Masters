using System;
using UnityEngine;

public class HoverScript : MonoBehaviour
{
    private float initialVerticalPos;
    private float currentOffset = 0.0f;
    private float step = 0.0625f;
    public GameObject Target;

    public void Start()
    {
        initialVerticalPos = Target.transform.position.y;
    }

    public void FixedUpdate()
    {
        int timestampMilliseconds = DateTime.UtcNow.Millisecond;

        Target.transform.position = new Vector3(
            Target.transform.position.x,
            initialVerticalPos + currentOffset / 40.0f,
            Target.transform.position.z
        );

        currentOffset += step;

        if (currentOffset == 0.0f || currentOffset == 10.0f)
        {
            step = -step;
        }

        Target.transform.Rotate(0, 1, 0);
    }
}