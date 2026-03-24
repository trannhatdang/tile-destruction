using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawHitbox : MonoBehaviour
{
	[SerializeField] Saw m_saw;

	void Start()
	{
		m_saw = GetComponentInParent<Saw>();

		if(!m_saw)
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!m_saw)
		{
			Destroy(this.gameObject);
		}

		if(!other.gameObject.CompareTag("TileHitbox"))
		{
			return;
		}

		other.gameObject.GetComponent<TileHitbox>().Hit(m_saw.Damage);
	}
}
