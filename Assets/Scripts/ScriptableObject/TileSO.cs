using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "TileSO", menuName = "ScriptableObjects/TileSO")]
public class TileSO : ScriptableObject
{
	[SerializeField] List<(Tile, Vector2)> m_tiles;
	[SerializeField] string m_name;

	public List<(Tile, Vector2)> Tiles{
		get { return m_tiles; }
	}

	public string Name {
		get { return m_name; }
	}

	public void AddTile(Tile tile, Vector2 vec)
	{
		m_tiles.Add((tile, vec));
	}
}
