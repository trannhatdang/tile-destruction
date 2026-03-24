using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] Rigidbody2D m_rb;
	
	[SerializeField] float m_hp = 100;
	[SerializeField] bool m_isParent = false;
	void Start()
	{
		if(transform.parent == null)
		{
			m_isParent = true;
			gameObject.AddComponent<Rigidbody2D>();
		}
		else
		{
			m_isParent = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(m_hp <= 0 && !m_isParent)
		{
			transform.parent = transform.parent.parent;
			m_isParent = true;
			gameObject.AddComponent<Rigidbody2D>();
		}
	}

	public void Hit(float damage)
	{
		m_hp -= damage;
	}
}
