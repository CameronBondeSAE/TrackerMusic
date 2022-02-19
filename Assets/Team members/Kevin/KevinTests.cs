using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SharpMik;
using SharpMik.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class KevinTests : MonoBehaviour
{
	public SharpMikManager sharpMikManager;
	public bool[] activeChannels;
	public GameObject prefabNote;

	bool doTheThingFromThread;

	private List<Color> colours = new List<Color>(32);

	private void Awake()
	{
		activeChannels = new bool[32];
		for (int i = 0; i < activeChannels.Length; i++)
		{
			activeChannels[i] = true;
		}

		for (int i = 0; i < 32; i++)
		{
			colours.Add(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
		}

		ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

		UnityThread.initUnityThread();
	}

	private void ModPlayerOnNoteEvent(MP_CONTROL mpControl)
	{
		UnityThread.executeInUpdate(() =>
		{
			// transform.Rotate(new Vector3(0f, 90f, 0f));
			if (mpControl.muted <= 0)
			{
				GameObject go = Instantiate(prefabNote);
				short mpControlVolume = (short) (mpControl.volume / 40);
				go.transform.position = new Vector3(mpControl.anote, 0, 0) + new Vector3(mpControl.main.sample, 0, 0);
				// go.transform.localScale = new Vector3(mpControlVolume, mpControlVolume, mpControlVolume);
				go.GetComponent<Renderer>().material.color = colours[mpControl.main.sample];
			}
		});

		// doTheThingFromThread = true;
	}

	private void Update()
	{
		// if (doTheThingFromThread)
		// {
		// Instantiate(prefabNote);
		// doTheThingFromThread = false;
		// }
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