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

    //3d stuff
    private GameObject parent;
    private GameObject left;
    private GameObject right;
    public List<GameObject> shapes;
    public Object[] materials;
    public GameObject core;

    //light stuff
    public GameObject lightingRig;
    public List<GameObject> spotlights;

    void Start()
    {
        //lights

        // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        // GPG230 stuff
        UnityThread.initUnityThread();
    }

    // GPG230 stuff
    private void ModPlayerOnNoteEvent(MP_CONTROL mpcontrol)
    {
        UnityThread.executeInUpdate(() => { NotePlayedEvent(mpcontrol); });
    }

    private void NotePlayedEvent(MP_CONTROL newNotePlayed)
    {
        // Your code goes here
        // Debug.Log(newNotePlayed.anote + " : Vol = "+newNotePlayed.volume);
        if (newNotePlayed.main.sample == 6 && newNotePlayed.muted <= 0)
        {
            createCreature();
        }

        if (newNotePlayed.main.sample == 5 && newNotePlayed.muted <= 0)
        {
            shakeTheCore();
        }
        if (newNotePlayed.main.sample == 4 && newNotePlayed.muted <= 0)
        {
            spinLights();
        }        
        if (newNotePlayed.main.sample == 17 && newNotePlayed.muted <= 0)
        {
            flashLights();
            Debug.Log("light flashed");
        }


        //creature spawner 
        void createCreature()
        {
            Destroy(parent);

            Material material = (Material) materials[Random.Range(0, materials.Length)];


            parent = new GameObject();
            left = new GameObject();
            right = new GameObject();

            parent.name = "creature (parent)";
            left.name = "left";
            right.name = "right";


            left.transform.parent = parent.transform;
            right.transform.parent = parent.transform;

            int bits = Random.Range(2, 4);
            for (int i = 0; i < bits; i++)
            {
                int shapeChosen = Random.Range(0, shapes.Count);
                float size = Random.Range(0.3f, 3f);
                float spin = Random.Range(-360, 360);

                GameObject piece1 = Instantiate(shapes[shapeChosen],
                    new Vector3(Random.Range(4f, 12f), Random.Range(-4.0f, 4f), Random.Range(4f, 12f)),
                    Quaternion.identity);
                GameObject piece2 = Instantiate(shapes[shapeChosen],
                    new Vector3(-piece1.transform.position.x, piece1.transform.position.y, piece1.transform.position.z),
                    Quaternion.identity);
                piece1.GetComponent<Renderer>().material = material;
                piece2.GetComponent<Renderer>().material = material;
                piece1.transform.parent = left.transform;
                //piece1.transform.localScale = new Vector3(size, size, size);
                piece1.transform.DOScale(new Vector3(size, size, size), 0.5f).SetEase(Ease.InOutBack);
                piece2.transform.DOScale(new Vector3(size, size, size), 0.5f).SetEase(Ease.InOutBack);
                //piece2.transform.localScale = new Vector3(-piece1.transform.localScale.x, piece1.transform.localScale.y, piece1.transform.localScale.z);
                piece1.transform.rotation = Quaternion.Euler(0, 0, spin);
                piece2.transform.rotation = Quaternion.Inverse(piece1.transform.rotation);
                piece2.transform.parent = right.transform;
                piece1.SetActive(true);
                piece2.SetActive(true);

            }
        }
        void shakeTheCore()
        {
            float size = Random.Range(3f, 5f);

            core.transform.DOScale(new Vector3(size, size, size), 0.5f).SetEase(Ease.OutBounce);
        }

        void spinLights()
        {
            float spin = Random.Range(-360, 360);
            lightingRig.transform.DORotate(new Vector3(spin, spin, spin), 2.5f, RotateMode.Fast);
        }

        void flashLights()
        {
            int lightChosen = Random.Range(0, spotlights.Count);
            for (int i = 0; i < lightChosen; i++)
            {
                //spotlights[lightChosen].GetComponent<Light>().enabled = true;
                //spotlights[lightChosen].GetComponent<Light>().enabled = false;
                spotlights[lightChosen].GetComponent<Light>().intensity = 3500;
                spotlights[lightChosen].GetComponent<Light>().DOIntensity(0, 0.5f).SetEase(Ease.Flash);
            }
        }
        
    }
}
