using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponPlacement : MonoBehaviour
{
	[SerializeField] UIManager m_uiManager;
	[SerializeField] SpriteRenderer m_spr;
	[SerializeField] WeaponChoice m_choice;

	InputAction mouseAction;
	InputAction mousePosition;

	void Start()
	{
		mouseAction = InputSystem.actions.FindAction("Mouse");
		mousePosition = InputSystem.actions.FindAction("MousePosition");
	}

	void Update()
	{
		transform.position = mousePosition.ReadValue<Vector2>();

		if(!mouseAction.IsPressed())
		{
			return;
		}

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>()), Vector2.zero);
		if(hit.collider == null)
		{
			return;
		}

		Saw saw = hit.collider.gameObject.GetComponent<Saw>();
		SawSlot sawSlot = hit.collider.gameObject.GetComponent<SawSlot>();

		bool res = false;

		if(m_choice == WeaponChoice.NewSaw && sawSlot)
		{
			res = sawSlot.AddNewSaw();
		}
		else if(m_choice != WeaponChoice.NewSaw && saw)
		{
			res = saw.Upgrade(m_choice);
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
