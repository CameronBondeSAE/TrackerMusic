using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class KevinMinimal : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    public GameObject prefabCube;
    void Start()
    {
        // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        // GPG230 stuff
        UnityThread.initUnityThread();
    }

    // GPG230 stuff
    private void ModPlayerOnNoteEvent(MP_CONTROL mpcontrol)
    {
        UnityThread.executeInUpdate(() =>
        {
            NotePlayedEvent(mpcontrol);
        });
    }

    private void NotePlayedEvent(MP_CONTROL mpControl)
    {
        // Your code goes here
        GameObject cubeO = Instantiate(prefabCube);
        cubeO.transform.DOMove(new Vector3(0, 0, 0), 2.0f * 0.5f).SetEase(Ease.OutBounce);
        short mpControlVolume = (short) (mpControl.volume / 40);
        cubeO.transform.position = new Vector3(mpControl.anote, 0, 0) + new Vector3(mpControl.main.sample, 0, 0);
    }
}
