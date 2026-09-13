using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CodeStage.AntiCheat.ObscuredTypes;
using Photon;
using UnityEngine;

public class MultiplayerChat : Photon.MonoBehaviour
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct ChatData
	{
		public string name { get; set; }

		public string text { get; set; }

		public Color color { get; set; }

		public float timer { get; set; }

		public ChatData(string string1, string string2, Color color1, float timer1)
		{
			name = string1;
			text = string2;
			color = color1;
			timer = timer1;
		}
	}

	public static MultiplayerChat SP;

	public List<ChatData> messages = new List<ChatData>();

	private bool disablechat;

	private int chatHeight = 300;

	private Vector2 scrollPos = Vector2.zero;

	[HideInInspector]
	public string chatInput = string.Empty;

	[HideInInspector]
	public bool isChatting;

	public GUIStyle chatStyle;

	private string playerName = "Player";

	private void Awake()
	{
		SP = this;
		if (PhotonNetwork.offlineMode)
		{
			disablechat = true;
		}
		if (ObscuredPrefs.GetString("PlayerType") == string.Empty)
		{
			playerName = ObscuredPrefs.GetString("PlayerName");
		}
		else
		{
			playerName = ObscuredPrefs.GetString("ZWName");
		}
	}

	private void Update()
	{
		chatInput = chatInput.Replace("<", string.Empty);
		for (int i = 0; i < messages.Count; i++)
		{
			ChatData chatData = messages[i];
			chatData.timer -= Time.deltaTime;
			if (chatData.timer > 0f)
			{
				messages[i] = new ChatData(chatData.name, chatData.text, chatData.color, chatData.timer);
			}
			else
			{
				messages.RemoveAt(i);
			}
		}
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(5f, Screen.height - 35, Screen.width, 30f));
		if (isChatting)
		{
			GUI.FocusControl("ChatField");
			GUI.SetNextControlName("ChatField");
			GUILayout.BeginHorizontal("box", GUILayout.Width(400f));
			GUI.color = Color.red;
			GUILayout.Label("Say: ", chatStyle);
			GUILayout.Space(5f);
			GUI.color = Color.white;
			chatInput = GUILayout.TextField(chatInput, chatStyle, GUILayout.Width(400f));
			GUILayout.EndHorizontal();
		}
		else
		{
			GUI.FocusControl(string.Empty);
		}
		GUILayout.EndArea();
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.T && !isChatting && !disablechat)
		{
			isChatting = true;
			StartCoroutine(ClearChat());
		}
		if (Event.current.type == EventType.KeyDown && Event.current.character == '\n')
		{
			isChatting = false;
			SendChat(PhotonTargets.All);
		}
		GUILayout.BeginArea(new Rect(5f, Screen.height - chatHeight - 50, Screen.width, chatHeight + 10));
		scrollPos = GUILayout.BeginScrollView(scrollPos);
		GUI.color = Color.white;
		GUILayout.FlexibleSpace();
		for (int i = 0; i < messages.Count; i++)
		{
			GUILayout.BeginHorizontal("box", GUILayout.Width(10f));
			GUI.color = messages[i].color;
			GUILayout.Label(messages[i].name, chatStyle);
			GUILayout.Space(5f);
			GUI.color = Color.white;
			GUILayout.Label(messages[i].text, chatStyle);
			GUILayout.EndHorizontal();
		}
		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}

	private void SendChat(PhotonTargets target)
	{
		if (chatInput != string.Empty)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string @string = ObscuredPrefs.GetString("PlayerType");
			if (@string == "2")
			{
				text = "<color=green>";
				text2 = "</color>";
			}
			if (@string == "3")
			{
				text = "<color=cyan>";
				text2 = "</color>";
			}
			if (@string == "4")
			{
				text = "<color=red>";
				text2 = "</color>";
			}
			if (@string == "5")
			{
				text = "<color=grey>";
				text2 = "</color>";
			}
			if (@string == "6")
			{
				text = "<color=cyan>";
				text2 = "</color>";
			}
			if (@string == "7")
			{
				text = "<color=grey>";
				text2 = "</color>";
			}
			if (@string == "8")
			{
				text = "<color=red>";
				text2 = "</color>";
			}
			if (@string == "9")
			{
				text = "<color=yellow>";
				text2 = "</color>";
			}
			if (@string == "10")
			{
				text = "<color=blue>";
				text2 = "</color>";
			}
			string text3 = playerName;
			if (text3.Contains("<"))
			{
				text3 = playerName.Replace("<", string.Empty);
			}
			text3 = text + text3 + text2;
			string text4 = " " + chatInput;
			base.photonView.RPC("SendChatMessage", target, text4, text3);
			chatInput = string.Empty;
		}
	}

	[RPC]
	private void SendChatMessage(string text, string theName, PhotonMessageInfo info)
	{
		AddMessage(" " + theName + ":", text);
	}

	private void AddMessage(string name, string text)
	{
		Color grey = Color.grey;
		SP.messages.Add(new ChatData(name, text, grey, 30f));
		if (SP.messages.Count > 8)
		{
			SP.messages.RemoveAt(0);
		}
	}

	private void OnLeftRoom()
	{
		messages.Clear();
		base.enabled = false;
	}

	private void OnJoinedRoom()
	{
		base.enabled = true;
	}

	private void OnCreatedRoom()
	{
		base.enabled = true;
	}

	private IEnumerator ClearChat()
	{
		yield return new WaitForSeconds(0.01f);
		chatInput = string.Empty;
	}
}
