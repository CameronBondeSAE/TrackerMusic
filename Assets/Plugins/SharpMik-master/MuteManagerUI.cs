using System.Collections;
using System.Collections.Generic;
using SharpMik.Player;
using UnityEngine;

public class MuteManagerUI : MonoBehaviour
{
	public SharpMikManager sharpMikManager;

	public bool[] activeChannels;
	public int height = 25;
	public int width = 100;

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

		GUILayout.BeginVertical();
		GUI.enabled = true;
		if (GUILayout.Button("All active", GUILayout.Width(width), GUILayout.Height(height)))
		{
			for (int i = 0; i < activeChannels.Length; i++)
			{
				activeChannels[i] = true;
				sharpMikManager.UnMuteChannel(i);
			}
		}

		if (GUILayout.Button("All mute", GUILayout.Width(width), GUILayout.Height(height)))
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
		if (GUILayout.Button("Next bar", GUILayout.Width(width), GUILayout.Height(height)))
		{
			// sharpMikManager.player.FastForwardTo(ModPlayer.mod.sngpos+1);
			sharpMikManager.player.SetPosition(ModPlayer.mod.sngpos+1);			
		}
		if (GUILayout.Button("Previous bar", GUILayout.Width(width), GUILayout.Height(height)))
		{
			// sharpMikManager.player.FastForwardTo(ModPlayer.mod.sngpos+1);
			if (ModPlayer.mod.sngpos>0)
			{
				sharpMikManager.player.SetPosition(ModPlayer.mod.sngpos-1);
			}
		}
		GUILayout.EndVertical();
	}

}
