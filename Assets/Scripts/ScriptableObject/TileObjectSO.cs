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
	[SerializeField] GameObject m_prefab;
	[SerializeField] string m_name;

	private Tile m_parentTile;
	private Tile m_lastTile;
	private Rigidbody2D m_parentTileRB;
	private Rigidbody2D m_lastTileRB;

	public List<TileInfo> Tiles{
		get { return m_tiles; }
	}

	public GameObject Prefab {
		get { return m_prefab; }
	}

	public string Name {
		get { return m_name; }
		set { m_name = value; }
	}

	public void SaveTileObject(Tile tile)
	{
		m_tiles.Clear();

		AddTile(tile);

		var gb = Load();
		bool prefabSuccess;
		m_prefab = PrefabUtility.SaveAsPrefabAsset(gb, $"Assets/Prefabs/{m_name}.prefab", out prefabSuccess);

		if(prefabSuccess)
		{
			Debug.Log("Prefab Saved Successfully");
		}
		else
		{
			Debug.Log("Prefab not Saved Successfully");
		}

		DestroyImmediate(gb);
	}

	public void AddTile(Tile tile)
	{
		if(tile == null)
		{
			return;
		}

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

	public GameObject Load()
	{
		List<Tile> addedTiles = new List<Tile>();
		this.name = m_name;

		GameObject emptyGB = new GameObject();
		Selection.activeGameObject = emptyGB;
		emptyGB.name = m_name;
		foreach(var tile in m_tiles)
		{
			if(tile.Position != Vector2.zero)
			{
				continue;
			}

			GameObject selectedGB = new GameObject();
			selectedGB.tag = "Tile";
			selectedGB.name = m_name;
			selectedGB.transform.parent = emptyGB.transform;

			m_parentTile = selectedGB.AddComponent<Tile>();
			selectedGB.AddComponent<SpriteRenderer>().sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(tile.Color));
			m_parentTileRB = selectedGB.AddComponent<Rigidbody2D>();
			m_parentTileRB.mass = 1000;
			m_parentTileRB.angularDrag = 0;
			selectedGB.AddComponent<BoxCollider2D>();

			m_parentTile.InitializeJoints();

			m_parentTile.TileObject = this;
			m_parentTile.Color = tile.Color;
			m_parentTile.Position = tile.Position;
			m_parentTile.IsParent = true;

			addedTiles.Add(m_parentTile);

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
			newGB.transform.parent = emptyGB.transform;
			newGB.transform.localPosition = Vector2.Scale(new Vector2(0.125f, 0.125f), tile.Position);
			newGB.name = m_name;

			newGB.AddComponent<SpriteRenderer>().sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetColor(tile.Color));
			newGB.AddComponent<Rigidbody2D>().angularDrag = 0;
			newGB.AddComponent<BoxCollider2D>();
			m_lastTileRB = newGB.GetComponent<Rigidbody2D>();

			Tile newTile = newGB.AddComponent<Tile>();
			newTile.TileObject = this;
			newTile.Color = tile.Color;
			newTile.Position = tile.Position;

			newTile.InitializeJoints();

			m_lastTile = newTile;
			addedTiles.Add(m_lastTile);
		}

		foreach(var tile in addedTiles)
		{
			foreach(var other_tile in addedTiles)
			{
				if(tile == other_tile) continue;

				if(tile.Position.x == other_tile.Position.x && tile.Position.y == other_tile.Position.y + 1)
				{
					tile.DownTile = other_tile;
					tile.DownJoint.connectedBody = other_tile.GetComponent<Rigidbody2D>();
					tile.DownJoint.enabled = true;
					other_tile.TopTile = tile;
					other_tile.TopJoint.connectedBody = tile.GetComponent<Rigidbody2D>();
					other_tile.TopJoint.enabled = true;
				}
				else if(tile.Position.x == other_tile.Position.x && tile.Position.y == other_tile.Position.y - 1)
				{
					tile.TopTile = other_tile;
					tile.TopJoint.connectedBody = other_tile.GetComponent<Rigidbody2D>();
					tile.TopJoint.enabled = true;
					other_tile.DownTile = tile;
					other_tile.DownJoint.connectedBody = tile.GetComponent<Rigidbody2D>();
					other_tile.DownJoint.enabled = true;
				}
				else if(tile.Position.y == other_tile.Position.y && tile.Position.x == other_tile.Position.x + 1)
				{
					tile.LeftTile = other_tile;
					tile.LeftJoint.connectedBody = other_tile.GetComponent<Rigidbody2D>();
					tile.LeftJoint.enabled = true;
					other_tile.RightTile = tile;
					other_tile.RightJoint.connectedBody = tile.GetComponent<Rigidbody2D>();
					other_tile.RightJoint.enabled = true;
				}
				else if(tile.Position.y == other_tile.Position.y && tile.Position.x == other_tile.Position.x - 1)
				{
					tile.RightTile = other_tile;
					tile.RightJoint.connectedBody = other_tile.GetComponent<Rigidbody2D>();
					tile.RightJoint.enabled = true;
					other_tile.LeftTile = tile;
					other_tile.LeftJoint.connectedBody = tile.GetComponent<Rigidbody2D>();
					other_tile.LeftJoint.enabled = true;
				}
			}

		}

		return emptyGB;
	}
}
