﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour {
	Grid grid;
	PathRequestManager requestManager;

	private void Awake() {
		grid = GetComponent<Grid>();
		requestManager = GetComponent<PathRequestManager>();
	}

	public void StartFindPath(Vector2 startPos, Vector2 targetPos) {
		StartCoroutine(FindPath(startPos, targetPos));
	}

	IEnumerator FindPath(Vector2 startPos, Vector2 targetPos) {
		Stopwatch sw = new Stopwatch();
		sw.Start();

		Vector2[] waypoints = new Vector2[0];
		bool pathSuccess = false;

		Node startNode = grid.nodeFromWorldPoint(startPos);
		Node targetNode = grid.nodeFromWorldPoint(targetPos);

		if (startNode.walkable && targetNode.walkable) {
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);

				if (currentNode == targetNode) {
					sw.Stop();
					print("Path found in: " + sw.ElapsedMilliseconds + " ms");
					pathSuccess = true;
					break;
				}

				foreach (Node neighbor in grid.getNeighbors(currentNode)) {
					if (!neighbor.walkable || closedSet.Contains(neighbor)) {
						continue;
					}

					int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
					if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor)) {
						neighbor.gCost = newMovementCostToNeighbor;
						neighbor.hCost = GetDistance(neighbor, targetNode);
						neighbor.parent = currentNode;

						if (!openSet.Contains(neighbor)) {
							openSet.Add(neighbor);
						} else {
							openSet.UpdateItem(neighbor);
						}
					}
				}
			}
		}
		yield return null;
		if (pathSuccess) {
			waypoints = RetracePath(startNode, targetNode);
		}
		requestManager.FinishedProcessingPath(waypoints, pathSuccess);
	}

	Vector2[] RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		Vector2[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
	}

	Vector2[] SimplifyPath(List<Node> path) {
		List<Vector2> waypoints = new List<Vector2>();
		Vector2 directionOld = Vector2.zero;

		for (int i = 1; i < path.Count; i++) {
			Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);

			if(directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			directionOld = directionNew;
		}

		return waypoints.ToArray();
	}

	int GetDistance(Node a, Node b) {
		int distX = Mathf.Abs(a.gridX - b.gridX);
		int distY = Mathf.Abs(a.gridY - b.gridY);

		if (distX > distY)
			return 14 * distY + 10 * (distX - distY);
		return 14 * distX + 10 * (distY - distX);
	}
}