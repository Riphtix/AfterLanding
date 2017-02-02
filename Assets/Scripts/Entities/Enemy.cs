using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;

	public Transform target;
	public float speed = 5;
	public float turnSpeed = 3;
	public float turnDist = 5;

	Path path;

	private void Start() {
		StartCoroutine(UpdatePath());
	}

	public void OnPathFound(Vector2[] waypoints, bool pathSuccessful) {
		if (pathSuccessful) {
			path = new Path(waypoints, transform.position, turnDist);

			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath() {
		if (Time.timeSinceLevelLoad < .3f) {
			yield return new WaitForSeconds(.3f);
		}
		PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector2 targetPosOld = target.position;

		while (true) {
			yield return new WaitForSeconds(minPathUpdateTime);
			if (((Vector2) target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
				PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
				targetPosOld = target.position;
			}
		}
	}

	IEnumerator FollowPath() {
		bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt(path.lookPoints[0]);

		while (true) {
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
			while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D)) {
				if(pathIndex == path.finishLineIndex) {
					followingPath = false;
					break;
				} else {
					pathIndex++;
				}
			}

			if (followingPath) {
				Quaternion targetRotation = Quaternion.LookRotation((Vector3) path.lookPoints[pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.rotation = new Quaternion(0, 0, transform.rotation.z, 1);
				transform.position = Vector2.MoveTowards(transform.position, path.lookPoints[pathIndex], speed * Time.deltaTime);
			}

			yield return null;
		}
	}


	public void OnDrawGizmos() {
		if (path != null) {
			path.DrawWithGizmos();
		}
	}
}

