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
        navAgent.speed = maxForwardVelocity;
        navAgent.acceleration = acceleration;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        if (speedBoost > 0) {
            navAgent.speed = maxForwardVelocity + speedBoost;
        }
	}

    protected override void DoMovement() {
        if (Vector3.Distance(navAgent.destination, navAgent.transform.position) < 5.0f) {
            waypointIndex++;

            if (waypointIndex >= trackManager.waypoints.Length)
                waypointIndex = 0;

            navAgent.SetDestination(trackManager.waypoints[waypointIndex].transform.position);
        }
    }

    protected override float RacerVelocity() {
        return navAgent.velocity.magnitude;
    }
}
