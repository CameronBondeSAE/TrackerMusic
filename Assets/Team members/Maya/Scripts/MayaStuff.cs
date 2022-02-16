using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MayaStuff : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    
    //creature stuff
    private GameObject parent;
    private GameObject left;
    private GameObject right;
    public List<GameObject> shapes;
    public Object[] materials;
    
    //lights
    public GameObject pointLight1;
    public GameObject pointLight2;
    
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

    private void NotePlayedEvent(MP_CONTROL newNotePlayed)
    {
        // Your code goes here
        // Debug.Log(newNotePlayed.anote + " : Vol = "+newNotePlayed.volume);
        if (newNotePlayed.main.sample == 4 && newNotePlayed.muted <=0)
        {
            createCreature();
        }
        
        
    }
    

    //creature spawner 
    void createCreature()
    {
        Destroy(parent);

        Material material = (Material)materials[Random.Range(0, materials.Length)];


        parent = new GameObject();
        left = new GameObject();
        right = new GameObject();

        parent.name = "creature (parent)";
        left.name = "left";
        right.name = "right";


        left.transform.parent = parent.transform;
        right.transform.parent = parent.transform;

        int bits = Random.Range(2, 5);
        for (int i = 0; i < bits; i++)
        {
            int shapeChosen = Random.Range(0, shapes.Count);
            float size = Random.Range(0f, 3f);
            float spin = Random.Range(-360, 360);
            
            GameObject piece1 = Instantiate(shapes[shapeChosen], new Vector3(Random.Range(0.35f, 3.0f), Random.Range(-1.0f, 3.0f), Random.Range(-1.0f, 3.0f)), Quaternion.identity);
            GameObject piece2 = Instantiate(shapes[shapeChosen], new Vector3(-piece1.transform.position.x, piece1.transform.position.y, piece1.transform.position.z), Quaternion.identity);
            piece1.GetComponent<Renderer>().material = material;
            piece2.GetComponent<Renderer>().material = material;
            piece1.transform.parent = left.transform;
            piece1.transform.localScale = new Vector3(size, size, size);
            //piece1.transform.DOScale(new Vector3(size, size, size), 2f);
            piece2.transform.localScale = new Vector3(-piece1.transform.localScale.x, piece1.transform.localScale.y, piece1.transform.localScale.z);
            piece1.transform.rotation = Quaternion.Euler(0, 0, spin);
            piece2.transform.rotation = Quaternion.Inverse(piece1.transform.rotation);
            piece2.transform.parent = right.transform;
            piece1.SetActive(true);
            piece2.SetActive(true);
            
        }
    }
}
