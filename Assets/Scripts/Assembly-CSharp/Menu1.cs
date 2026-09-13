using UnityEngine;

public class Menu1 : MonoBehaviour
{
	public GUISkin skin;

	private string show = "Main Menu";

	private float creditsHeight;

	private float musicValue = 100f;

	private float voiceValue = 100f;

	private float fxValue = 100f;

	private float tmpQualitySettings;

	private float qualitySettings = 3f;

	private float difficulty = 2f;

	private bool subtitles;

	private bool tmpFullScreen = true;

	private bool fullScreen = true;

	private float tmpXResolution;

	private float tmpYResolution;

	private float tmpResolutionLevel;

	private float xResolution = 800f;

	private float yResolution = 600f;

	private float resolutionLevel = 4f;

	private bool canUseButton;

	private bool changeMoveForwardKey;

	private bool changeMoveBackKey;

	private bool changeMoveLeftKey;

	private bool changeMoveRightKey;

	private bool changeFireKey;

	private bool changeAimKey;

	private void Start()
	{
		if (PlayerPrefs.HasKey("Music"))
		{
			musicValue = LoadFloat("Music");
		}
		else
		{
			musicValue = 100f;
		}
		if (PlayerPrefs.HasKey("Voice"))
		{
			voiceValue = LoadFloat("Voice");
		}
		else
		{
			voiceValue = 100f;
		}
		if (PlayerPrefs.HasKey("FX"))
		{
			fxValue = LoadFloat("FX");
		}
		else
		{
			fxValue = 100f;
		}
		if (PlayerPrefs.HasKey("qualitySettings"))
		{
			qualitySettings = LoadFloat("qualitySettings");
			QualitySettings.SetQualityLevel((int)qualitySettings);
		}
		if (PlayerPrefs.HasKey("resolutionLevel"))
		{
			resolutionLevel = LoadFloat("resolutionLevel");
			if (resolutionLevel == 0f)
			{
				xResolution = 640f;
				yResolution = 480f;
			}
			if (resolutionLevel == 1f)
			{
				xResolution = 800f;
				yResolution = 480f;
			}
			if (resolutionLevel == 2f)
			{
				xResolution = 800f;
				yResolution = 600f;
			}
			if (resolutionLevel == 3f)
			{
				xResolution = 1024f;
				yResolution = 600f;
			}
			if (resolutionLevel == 4f)
			{
				xResolution = 1024f;
				yResolution = 768f;
			}
			if (resolutionLevel == 5f)
			{
				xResolution = 1280f;
				yResolution = 768f;
			}
			if (resolutionLevel == 6f)
			{
				xResolution = 1280f;
				yResolution = 960f;
			}
		}
		if (PlayerPrefs.HasKey("fullScreen"))
		{
			fullScreen = LoadBool("fullScreen");
			Screen.SetResolution((int)xResolution, (int)yResolution, fullScreen);
			qualitySettings = LoadFloat("qualitySettings");
			QualitySettings.SetQualityLevel((int)qualitySettings);
		}
		if (!PlayerPrefs.HasKey("MoveForward"))
		{
			SaveString("MoveForward", "W");
		}
		if (!PlayerPrefs.HasKey("MoveBack"))
		{
			SaveString("MoveBack", "S");
		}
		if (!PlayerPrefs.HasKey("MoveLeft"))
		{
			SaveString("MoveLeft", "A");
		}
		if (!PlayerPrefs.HasKey("MoveRight"))
		{
			SaveString("MoveRight", "D");
		}
		if (!PlayerPrefs.HasKey("Fire"))
		{
			SaveString("Fire", "Mouse1");
		}
		if (!PlayerPrefs.HasKey("Aim"))
		{
			SaveString("Aim", "Mouse2");
		}
		if (PlayerPrefs.HasKey("Difficulty"))
		{
			difficulty = LoadFloat("Difficulty");
		}
		if (PlayerPrefs.HasKey("Subtitles"))
		{
			subtitles = LoadBool("Subtitles");
		}
	}

	private void OnGUI()
	{
		GUI.skin = skin;
		if (show == "Main Menu")
		{
			MainMenu();
		}
		else if (show == "Play")
		{
			Play();
		}
		else if (show == "Online")
		{
			Online();
		}
		else if (show == "Option")
		{
			Option();
		}
		else if (show == "AudioOption")
		{
			AudioOption();
		}
		else if (show == "VideoOption")
		{
			VideoOption();
		}
		else if (show == "ControlsOption")
		{
			ControlsOption();
		}
		else if (show == "GameplayOption")
		{
			GameplayOption();
		}
		else if (show == "Credits")
		{
			Credits();
		}
		else if (show == "Quit")
		{
			Quit();
		}
		else if (show == "YourCodeHere-Play")
		{
			YourCodeHerePlay();
		}
		else if (show == "YourCodeHere-Online")
		{
			YourCodeHereOnline();
		}
	}

	private void MainMenu()
	{
		if (GUI.Button(new Rect(Screen.width / 2 - 55, 125f, 110f, 45f), "Play"))
		{
			show = "Play";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 75, 210f, 150f, 45f), "Online"))
		{
			show = "Online";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 75, 295f, 150f, 45f), "Option"))
		{
			show = "Option";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 75, 375f, 150f, 45f), "Credits"))
		{
			creditsHeight = 0f;
			show = "Credits";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 45, 455f, 90f, 45f), "Quit"))
		{
			show = "Quit";
		}
	}

	private void Play()
	{
		GUI.Label(new Rect(0f, 10f, Screen.width, 70f), "Play");
		if (GUI.Button(new Rect(Screen.width / 2 - 100, 125f, 200f, 45f), "New Game"))
		{
			show = "YourCodeHere-Play";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 100, 175f, 200f, 45f), "Continue"))
		{
			show = "YourCodeHere-Play";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 100, 225f, 200f, 45f), "Load Game"))
		{
			show = "YourCodeHere-Play";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 60, 100f, 40f), "Back"))
		{
			show = "Main Menu";
		}
	}

	private void Online()
	{
		GUI.Label(new Rect(0f, 10f, Screen.width, 70f), "Online");
		if (GUI.Button(new Rect(Screen.width / 2 - 125, 125f, 250f, 45f), "Join Server"))
		{
			show = "YourCodeHere-Online";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 125, 175f, 250f, 45f), "Host Server"))
		{
			show = "YourCodeHere-Online";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 60, 100f, 40f), "Back"))
		{
			show = "Main Menu";
		}
	}

	private void Option()
	{
		GUI.Label(new Rect(0f, 10f, Screen.width, 70f), "Options");
		if (GUI.Button(new Rect(Screen.width / 2 - 75, 150f, 150f, 45f), "Audio"))
		{
			show = "AudioOption";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 75, 225f, 150f, 45f), "Video"))
		{
			tmpResolutionLevel = resolutionLevel;
			tmpXResolution = xResolution;
			tmpYResolution = yResolution;
			tmpFullScreen = fullScreen;
			tmpQualitySettings = qualitySettings;
			show = "VideoOption";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 100, 300f, 200f, 45f), "Controls"))
		{
			show = "ControlsOption";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 100, 375f, 200f, 45f), "Gameplay"))
		{
			show = "GameplayOption";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 60, 100f, 40f), "Back"))
		{
			show = "Main Menu";
		}
	}

	private void AudioOption()
	{
		GUI.Label(new Rect(0f, 10f, Screen.width, 70f), "Audio Options");
		GUI.Label(new Rect(Screen.width / 2 - 175, 120f, 300f, 70f), "Music: ");
		skin.label.alignment = TextAnchor.MiddleLeft;
		GUI.Label(new Rect(Screen.width / 2 + 25, 120f, 300f, 70f), musicValue.ToString());
		skin.label.alignment = TextAnchor.MiddleCenter;
		musicValue = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 100, 170f, 200f, 30f), (int)musicValue, 0f, 100f);
		GUI.Label(new Rect(Screen.width / 2 - 170, 220f, 300f, 70f), "Voice: ");
		skin.label.alignment = TextAnchor.MiddleLeft;
		GUI.Label(new Rect(Screen.width / 2 + 25, 220f, 300f, 70f), voiceValue.ToString());
		skin.label.alignment = TextAnchor.MiddleCenter;
		voiceValue = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 100, 270f, 200f, 30f), (int)voiceValue, 0f, 100f);
		GUI.Label(new Rect(Screen.width / 2 - 170, 320f, 300f, 70f), "FX: ");
		skin.label.alignment = TextAnchor.MiddleLeft;
		GUI.Label(new Rect(Screen.width / 2 + 10, 320f, 300f, 70f), fxValue.ToString());
		skin.label.alignment = TextAnchor.MiddleCenter;
		fxValue = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 100, 370f, 200f, 30f), (int)fxValue, 0f, 100f);
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 60, 100f, 40f), "Back"))
		{
			SaveFloat("Music", (int)musicValue);
			SaveFloat("Voice", (int)voiceValue);
			SaveFloat("FX", (int)fxValue);
			show = "Option";
		}
	}

	private void VideoOption()
	{
		GUI.Label(new Rect(0f, 10f, Screen.width, 70f), "Video Options");
		GUI.Label(new Rect(0f, 120f, Screen.width, 70f), "Quality Level: " + QualitySettings.names[(int)tmpQualitySettings]);
		tmpQualitySettings = (int)GUI.HorizontalSlider(new Rect(Screen.width / 2 - 100, 170f, 200f, 30f), tmpQualitySettings + 0.5f, 0.5f, (float)QualitySettings.names.Length - 0.5f);
		GUI.Label(new Rect(0f, 220f, Screen.width, 70f), "Resolution: " + tmpXResolution + "/" + tmpYResolution);
		tmpResolutionLevel = (int)GUI.HorizontalSlider(new Rect(Screen.width / 2 - 100, 270f, 200f, 30f), tmpResolutionLevel + 0.5f, 0.5f, 6.5f);
		if (tmpResolutionLevel == 0f)
		{
			tmpXResolution = 640f;
			tmpYResolution = 480f;
		}
		if (tmpResolutionLevel == 1f)
		{
			tmpXResolution = 800f;
			tmpYResolution = 480f;
		}
		if (tmpResolutionLevel == 2f)
		{
			tmpXResolution = 800f;
			tmpYResolution = 600f;
		}
		if (tmpResolutionLevel == 3f)
		{
			tmpXResolution = 1024f;
			tmpYResolution = 600f;
		}
		if (tmpResolutionLevel == 4f)
		{
			tmpXResolution = 1024f;
			tmpYResolution = 768f;
		}
		if (tmpResolutionLevel == 5f)
		{
			tmpXResolution = 1280f;
			tmpYResolution = 768f;
		}
		if (tmpResolutionLevel == 6f)
		{
			tmpXResolution = 1280f;
			tmpYResolution = 960f;
		}
		GUI.Label(new Rect(-40f, 320f, Screen.width, 70f), "FullScreen:");
		string text = "No";
		if (tmpFullScreen)
		{
			text = "Yes";
		}
		if (GUI.Button(new Rect(Screen.width / 2 + 50, 335f, 80f, 40f), text))
		{
			tmpFullScreen = !tmpFullScreen;
		}
		if (GUI.Button(new Rect(Screen.width / 2 + 20, Screen.height - 60, 120f, 40f), "Apply"))
		{
			qualitySettings = tmpQualitySettings;
			SaveFloat("qualitySettings", qualitySettings);
			QualitySettings.SetQualityLevel((int)qualitySettings);
			resolutionLevel = tmpResolutionLevel;
			xResolution = tmpXResolution;
			yResolution = tmpYResolution;
			SaveFloat("resolutionLevel", resolutionLevel);
			SaveFloat("xResolution", xResolution);
			SaveFloat("yResolution", yResolution);
			fullScreen = tmpFullScreen;
			SaveBool("fullScreen", fullScreen);
			Screen.SetResolution((int)xResolution, (int)yResolution, fullScreen);
			show = "Option";
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 120, Screen.height - 60, 100f, 40f), "Back"))
		{
			show = "Option";
		}
	}

	private void ControlsOption()
	{
		GUI.Label(new Rect(0f, 10f, Screen.width, 70f), "Controls");
		skin.label.fontSize = 25;
		skin.label.alignment = TextAnchor.MiddleRight;
		GUI.Label(new Rect(0f, 100f, Screen.width / 2, 70f), "Move Forward: ");
		if (changeMoveForwardKey)
		{
			if (Input.GetMouseButtonDown(0))
			{
				SaveString("MoveForward", KeyCode.Mouse1.ToString());
				changeMoveForwardKey = false;
				canUseButton = false;
			}
			else if (Input.GetMouseButtonDown(1))
			{
				SaveString("MoveForward", KeyCode.Mouse2.ToString());
				changeMoveForwardKey = false;
				canUseButton = false;
			}
			else if (Event.current.type == EventType.KeyDown)
			{
				SaveString("MoveForward", Event.current.keyCode.ToString());
				changeMoveForwardKey = false;
				canUseButton = false;
			}
			GUI.Button(new Rect(Screen.width / 2 + 10, 115f, 250f, 40f), "...");
		}
		else
		{
			if (canUseButton && GUI.Button(new Rect(Screen.width / 2 + 10, 115f, 250f, 40f), LoadString("MoveForward")))
			{
				changeMoveForwardKey = true;
			}
			canUseButton = true;
		}
		GUI.Label(new Rect(0f, 150f, Screen.width / 2, 70f), "Move Back: ");
		if (changeMoveBackKey)
		{
			if (Input.GetMouseButtonDown(0))
			{
				SaveString("MoveBack", KeyCode.Mouse1.ToString());
				changeMoveBackKey = false;
				canUseButton = false;
			}
			else if (Input.GetMouseButtonDown(1))
			{
				SaveString("MoveBack", KeyCode.Mouse2.ToString());
				changeMoveBackKey = false;
				canUseButton = false;
			}
			else if (Event.current.type == EventType.KeyDown)
			{
				SaveString("MoveBack", Event.current.keyCode.ToString());
				changeMoveBackKey = false;
				canUseButton = false;
			}
			GUI.Button(new Rect(Screen.width / 2 + 10, 165f, 250f, 40f), "...");
		}
		else if (canUseButton && GUI.Button(new Rect(Screen.width / 2 + 10, 165f, 250f, 40f), LoadString("MoveBack")))
		{
			if (!changeMoveBackKey)
			{
				changeMoveBackKey = true;
			}
			canUseButton = true;
		}
		GUI.Label(new Rect(0f, 200f, Screen.width / 2, 70f), "Move Left: ");
		if (changeMoveLeftKey)
		{
			if (Input.GetMouseButtonDown(0))
			{
				SaveString("MoveLeft", KeyCode.Mouse1.ToString());
				changeMoveLeftKey = false;
				canUseButton = false;
			}
			else if (Input.GetMouseButtonDown(1))
			{
				SaveString("MoveLeft", KeyCode.Mouse2.ToString());
				changeMoveLeftKey = false;
				canUseButton = false;
			}
			else if (Event.current.type == EventType.KeyDown)
			{
				SaveString("MoveLeft", Event.current.keyCode.ToString());
				changeMoveLeftKey = false;
				canUseButton = false;
			}
			GUI.Button(new Rect(Screen.width / 2 + 10, 215f, 250f, 40f), "...");
		}
		else if (canUseButton && GUI.Button(new Rect(Screen.width / 2 + 10, 215f, 250f, 40f), LoadString("MoveLeft")))
		{
			if (!changeMoveLeftKey)
			{
				changeMoveLeftKey = true;
			}
			canUseButton = true;
		}
		GUI.Label(new Rect(0f, 250f, Screen.width / 2, 70f), "Move Right: ");
		if (changeMoveRightKey)
		{
			if (Input.GetMouseButtonDown(0))
			{
				SaveString("MoveRight", KeyCode.Mouse1.ToString());
				changeMoveRightKey = false;
				canUseButton = false;
			}
			else if (Input.GetMouseButtonDown(1))
			{
				SaveString("MoveRight", KeyCode.Mouse2.ToString());
				changeMoveRightKey = false;
				canUseButton = false;
			}
			else if (Event.current.type == EventType.KeyDown)
			{
				SaveString("MoveRight", Event.current.keyCode.ToString());
				changeMoveRightKey = false;
				canUseButton = false;
			}
			GUI.Button(new Rect(Screen.width / 2 + 10, 265f, 250f, 40f), "...");
		}
		else if (canUseButton && GUI.Button(new Rect(Screen.width / 2 + 10, 265f, 250f, 40f), LoadString("MoveRight")))
		{
			if (!changeMoveRightKey)
			{
				changeMoveRightKey = true;
			}
			canUseButton = false;
		}
		GUI.Label(new Rect(0f, 350f, Screen.width / 2, 70f), "Fire: ");
		if (changeFireKey)
		{
			if (Input.GetMouseButtonDown(0))
			{
				SaveString("Fire", KeyCode.Mouse1.ToString());
				changeFireKey = false;
				canUseButton = false;
			}
			else if (Input.GetMouseButtonDown(1))
			{
				SaveString("Fire", KeyCode.Mouse2.ToString());
				changeFireKey = false;
				canUseButton = false;
			}
			else if (Event.current.type == EventType.KeyDown)
			{
				SaveString("Fire", Event.current.keyCode.ToString());
				changeFireKey = false;
				canUseButton = false;
			}
			GUI.Button(new Rect(Screen.width / 2 + 10, 365f, 250f, 40f), "...");
		}
		else if (canUseButton && GUI.Button(new Rect(Screen.width / 2 + 10, 365f, 250f, 40f), LoadString("Fire")))
		{
			if (!changeFireKey)
			{
				changeFireKey = true;
			}
			canUseButton = false;
		}
		GUI.Label(new Rect(0f, 400f, Screen.width / 2, 70f), "Aim: ");
		if (changeAimKey)
		{
			if (Input.GetMouseButtonDown(0))
			{
				SaveString("Aim", KeyCode.Mouse1.ToString());
				changeAimKey = false;
				canUseButton = false;
			}
			else if (Input.GetMouseButtonDown(1))
			{
				SaveString("Aim", KeyCode.Mouse2.ToString());
				changeAimKey = false;
				canUseButton = false;
			}
			else if (Event.current.type == EventType.KeyDown)
			{
				SaveString("Aim", Event.current.keyCode.ToString());
				changeAimKey = false;
				canUseButton = false;
			}
			GUI.Button(new Rect(Screen.width / 2 + 10, 415f, 250f, 40f), "...");
		}
		else if (canUseButton && GUI.Button(new Rect(Screen.width / 2 + 10, 415f, 250f, 40f), LoadString("Aim")))
		{
			if (!changeAimKey)
			{
				changeAimKey = true;
			}
			canUseButton = false;
		}
		skin.label.fontSize = 20;
		skin.label.alignment = TextAnchor.MiddleCenter;
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 60, 100f, 40f), "Back"))
		{
			show = "Option";
		}
	}

	private void GameplayOption()
	{
		GUI.Label(new Rect(0f, 10f, Screen.width, 70f), "Gameplay");
		string text = "Noob";
		if (difficulty == 1f)
		{
			text = "Easy";
		}
		else if (difficulty == 2f)
		{
			text = "Normal";
		}
		else if (difficulty == 3f)
		{
			text = "Hard";
		}
		else if (difficulty == 4f)
		{
			text = "Pro";
		}
		GUI.Label(new Rect(0f, 120f, Screen.width, 70f), "Difficulty: " + text);
		difficulty = (int)GUI.HorizontalSlider(new Rect(Screen.width / 2 - 100, 170f, 200f, 30f), difficulty + 0.5f, 0.5f, 4.5f);
		string text2 = "No";
		if (subtitles)
		{
			text2 = "Yes";
		}
		GUI.Label(new Rect(-40f, 200f, Screen.width, 70f), "Subtitles: ");
		if (GUI.Button(new Rect(Screen.width / 2 + 40, 215f, 80f, 40f), text2))
		{
			subtitles = !subtitles;
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 60, 100f, 40f), "Back"))
		{
			SaveFloat("Difficulty", difficulty);
			SaveBool("Subtitles", subtitles);
			show = "Option";
		}
	}

	private void Credits()
	{
		skin.label.fontSize = 25;
		GUI.BeginGroup(new Rect(0f, 0f, Screen.width, Screen.height - 70));
		GUI.Label(new Rect(0f, -60f + creditsHeight, Screen.width, 60f), "Artwork By: Sophia");
		GUI.Label(new Rect(0f, -120f + creditsHeight, Screen.width, 60f), "Written By: Jacob");
		GUI.Label(new Rect(0f, -180f + creditsHeight, Screen.width, 60f), "Lyrics By: Isabella");
		GUI.Label(new Rect(0f, -240f + creditsHeight, Screen.width, 60f), "Music By: Mason");
		GUI.Label(new Rect(0f, -300f + creditsHeight, Screen.width, 60f), "Programmed By: Emma");
		GUI.Label(new Rect(0f, -360f + creditsHeight, Screen.width, 60f), "Sound Designer: William");
		GUI.Label(new Rect(0f, -420f + creditsHeight, Screen.width, 60f), "Hosted By: Noah");
		GUI.Label(new Rect(0f, -480f + creditsHeight, Screen.width, 60f), "Directed By: Michael");
		GUI.Label(new Rect(0f, -540f + creditsHeight, Screen.width, 60f), "Producer: Alexander");
		GUI.Label(new Rect(0f, -600f + creditsHeight, Screen.width, 60f), "Design: Your Momma");
		GUI.Label(new Rect(0f, -660f + creditsHeight, Screen.width, 60f), "Graphics: Emily");
		GUI.Label(new Rect(0f, -720f + creditsHeight, Screen.width, 60f), "Lighting: Logan");
		GUI.Label(new Rect(0f, -780f + creditsHeight, Screen.width, 60f), "Video Editor: David");
		GUI.EndGroup();
		creditsHeight += 50f * Time.deltaTime;
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 60, 100f, 40f), "Back"))
		{
			skin.label.fontSize = 20;
			show = "Main Menu";
		}
	}

	private void Quit()
	{
		GUI.Label(new Rect(0f, Screen.height / 2 - 100, Screen.width, 60f), "Are you sure you want to quit the game");
		if (GUI.Button(new Rect(Screen.width / 2 - 105, Screen.height / 2 - 10, 80f, 40f), "Yes"))
		{
			Application.Quit();
		}
		else if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 - 10, 70f, 40f), "No"))
		{
			show = "Main Menu";
		}
	}

	private void YourCodeHerePlay()
	{
		GUI.Label(new Rect(0f, Screen.height / 2 - 100, Screen.width, 60f), "Your Code Here");
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 60, 100f, 40f), "Back"))
		{
			skin.label.fontSize = 20;
			show = "Play";
		}
	}

	private void YourCodeHereOnline()
	{
		GUI.Label(new Rect(0f, Screen.height / 2 - 100, Screen.width, 60f), "Your Code Here");
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 60, 100f, 40f), "Back"))
		{
			skin.label.fontSize = 20;
			show = "Online";
		}
	}

	private void SaveFloat(string _name, float _value)
	{
		PlayerPrefs.SetFloat(_name, _value);
	}

	private float LoadFloat(string _name)
	{
		return PlayerPrefs.GetFloat(_name);
	}

	private void SaveString(string _name, string _value)
	{
		PlayerPrefs.SetString(_name, _value);
	}

	private string LoadString(string _name)
	{
		return PlayerPrefs.GetString(_name);
	}

	private void SaveBool(string _name, bool _value)
	{
		if (_value)
		{
			PlayerPrefs.SetInt(_name, 1);
		}
		else
		{
			PlayerPrefs.SetInt(_name, 0);
		}
	}

	private bool LoadBool(string _name)
	{
		int @int = PlayerPrefs.GetInt(_name);
		if (@int == 1)
		{
			return true;
		}
		return false;
	}
}
