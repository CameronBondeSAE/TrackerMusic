using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        DOTween.To(Setter, 0, 1f, 5f);
    }

    void Setter(float pnewvalue)
    {
        Debug.Log(pnewvalue);
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
        //cubeO.transform.position = new Vector3(40f, -6f, 25f);
        //cubeO.GetComponent<KevinCube>().instrument = newNotePlayed.main.sample;
        //cubeO.GetComponent<KevinCube>().note = newNotePlayed.anote;
        //cubeO.GetComponent<KevinCube>().volume = newNotePlayed.volume;
        short mpControlVolume = (short) (mpControl.volume / 40);
        cubeO.transform.position = new Vector3(mpControl.anote, 0, 0) + new Vector3(mpControl.main.sample, 0, 0);
        cubeO.transform.DOMove(new Vector3(mpControl.anote - 10f,cubeO.transform.position.y + 10f,0f),1f).SetEase(Ease.InOutSine).OnComplete(
            () => cubeO.transform.DOMoveY(- 20f,1f).SetEase(Ease.InOutSine).OnComplete(
                () => cubeO.transform.DOMove(new Vector3(cubeO.transform.position.x + 55f,cubeO.transform.position.y + 30f,0f),1f).SetEase(Ease.InOutSine).OnComplete(
                    ()=>cubeO.transform.DOMoveY(- 20f,1f).SetEase(Ease.InOutSine).OnComplete(
                        ()=> cubeO.transform.DOMove(new Vector3(mpControl.anote - 10f,cubeO.transform.position.y + 30f,0f),1f).SetEase(Ease.InOutSine)))));
        
        //cubeO.transform.DORotate(new Vector3(360f, 0f, 0f), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine).SetLoops(-1);
        //cubeO.transform.DOMove(cubeO.transform.position, 2.0f * 0.5f).SetEase(Ease.OutBounce);


    }
    
    /*private void NotePlayedEvent(MP_CONTROL newNotePlayed)
    {   
        // Your code goes here
        GameObject cubeO = Instantiate(prefabCube);
        cubeO.transform.position = new Vector3(40f, -6f, 25f);
        cubeO.GetComponent<KevinCube>().instrument = newNotePlayed.main.sample;
        cubeO.GetComponent<KevinCube>().note = newNotePlayed.anote;
        cubeO.GetComponent<KevinCube>().volume = newNotePlayed.volume;
        //short mpControlVolume = (short) (mpControl.volume / 40);
        //cubeO.transform.position = new Vector3(mpControl.anote, 0, 0) + new Vector3(mpControl.main.sample, 0, 0);
        //cubeO.transform.DOMove(cubeO.transform.position, 2.0f * 0.5f).SetEase(Ease.OutBounce);
        
        
    }*/
}
