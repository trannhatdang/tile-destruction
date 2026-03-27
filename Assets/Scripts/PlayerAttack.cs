using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] CircleCollider2D m_col;
	[SerializeField] PlayerStatsSO m_playerStats;
	[SerializeField] int m_numFrames = 5;
	[SerializeField] int m_currFrame = 0;
	bool m_active = true;

	InputAction mouseAction;
	InputAction mousePosition;

	void Start()
	{
		mouseAction = InputSystem.actions.FindAction("Mouse");
		mousePosition = InputSystem.actions.FindAction("MousePosition");
		m_col.radius = m_playerStats.DamageRadius;
	}

	// Update is called once per frame
	void Update()
	{
		if(!m_active) return;

		if(m_col.enabled)
		{
			if(m_currFrame >= m_numFrames)
			{
				m_currFrame = 0;
				m_col.enabled = false;
			}
			else
			{
				m_currFrame++;
			}
		}

		Vector2 mousePos = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
		transform.position = mousePos;

		if(mouseAction.IsPressed())
		{
			m_col.enabled = true;
		}
	}

	public float GetDamage(Vector3 pos)
	{
		return m_playerStats.GetDamage((pos - transform.position).magnitude);
	}

	public void Pause()
	{
		m_active = false;
	}

	public void Unpause()
	{
		m_active = true;
	}
}
