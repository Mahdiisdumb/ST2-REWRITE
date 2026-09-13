using UnityEngine;

public class GUISkinTest : MonoBehaviour
{
	public GUISkin[] thisGUISkins;

	private bool error_GUISkins;

	private int selectedGUISkin;

	private Rect rctWindow1;

	private Rect rctWindow2;

	private bool blnToggleState = true;

	private float fltSliderValue = 0.5f;

	private Vector2 scrollPosition = Vector2.zero;

	private void Awake()
	{
		if (thisGUISkins.Length <= 0)
		{
			Debug.LogError("Missing GUI Skin, assign a GUI Skins in the inspector");
			error_GUISkins = true;
		}
		else
		{
			for (int i = 0; i < thisGUISkins.Length; i++)
			{
				if (!thisGUISkins[i])
				{
					Debug.LogError("Missing GUI Skin #" + i + ", assign a GUI Skin in the inspector");
					error_GUISkins = true;
				}
			}
		}
		rctWindow1 = new Rect(20f, 20f, 200f, 100f);
		rctWindow2 = new Rect(240f, 20f, 200f, 380f);
	}

	private void OnGUI()
	{
		if (!error_GUISkins)
		{
			GUI.skin = thisGUISkins[selectedGUISkin];
			rctWindow1 = GUILayout.Window(0, rctWindow1, DoConfigWindow, "GUI Skin Config Window", GUI.skin.GetStyle("window"));
			rctWindow2 = GUILayout.Window(1, rctWindow2, DoMyWindow, thisGUISkins[selectedGUISkin].name, GUI.skin.GetStyle("window"));
		}
	}

	private void DoConfigWindow(int windowID)
	{
		GUILayout.BeginVertical();
		GUILayout.Label("Select GUI Skin:");
		GUILayout.Space(2f);
		for (int i = 0; i < thisGUISkins.Length; i++)
		{
			GUILayout.BeginHorizontal();
			string text = ((i != selectedGUISkin) ? thisGUISkins[i].name : ("--- " + thisGUISkins[i].name + " ---"));
			if (GUILayout.Button(text))
			{
				selectedGUISkin = i;
				rctWindow1 = new Rect(rctWindow1.x, rctWindow1.y, 200f, 100f);
				rctWindow2 = new Rect(rctWindow2.x, rctWindow2.y, 200f, 380f);
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndVertical();
		GUI.DragWindow();
	}

	private void DoMyWindow(int windowID)
	{
		GUILayout.BeginVertical();
		GUILayout.Label("Im a Label");
		GUILayout.Box("Im a Box\nIm the second line");
		GUILayout.Space(4f);
		GUILayout.Button("Im a Button");
		GUILayout.Button("Im a ButtonIcon", "ButtonIcon");
		GUILayout.TextField("Im a Text Field");
		GUILayout.TextArea("Im a Text Area\nIm the second line\nIm the third line");
		blnToggleState = GUILayout.Toggle(blnToggleState, "Im a Toggle");
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		GUILayout.Space(8f);
		GUILayout.BeginHorizontal();
		fltSliderValue = GUILayout.HorizontalSlider(fltSliderValue, 0f, 1.1f);
		fltSliderValue = GUILayout.VerticalSlider(fltSliderValue, 0f, 1.1f, GUILayout.Height(50f));
		GUILayout.EndHorizontal();
		GUILayout.Space(20f);
		GUILayout.BeginHorizontal();
		GUILayout.Space(8f);
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true, GUILayout.Height(80f), GUILayout.Width(156f));
		for (int i = 0; i < 8; i++)
		{
			GUILayout.Label("Im the #" + (i + 1) + " very long line of text", GUILayout.Width(205f));
		}
		GUILayout.EndScrollView();
		GUILayout.EndHorizontal();
		GUILayout.Space(8f);
		GUILayout.EndVertical();
		GUI.DragWindow();
	}
}
