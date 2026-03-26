using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLimit : MonoBehaviour
{
	[SerializeField] bool m_isHittingLimit;

	public bool IsHittingLimit {
		get { return m_isHittingLimit; }
	}

}
