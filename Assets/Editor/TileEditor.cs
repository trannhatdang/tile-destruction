using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class TileEditor : EditorWindow
{
	private VisualElement m_bottomPane;

	private Tile m_selectedTile;
	private TileColor m_currColor;

	private Button m_createGBButton;
	private List<Button> m_dirButtons;

	private Rect m_bottomPaneRect;

	void createGB()
	{
		GameObject selectedGB = new GameObject();
		m_selectedTile = selectedGB.AddComponent<Tile>();
		m_selectedTile.SetColor(m_currColor);
	}

	void save()
	{

	}

	void load()
	{

	}

	[MenuItem("Deng/Tile Editor")]
	public static void ShowWindow()
	{
		TileEditor wnd = GetWindow<TileEditor>();
		wnd.titleContent = new GUIContent("TileEditor");
	}

	public void CreateGUI()
	{
		m_dirButtons = new List<Button>(4);

		// Each editor window contains a root VisualElement object
		VisualElement root = rootVisualElement;
		var splitView = new TwoPaneSplitView(0, 40, TwoPaneSplitViewOrientation.Vertical);
		root.Add(splitView);

		VisualElement topPane = new VisualElement();
		splitView.Add(topPane);
		m_bottomPane = new VisualElement();
		splitView.Add(m_bottomPane);

		// Create Save button
		Button saveButton = new Button();
		saveButton.name = "Save";
		saveButton.text = "Save";
		saveButton.RegisterCallback<MouseUpEvent>((evt) => save());
		topPane.Add(saveButton);

		// Create Load button
		Button loadButton = new Button();
		loadButton.name = "Load";
		loadButton.text = "Load";
		loadButton.RegisterCallback<MouseUpEvent>((evt) => load());
		topPane.Add(loadButton);

		m_createGBButton = new Button();
		m_createGBButton.name = "Create Game Object";
		m_createGBButton.text = "Create Object";
		m_createGBButton.RegisterCallback<MouseUpEvent>((evt) => createGB());

		m_bottomPane.Add(m_createGBButton);

		for(int i = 0; i < 4; ++i)
		{
			m_dirButtons.Add(new Button());
			m_dirButtons[i].style.width = 50;
		}


		m_dirButtons[0].name = "Up";
		m_dirButtons[0].text = "↑";

		m_dirButtons[1].name = "Down";
		m_dirButtons[1].text = "↓";

		m_dirButtons[2].name = "Left";
		m_dirButtons[2].text = "←";

		m_dirButtons[3].name = "Right";
		m_dirButtons[3].text = "→";

		foreach(Button btn in m_dirButtons)
		{
			m_bottomPane.Add(btn);
		}
	}

	void Update()
	{
		m_createGBButton.style.top = m_bottomPaneRect.width / 2;

		if(Selection.count == 1 && Selection.activeGameObject && Selection.activeGameObject.GetComponent<Tile>())
		{
			m_selectedTile = Selection.activeGameObject.GetComponent<Tile>();
		}
		else
		{
			m_selectedTile = null;
		}

		foreach(Button btn in m_dirButtons)
		{
			btn.visible = m_selectedTile;
		}

		m_createGBButton.visible = !m_selectedTile;
		m_bottomPaneRect = m_bottomPane.layout;

		//Preprogramed values -- maybe bad
		m_dirButtons[0].style.top = m_bottomPaneRect.height / 2 - 90;
		m_dirButtons[0].style.left = m_bottomPaneRect.width / 2 - 30;

		m_dirButtons[1].style.top = m_bottomPaneRect.height / 2 - 30;
		m_dirButtons[1].style.left = m_bottomPaneRect.width / 2 - 30;

		m_dirButtons[2].style.top = m_bottomPaneRect.height / 2 - 90;
		m_dirButtons[2].style.left = m_bottomPaneRect.width / 2 - 90;

		m_dirButtons[3].style.top = m_bottomPaneRect.height / 2 - 110;
		m_dirButtons[3].style.left = m_bottomPaneRect.width / 2 + 30;
	}
}
