using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIRacer : Racer {

    // DEBUG
    public bool Debugging;

    private bool destroyed;

    private TrackManager trackManager;
    private NavMeshAgent navAgent;

    private int waypointIndex = 0;
    private GameObject previousWaypoint;

    public GameObject[] ships;

	// Use this for initialization
	protected override void Start () {
        base.Start();

        trackManager = FindObjectOfType<TrackManager>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = maxForwardVelocity;
        navAgent.acceleration = acceleration;

        navAgent.SetDestination(trackManager.waypoints[waypointIndex].transform.position);
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        
        if (!racerInfo.CanMove())
        {
            navAgent.speed = 0;
        }
        else {
            navAgent.speed = maxForwardVelocity + speedBoost;
        }
        
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Racer")
        {
            Shoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (trackManager) {
            if (other.tag == "Waypoint" && other.gameObject != previousWaypoint) {
                if (Debugging) Debug.Log(gameObject.name + " Waypoint: " + other.gameObject.name);
                waypointIndex = (waypointIndex + 1) % trackManager.waypoints.Length;
                previousWaypoint = other.gameObject;

                navAgent.SetDestination(trackManager.waypoints[waypointIndex].transform.position);
            }
        }
    }

    protected override void DoMovement() {
        //Debug.Log("Brandon Sux");
    }

    protected override float RacerVelocity() {
        return navAgent.velocity.magnitude;
    }
}
