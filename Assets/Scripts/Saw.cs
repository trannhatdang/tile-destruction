using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
	[SerializeField] float m_speed = 1.0f;
	[SerializeField] float m_damage = 1.0f;

	public float Damage {
		get { return m_damage; }
	}

	void Start()
	{

	}

	void FixedUpdate()
	{
		transform.Rotate(0, 0, 1 * m_speed);
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Tile"))
		{
			other.gameObject.GetComponent<Tile>().Hit(m_damage);
		}

	}

	public bool Upgrade(WeaponChoice choice)
	{
		if(choice == WeaponChoice.StrongerSaw)
		{
			m_damage *= 2;
			m_speed *= 1.1f;

			return true;
		}
		else if(choice == WeaponChoice.BiggerSaw)
		{
			transform.localScale *= 1.1f;

			return true;
		}
		else if(choice == WeaponChoice.FasterSaw)
		{
			m_speed *= 1.5f;
		}

		return false;
	}

}
