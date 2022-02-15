using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SharpMik;
using SharpMik.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class CamTests : MonoBehaviour
{
	public SharpMikManager sharpMikManager;
	public GameObject prefabNote;

	bool doTheThingFromThread;

	private List<Color> colours = new List<Color>(32);

	private void Awake()
	{
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
				// go.GetComponent<Renderer>().material.color = colours[mpControl.main.sample];
				
				// HDRP renderer doesn't seem to like material.colour as above. So change the shader variable directly
				go.GetComponent<Renderer>().material.SetVector("_Colour", colours[mpControl.main.sample]);
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

}