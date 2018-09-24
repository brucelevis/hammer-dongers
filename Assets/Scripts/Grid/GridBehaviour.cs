﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CreativeSpore.SuperTilemapEditor;

public class GridBehaviour : MonoBehaviour {

	public Vector2[] spawnPoints = { new Vector2(-7, 0.4f), new Vector2(7, 0.4f)};
	List<List<TileBehaviour>> subsets;
	STETilemap tilemap;
	TileMatrix matrix;

	void SetMatrix ()
	{
		var tiles = new List<TileBehaviour> ();
		foreach (Transform child in transform){
			var tile = child.GetComponent<TileBehaviour> ();
			if(tile != null)
				tiles.Add(tile);
		}

		matrix = new TileMatrix (tiles);
	}
	
	void Start() {
		subsets = new List<List<TileBehaviour>> ();
		tilemap = GameObject.Find("Grid").GetComponent<STETilemap>();
		SetMatrix ();
	}

	public void Crack (TileBehaviour origin)
	{
		Clear ();
		TileBehaviour[] adjacents = GetAdjacents (origin);

		foreach(TileBehaviour tile in adjacents) {
			if (tile != null && !(tile.cracked || IsContained (tile))) {
				subsets.Add (GetSet (tile, new List<TileBehaviour>()));
			}
		}

		if (!(subsets.Count > 1))
			return;
		
		int max = subsets [0].Count;

		foreach (var tiles in subsets)
			if (tiles.Count > max)
				max = tiles.Count;

		float offset = 0;
		foreach (var tiles in subsets)
			if (tiles.Count < max) {
				tiles.Sort ((a, b) => {
					return (int)Mathf.Floor(Vector2.Distance(origin.transform.position, a.transform.position) 
					- Vector2.Distance(origin.transform.position, b.transform.position));
				});
				foreach (var tile in tiles) {
					tile.InvokeCrack (offset);
					offset += 0.1f;
				}
			}
	}

	List<TileBehaviour> GetSet (TileBehaviour origin, List<TileBehaviour> visited)
	{
		List<TileBehaviour> result = new List<TileBehaviour> ();
		TileBehaviour[] adjacents = GetAdjacents (origin);

		if(!IsContained(origin, visited))
			result.Add (origin);	

		foreach(TileBehaviour tile in adjacents) {
			if (tile != null && !(tile.cracked || IsContained (tile, visited))) {
				visited = visited.Concat (result).ToList ();
				result = result.Concat (GetSet (tile, visited)).ToList();
			}
		}
		return result;
	}

	TileBehaviour[] GetAdjacents (TileBehaviour origin)
	{
		return new [] {
			GetTile (origin.x, origin.y + 1),
			GetTile (origin.x, origin.y - 1),
			GetTile (origin.x - 1, origin.y),
			GetTile (origin.x + 1, origin.y)
		};
	}

	bool IsContained (TileBehaviour target)
	{
		foreach(List<TileBehaviour> tiles in subsets) 
			if (IsContained(target, tiles))
				return true;
		return false;
	}

	bool IsContained (TileBehaviour target, List<TileBehaviour> tiles)
	{
		foreach (TileBehaviour tile in tiles)
			if (tile.Equals (target))
				return true;
		return false;
	}



	private TileBehaviour GetTile(float x, float y) {
		return matrix.GetTile (x, y);
	}

	void Clear ()
	{
		foreach (List<TileBehaviour> tiles in subsets)
			tiles.Clear ();
		subsets.Clear ();
	}
}