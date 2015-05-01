using UnityEngine;
using System.Collections;

public class AIRacer : Racer {

    private TrackManager trackManager;
    private NavMeshAgent navAgent;

    private int waypointIndex = 0;

	// Use this for initialization
	protected override void Start () {
        base.Start();

        trackManager = FindObjectOfType<TrackManager>();
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    protected override void DoMovement() {
        Debug.Log("ASDF: " + Vector3.Distance(navAgent.destination, navAgent.transform.position));
        if (Vector3.Distance(navAgent.destination, navAgent.transform.position) < 1.0f) {
            Debug.Log("New destination");

            waypointIndex++;

            if (waypointIndex >= trackManager.waypoints.Length)
                waypointIndex = 0;

            navAgent.SetDestination(trackManager.waypoints[waypointIndex].transform.position);
        }
    }
}
