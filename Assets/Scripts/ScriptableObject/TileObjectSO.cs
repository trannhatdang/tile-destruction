using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileInfo {
	public TileInfo(TileColor col, Vector2 pos)
	{
		this.col = col;
		this.pos = pos;
	}

	TileColor col;
	Vector2 pos;
}

[CreateAssetMenu(fileName = "TileObjectSO", menuName = "ScriptableObjects/TileObjectSO")]
public class TileObjectSO : ScriptableObject
{
	[SerializeField] List<TileInfo> m_tiles;
	[SerializeField] string m_name;

	public List<TileInfo> Tiles{
		get { return m_tiles; }
	}

	public string Name {
		get { return m_name; }
		set { m_name = value; }
	}

	public void SaveTileObject(Tile tile)
	{
		m_tiles.Clear();

		AddTile(tile);
	}

	public void AddTile(Tile tile)
	{
		TileInfo newTile = new TileInfo(tile.Color, tile.Position);
		if(m_tiles.Contains(newTile)) return;
		m_tiles.Add(newTile);

		if(tile.LeftTile)
		{

		}

		if(tile.RightTile)
		{

		}

		if(tile.DownTile)
		{

		}

		if(tile.TopTile)
		{

		}
	}

	public void ClearTiles()
	{
		m_tiles.Clear();
	}

	public void Load()
	{

	}
}
