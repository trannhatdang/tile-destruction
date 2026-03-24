using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] FixedJoint2D m_joint;
	
	[SerializeField] float m_hp = 100;
	[SerializeField] bool m_isParent = false;
	void Start()
	{
		m_joint = gameObject.GetComponent<FixedJoint2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if(m_hp <= 0 && !m_isParent)
		{
			m_isParent = true;
		}

		m_joint.enabled = !m_isParent;
	}

	public void Hit(float damage)
	{
		m_hp -= damage;
	}

	void OnMouseUpAsButton()
	{
		Hit(25);
	}
}
