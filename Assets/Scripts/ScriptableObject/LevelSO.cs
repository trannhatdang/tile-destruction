using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "LevelSO", menuName = "ScriptableObjects/LevelSO")]
public class LevelSO : ScriptableObject
{
	[SerializeField] List<(TileObjectSO, float)> m_objects = new List<(TileObjectSO, float)>();
	[SerializeField] int m_levelUpXP;
	[SerializeField] int m_passLevelXP;
	
	public Action objectsChanged;
	public List<(TileObjectSO, float)> Objects {
		get { return m_objects; }
	}
	public int LevelUpXP {
		get { return m_levelUpXP; }
	}
	public int PassLevelXP {
		get { return m_passLevelXP; }
	}

	public void SetObject(TileObjectSO obj, float timing, int index)
	{
		if(index < 0 || index >= m_objects.Count) return;

		m_objects[index] = (obj, timing);
		objectsChanged();
	}

	public void AddNewObject()
	{
		m_objects.Add((null, 0.0f));
		objectsChanged();
	}
}
