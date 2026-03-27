using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponPlacement : MonoBehaviour
{
	[SerializeField] UIManager m_uiManager;
	[SerializeField] SpriteRenderer m_spr;
	[SerializeField] WeaponChoice m_choice;
	[SerializeField] WeaponSelectionButton m_btn1;
	[SerializeField] WeaponSelectionButton m_btn2;

	InputAction mouseAction;
	InputAction mousePosition;

	void Start()
	{
		mouseAction = InputSystem.actions.FindAction("Mouse");
		mousePosition = InputSystem.actions.FindAction("MousePosition");
	}

	void Update()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
		(transform as RectTransform).position = mousePos;

		if(!mouseAction.IsPressed())
		{
			return;
		}

		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
		if(hit.collider == null)
		{
			return;
		}

		SawSlot sawSlot = hit.collider.gameObject.GetComponent<SawSlot>();
		Debug.Log(hit.collider.gameObject.name);
		if(!sawSlot)
		{
			return;
		}

		bool res = false;

		if(m_choice == WeaponChoice.NewSaw)
		{
			res = sawSlot.AddNewSaw();
		}
		else if(m_choice != WeaponChoice.NewSaw)
		{
			res = sawSlot.Upgrade(m_choice);
		}

		if(res)
		{
			m_uiManager.Resume();
		}
	}

	public void Set(WeaponChoice choice)
	{
		m_spr.sprite = Utils.LoadAsset<Sprite>(Constants.Instance.GetWeaponPlacementSprite(choice));
		m_choice = choice;
	}
}
