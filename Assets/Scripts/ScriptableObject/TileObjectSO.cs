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

	private Tile m_parentTile;
	private Tile m_lastTile;
	private Rigidbody2D m_lastTileRB;

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

	public void Load()
	{
		foreach(var tile in m_tiles)
		{
			if(tile.Position != Vector2.zero)
			{
				continue;
			}

			GameObject selectedGB = new GameObject();
			selectedGB.tag = "Tile";
			selectedGB.name = m_name;

			m_parentTile = selectedGB.AddComponent<Tile>();
			selectedGB.AddComponent<SpriteRenderer>().sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(tile.Color));
			selectedGB.AddComponent<Rigidbody2D>();
			selectedGB.AddComponent<BoxCollider2D>();
			selectedGB.AddComponent<FixedJoint2D>().enabled = false;

			m_parentTile.TileObject = this;
			m_parentTile.Color = tile.Color;
			m_parentTile.Position = tile.Position;

			Selection.activeGameObject = selectedGB;

			break;
		}

		foreach(var tile in m_tiles)
		{
			if(tile.Position == Vector2.zero) 
			{
				continue;
			}

			GameObject newGB = new GameObject();
			newGB.tag = "Tile";
			newGB.transform.parent = m_parentTile.transform;
			newGB.transform.localPosition = Vector2.Scale(new Vector2(0.125f, 0.125f), tile.Position);
			newGB.name = m_name;

			if(m_lastTile)
			{
				m_lastTile.FloodTilesWithNewTile(newGB.AddComponent<Tile>());
			}
			else
			{
				newGB.AddComponent<Tile>();
			}

			newGB.AddComponent<SpriteRenderer>().sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(tile.Color));
			newGB.AddComponent<Rigidbody2D>();
			newGB.AddComponent<BoxCollider2D>();
			var joint = newGB.AddComponent<FixedJoint2D>();
			joint.connectedBody = m_lastTileRB;

			m_lastTileRB = newGB.GetComponent<Rigidbody2D>();

			m_lastTile = newGB.GetComponent<Tile>();
			m_lastTile.TileObject = this;
			m_lastTile.Color = tile.Color;
			m_lastTile.Position = tile.Position;
		}
	}
}
