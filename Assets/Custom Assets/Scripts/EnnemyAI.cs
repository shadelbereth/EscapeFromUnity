using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.Audio;

public class EnnemyAI : MonoBehaviour {

    private AICharacterControl controller;
    public Transform waypoints;
    public Transform playert;
    private Transform last_target;
    private bool chasing;
    public AudioSource found;
    public AudioMixerSnapshot calm;
    public AudioMixerSnapshot angry;

    Transform SelectDestination () {
        Transform[] wps = waypoints.GetComponentsInChildren<Transform>();
        return wps[Random.Range(1, waypoints.transform.childCount + 1)];
    }

    void OnTriggerEnter (Collider other) {
        if (other.transform != controller.target) {
            return;
        }
        Transform destination = SelectDestination();
        while (destination == other.transform) {
            destination = SelectDestination();
        }
        controller.target = destination;
        last_target = controller.target;
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            //Destroy(other.gameObject);
            print("dead");
        }
    }

	// Use this for initialization
	void Start () {
	   controller = GetComponent<AICharacterControl>();
       controller.target = SelectDestination();
       last_target = controller.target;
       chasing = false;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, playert.position - transform.position, Color.red);
        RaycastHit hit;
        Vector3 facing = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, playert.position - transform.position, out hit, 10)) {
            if (playert == hit.collider.transform && Vector3.Angle(facing, playert.position - transform.position) <= 45) {
                if (! chasing) {
                    if (! found.isPlaying) {
                        found.Play();
                    }
                    angry.TransitionTo(1.0f);
                    controller.target = playert;
                    chasing = true;
                }
            } else {
                if (chasing) {
                    calm.TransitionTo(1.0f);
                    controller.target = last_target;
                    chasing = false;
                }
            }
        }
	}
}
