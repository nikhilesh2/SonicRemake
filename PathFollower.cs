using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollower : MonoBehaviour {
	public List<Vector3> pathPoints = new List<Vector3>();
	public bool loop = true;
	private int pointToGoTo = 0, fromPoint;
	public float atPointThreshold = .3f;
	private float percentBetweenPoints = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, pathPoints [pointToGoTo]) <= atPointThreshold) {
			fromPoint = pointToGoTo;
			pointToGoTo++;
			percentBetweenPoints = 0;

				}
		if (loop && pointToGoTo >= pathPoints.Count) {
			fromPoint = pointToGoTo-1;
			pointToGoTo = 0;


				}
		percentBetweenPoints += Time.deltaTime/2;
		transform.position = Vector3.Lerp (pathPoints [fromPoint], pathPoints [pointToGoTo], percentBetweenPoints);
	
	}
}
