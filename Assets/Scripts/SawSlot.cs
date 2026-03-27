using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSlot : MonoBehaviour
{
	[SerializeField] GameObject sawPrefab;
	[SerializeField] bool alreadyHasSaw = false;
	public bool AddNewSaw()
	{
		if(alreadyHasSaw) return false;

		if(!sawPrefab)
		{
			return false;
		}

		Instantiate(sawPrefab, transform.position, Quaternion.identity, transform);

		return true;
	}
}
