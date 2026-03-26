using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] Transform m_spawnPosition;
	[SerializeField] LevelSO m_levelInfo;
	[SerializeField] Abyss m_abyss;
	[SerializeField] TopLimit m_topLimit;
	[SerializeField] float m_timeSinceLastDrop;
	[SerializeField] int m_objIndex;
	[SerializeField] int m_xp;

	private List<(TileObjectSO obj, float timing)> objects = new List<(TileObjectSO, float)>();

	void Start()
	{
		objects = m_levelInfo.Objects;
	}
	
	void Update()
	{
		m_timeSinceLastDrop += Time.deltaTime;
		
		if(m_timeSinceLastDrop > objects[m_objIndex].timing && m_topLimit.IsHittingLimit)
		{
			Deng.ObjectPoolManager.SpawnObject(objects[m_objIndex++].obj.Prefab, m_spawnPosition.position, Quaternion.identity);
			m_timeSinceLastDrop = 0.0f;

		}
	}

	void dropTile()
	{

	}
}
