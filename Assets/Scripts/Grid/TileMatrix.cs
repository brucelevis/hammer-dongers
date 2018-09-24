using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileMatrix {

	TileBehaviour [,] matrix;
	float yOffset;
	float xOffset;
	int rows;
	int columns;

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

		for (int x = 0; x < columns; ++x)
			for (int y = 0; y < rows; ++y) 
				foreach (var tile in tiles) {
					if (tile.x - xOffset == x && tile.y   - yOffset == y)
						matrix [x,y] = tile;
				}
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
