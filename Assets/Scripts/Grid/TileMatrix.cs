using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileMatrix {

	public TileBehaviour [,] matrix;
	public float yOffset;
	public float xOffset;
	public int rows;
	public int columns;

	public TileMatrix(List<TileBehaviour> tiles) {
		tiles.Sort (CompareTileCoordinates);

		var xCoordinates = tiles.Select (t => t.x);
		var yCoordinates = tiles.Select (t => t.y);

		var xMax = xCoordinates.Max ();
		var xMin = xCoordinates.Min ();

		var yMax = yCoordinates.Max ();
		var yMin = yCoordinates.Min ();

		rows = (int)(yMax - yMin);
		columns = (int)(xMax - xMin);

		matrix = new TileBehaviour[columns + 1, rows + 1];

		yOffset = yMin;
		xOffset = xMin;

		for (int x = 0; x <= columns; ++x)
			for (int y = 0; y <= rows; ++y) 
				foreach (var tile in tiles) {
					if (tile.x - xOffset == x && tile.y   - yOffset == y)
						matrix [x,y] = tile;
				}
	}

	public TileBehaviour GetRandomTile () {
		int x = Random.Range (0, columns + 1);
		int y = Random.Range (0, rows + 1);
		TileBehaviour tile = matrix [x, y];

		return tile == null || tile.Cracked ? GetRandomTile() : tile;
	}

	public TileBehaviour GetRandomAvailableTile() {
		TileBehaviour tile = GetRandomTile();

		return tile.Interactable != null ? GetRandomAvailableTile () : tile;
	}

	public static int CompareTileCoordinates(TileBehaviour a, TileBehaviour b) {
		if (a.x == b.x && a.y == b.y)
			return 0;

		if(a.x == b.x)
			return a.y > b.y ? 1 : -1;
		return a.x > b.x ? 1 : -1;
	}

	public TileBehaviour GetTile(float x, float y) {
		x = (x - xOffset);
		y = (y - yOffset);

		if (x < 0 || y < 0 || x > columns || y > rows)
			return null;
		
		return matrix [(int)x, (int)y];
	}
}
