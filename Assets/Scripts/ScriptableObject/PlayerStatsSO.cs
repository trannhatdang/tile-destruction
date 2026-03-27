using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "PlayerStastSO", menuName = "ScriptableObjects/PlayerStatsSO")]
public class PlayerStatsSO : ScriptableObject
{
	[SerializeField] float m_damageRadius;
	[SerializeField] float m_minDmg;
	[SerializeField] float m_maxDmg;

	public float DamageRadius {
		get { return m_damageRadius; }
	}

	public float GetDamage(float dist)
	{
		Debug.Log(dist);
		return Math.Min(((m_minDmg * dist) / m_damageRadius), m_maxDmg);
	}

}
