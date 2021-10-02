using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField]
    private GameObject[] waypoints;

#pragma warning restore 0649

    private Queue<GameObject> pointsQueue;

    [SerializeField]
    private float speed = 2f;

    private void Start()
    {
        pointsQueue = new Queue<GameObject>(waypoints);
    }

    private void Update()
    {
        try
        {
            if (Vector2.Distance(pointsQueue.Peek().transform.position, transform.position) < .1f)
            {
                pointsQueue.Enqueue(pointsQueue.Dequeue()); // update first to last in queue
            }
        }
        catch
        {
            Start();
        }

        transform.position = Vector2.MoveTowards(transform.position, pointsQueue.Peek().transform.position, Time.deltaTime * speed);
    }
}
