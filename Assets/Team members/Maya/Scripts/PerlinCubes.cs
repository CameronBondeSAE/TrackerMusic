using System.Collections;
using System.Collections.Generic;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class PerlinCubes : MonoBehaviour
{
    public GameObject me;
    public float offset;
    
    
    // Start is called before the first frame update
    void Start()
    {
        offset = Random.Range(1, 21);
        me = this.gameObject;
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

    private void NotePlayedEvent(MP_CONTROL newNotePlayed)
    {
        if (newNotePlayed.volume >= 30)
        {
            offset = Random.Range(25, 50);
        }
        else if (newNotePlayed.volume <= 30)
            offset = Random.Range(100, 200);
    }

    // Update is called once per frame
    void Update()
    {
        var localScale = me.transform.localScale;
        localScale = new Vector3(localScale.x, Mathf.PerlinNoise(0, Time.time+offset*2),
            localScale.z);
        me.transform.localScale = localScale;
    }
}
