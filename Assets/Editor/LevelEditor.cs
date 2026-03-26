using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class LevelEditor : EditorWindow
{
	private VisualElement m_topPane;
	private VisualElement m_bottomPane;
	private VisualElement m_customizePane;
	private Label m_noLevelLabel;
	private ObjectField m_levelObjectField;

	private LevelSO m_selectedLevel;

	[MenuItem("Deng/Level Editor")]
	public static void ShowWindow()
	{
		TileEditor wnd = GetWindow<TileEditor>();
		wnd.titleContent = new GUIContent("Level Editor");
	}

	void changeSelectedLevel()
	{
		m_selectedLevel = m_levelObjectField.value;

		m_noLevelLabel.visible = (m_selectedLevel == null);
		m_customizePane.visible = (m_selectedLevel != null);

		if(m_selectedLevel != null)
		{
			m_selectedLevel.objectsChanged += objectsInfoChanged();
			objectsInfoChanged();
		};
	}

	void objectsInfoChanged()
	{
		var objs = m_selectedLevel.Objects;
		int size = objs.Count();
		for(int i = 0; i < size; ++i)
		{
			VisualElement row = new VisualElement();
			ObjectField objField = new ObjectField();
			FloatField floatField = new FloatField();

			objField.RegisterValueChangedCallback((evt) => m_selectedLevel.SetObject(objField.value, floatField.value, i));
			floatField.RegisterValueChangedCallback((evt) => m_selectedLevel.SetObject(objField.value, floatField.value, i))

			row.Add(objField);
			row.Add(floatField);

			m_customizePane.Add(row);
		}
	}

	public void CreateGUI()
	{
		VisualElement root = rootVisualElement;
	
		var splitView = new TwoPaneSplitView(0, 40, TwoPaneSplitViewOrientation.Vertical);
		root.Add(splitView);

		m_topPane = new VisualElement();
		splitView.Add(m_topPane);
		m_bottomPane = new VisualElement();
		splitView.Add(m_bottomPane);

		m_levelObjectField = new ObjectField("Level: ");
		m_levelObjectField.objectType = typeof(LevelSO);
		m_levelObjectField.RegisterValueChangedCallback((evt) => changeSelectedLevel());
		m_topPane.Add(m_levelObjectField);

		m_noLevelLabel = new Label();
		m_noLevelLabel.text = "Choose a level to customize";
		m_bottomPane.Add(m_noLevelLabel);

		m_customizePane = new VisualElement();
		m_customizePane.style.flexDirrection = FlexDirection.Column;
		m_customizePane.style.left = 20;
		m_customizePane.style.top = 20;

		m_bottomPane.Add(m_customizePane);
	}

	void Update()
	{
		Rect bottomPaneRect = m_bottomPane.layout;

		m_noLevelLabel.style.top = bottomPaneRect.height / 2 - 50;
	}
}
