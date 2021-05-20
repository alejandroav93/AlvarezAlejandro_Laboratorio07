using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public float maxwait = 2.0f;
    float waitTime = 0.0f;
    public Transform WPContainer;
    int currentWP = 0;
    List<Transform> waypoints;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waypoints = new List<Transform>();
        foreach (Transform child in WPContainer)
            waypoints.Add(child);
        currentWP = Random.Range(0, waypoints.Count);
        agent.SetDestination(waypoints[currentWP].position);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance < 0.2f)
        {
            waitTime += Time.deltaTime;
            if (waitTime >= maxwait)
            {

                waitTime = 0.0f;
                int newWP = currentWP;
                while (newWP == currentWP)
                {
                    newWP = Random.Range(0, waypoints.Count);
                    
                }
                currentWP = newWP;
                agent.SetDestination(waypoints[currentWP].position);
            }

        }
    }
}
