using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class CheatTween : MonoBehaviour
{
    public SharpMikManager sharpMikManager;
    public GameObject prefabCylinder; 
    void Start()
    {
  
        // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        // GPG230 stuff
        UnityThread.initUnityThread();
        //Cheat version using convenience functions made by DOTween guy
        //transform.DOScale(new Vector3(5f, 5f, 5f), 4f);

        DOTween.To(Setter, 0, 1f, 5f);
    }
    
    private void ModPlayerOnNoteEvent(MP_CONTROL mpcontrol)
    {
        throw new System.NotImplementedException();
    }

    private void Setter(float pnewvalue)
    {
        Debug.Log(pnewvalue);
    }
    
    //DOTweens are better triggered not updated 
    //Will use events
    //Polling is bad for some things
    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
    
    //Couroutine Refresher 
    /*void Start()
    {
        StartCoroutine(DoThingOverTime()); 
    }
    public IEnumerator DoThingOverTime()
    {
        Debug.Log("Hi!");
        yield return new WaitForSeconds(2);
        Debug.Log("Hey!");
        yield return new WaitForSeconds(2);
        Debug.Log("Bye!");
        yield return new WaitForSeconds(2);
        Debug.Log("Laters!");
    }*/
}
