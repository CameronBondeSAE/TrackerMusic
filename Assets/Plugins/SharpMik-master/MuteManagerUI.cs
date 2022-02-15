using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteManagerUI : MonoBehaviour
{
	public SharpMikManager sharpMikManager;

	public bool[] activeChannels;

	private void Awake()
	{
		activeChannels = new bool[32];
		for (int i = 0; i < activeChannels.Length; i++)
		{
			activeChannels[i] = true;
		}
	}

	private void OnGUI()
	{
		GUILayout.BeginHorizontal();
		for (int i = 0; i < 32; i++)
		{
			GUILayout.BeginVertical();
			GUI.enabled = activeChannels[i];
			if (GUILayout.Button("M"))
			{
				activeChannels[i] = false;
				sharpMikManager.MuteChannel(i);
			}

			GUI.enabled = !activeChannels[i];
			if (GUILayout.Button("A"))
			{
				activeChannels[i] = true;
				sharpMikManager.UnMuteChannel(i);
			}

			GUILayout.EndVertical();
		}

		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUI.enabled = true;
		if (GUILayout.Button("All active"))
		{
			for (int i = 0; i < activeChannels.Length; i++)
			{
				activeChannels[i] = true;
				sharpMikManager.UnMuteChannel(i);
			}
		}

		if (GUILayout.Button("All mute"))
		{
			for (int i = 0; i < activeChannels.Length; i++)
			{
				activeChannels[i] = false;
				sharpMikManager.MuteChannel(i);
			}
		}

		// if (GUILayout.Button("Start"))
		// {
		// 	sharpMikManager.player.Play(sharpMikManager.ModuleAsset.text);
		// }
		GUILayout.EndHorizontal();
	}

}
