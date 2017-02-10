using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class PathRequestManager : MonoBehaviour {
	Queue<PathResult> results = new Queue<PathResult>();

	static PathRequestManager instance;
	Pathfinding pathfinding;


	private void Awake() {
		instance = this;
		pathfinding = GetComponent<Pathfinding>();
	}

	private void Update() {
		if(results.Count > 0) {
			int itemsInQueue = results.Count;
			lock (results) {
				for(int i = 0; i < itemsInQueue; i++) {
					PathResult result = results.Dequeue();
					result.callback(result.path, result.success);
				}
			}
		}
	}

	public static void RequestPath(PathRequest request) {
		ThreadStart threadStart = delegate {
			instance.pathfinding.FindPath(request, instance.FinishedProcessingPath);
		};
		threadStart.Invoke();
	}

	public void FinishedProcessingPath(PathResult result) {
		lock (results) {
			results.Enqueue(result);
		}
	}
}


public struct PathRequest {
	public Vector2 pathStart;
	public Vector2 pathEnd;
	public Action<Vector2[], bool> callback;

	public PathRequest(Vector2 start, Vector2 end, Action<Vector2[], bool> callback) {
		this.pathStart = start;
		this.pathEnd = end;
		this.callback = callback;
	}
}

public struct PathResult {
	public Vector2[] path;
	public bool success;
	public Action<Vector2[], bool> callback;

	public PathResult(Vector2[] path, bool success, Action<Vector2[], bool> callback) {
		this.path = path;
		this.success = success;
		this.callback = callback;
	}
}
