using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileInfo {
	public TileInfo(TileInfo left, TileInfo right, TileInfo up, TileInfo down, Vector2 pos)
	{
		this.left = left;
		this.right = right;
		this.up = up;
		this.down = down;
		this.pos = pos;
	}

	TileInfo left;
	TileInfo right;
	TileInfo up;
	TileInfo down;

	Vector2 pos;
}

[CreateAssetMenu(fileName = "TileObjectSO", menuName = "ScriptableObjects/TileObjectSO")]
public class TileObjectSO : ScriptableObject
{
	[SerializeField] TileInfo m_headTile;
	[SerializeField] string m_name;

	public TileInfo HeadTile{
		get { return m_headTile; }
		set { m_headTile = value; }
	}

	public string Name {
		get { return m_name; }
		set { m_name = value; }
	}
}
