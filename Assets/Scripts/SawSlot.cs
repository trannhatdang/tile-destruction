using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSlot : MonoBehaviour
{
	[SerializeField] GameObject sawPrefab;
	[SerializeField] Saw m_saw;
	[SerializeField] bool m_alreadyHasSaw = false;
	public bool AddNewSaw()
	{
		if(m_alreadyHasSaw) return false;

		if(!sawPrefab)
		{
			return false;
		}

		var gb = Instantiate(sawPrefab, transform.position, Quaternion.identity, transform);

		if(gb)
		{
			m_saw = gb.GetComponent<Saw>();
		}
		m_alreadyHasSaw = true;

		return true;
	}

	public bool Upgrade(WeaponChoice choice)
	{
		if(!m_saw)
		{
			return false;
		}

		m_saw.Upgrade(choice);

		return true;
	}
}
