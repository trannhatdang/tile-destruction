using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class TileInfo {
	public TileInfo(TileColor col, Vector2 pos)
	{
		this.Color = col;
		this.Position = pos;
	}

	public TileColor Color;
	public Vector2 Position;
}

[CreateAssetMenu(fileName = "TileObjectSO", menuName = "ScriptableObjects/TileObjectSO")]
public class TileObjectSO : ScriptableObject
{
	[SerializeField] List<TileInfo> m_tiles = new List<TileInfo>();
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
		foreach(var tileinfo in m_tiles)
		{
			if(tileinfo.Position == tile.Position)
			{
				return;
			}
		}

		TileInfo newTile = new TileInfo(tile.Color, tile.Position);

		tile.gameObject.name = m_name;
		m_tiles.Add(newTile);

		if(tile.GetLeftTile())
		{
			AddTile(tile.GetLeftTile());
		}

		if(tile.GetRightTile())
		{
			AddTile(tile.GetRightTile());
		}

		if(tile.GetTopTile())
		{
			AddTile(tile.GetTopTile());
		}

		if(tile.GetDownTile())
		{
			AddTile(tile.GetDownTile());
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
