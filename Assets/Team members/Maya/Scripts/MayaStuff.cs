using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
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
    private GameObject cubeParent;
    private GameObject cubeLeft;
    private GameObject cubeRight;
    private Vector3 defaultCoreScale;
    public List<GameObject> shapes;
    public Object[] materials;
    public GameObject core;
    
    //text stuff
    /*public GameObject textBox;
    private GameObject textParent;
    private GameObject textLeft;
    private GameObject textRight;
    public List<String> wordsToUse;*/
    public GameObject textBox;
    public TMP_FontAsset[] fonts;
    public Transform[] textPoints;
    private String textToWrite;
    

    //light stuff
    public GameObject lightingRig;
    public List<GameObject> spotlights;
    
    //the dance floor (perlin noise)
    public GameObject origin;
    public int maxCubes;
    public Vector3 originOffset;
    public Vector3 sceneFloor;
    public GameObject prefabCube;
    public float divisor;

    void Start()
    {
        //3d stuff
        defaultCoreScale = core.GetComponent<Transform>().localScale;
        DOTween.SetTweensCapacity(1000, 1000);
        
        //dance floor
        sceneFloor = new Vector3(0, -5, -15);
        originOffset = new Vector3(maxCubes / 2f, 0, maxCubes / 2f);
        MakeSomeNoise();
        
        //text setup
        textBox.SetActive(false);


        // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        // GPG230 stuff
        UnityThread.initUnityThread();
    }

    private void MakeSomeNoise()
    {
        //sceneFloor = new Vector3(0, -5, -15);
        
        origin = new GameObject("middle");
        origin.transform.position = sceneFloor;
        //origin = Instantiate(origin, sceneFloor, Quaternion.identity);
        for (int j = 0; j < maxCubes; j++)
        {
            for (int i = 0; i < maxCubes; i++)
            {
                GameObject spawnedCube = Instantiate(prefabCube, origin.transform);
                spawnedCube.transform.localPosition = new Vector3(j - originOffset.x, Mathf.PerlinNoise(j/divisor, i/divisor), i - originOffset.z);
                spawnedCube.transform.localScale = new Vector3(1, Mathf.PerlinNoise(j/divisor, i/divisor)*2f, 1);
            }
        }
    }

    // GPG230 stuff
    private void ModPlayerOnNoteEvent(MP_CONTROL mpcontrol)
    {
        UnityThread.executeInUpdate(() => { NotePlayedEvent(mpcontrol); });
    }

    private void NotePlayedEvent(MP_CONTROL newNotePlayed)
    {
        // Your code goes here
        Debug.Log("Vol = "+newNotePlayed.volume);
        if (newNotePlayed.main.sample == 6 && newNotePlayed.muted <= 0)
        {
            textBox.GetComponent<TextMeshPro>().font = fonts[1];
            textToWrite = "PAP";
            createText();
            createCreature();
        }

        if (newNotePlayed.main.sample == 4 && newNotePlayed.volume == 64)
        {
            textBox.GetComponent<TextMeshPro>().font = fonts[0];
            textToWrite = "BOOM";
            createText();
            shakeTheCore();
            //Debug.Log(newNotePlayed.anote + " : Vol = "+newNotePlayed.volume);
        }
        if (newNotePlayed.main.sample == 8 && newNotePlayed.muted <= 0)
        {
            //createText();
            makeCubes();
            spinLights();
        }
        if (newNotePlayed.main.sample == 5 && newNotePlayed.muted <= 0)
        {
            textToWrite = "TSS";
           // createText();
            flashLights();
            
            //Debug.Log("light flashed");
        }
        if (newNotePlayed.main.sample == 19 && newNotePlayed.muted <= 0)
        {
            textToWrite = "SPSssh";
            //createText();
            //Debug.Log("light flashed");
        }

        void createText()
        {
            textBox.SetActive(false);
            textBox.GetComponent<TextMeshPro>().text = textToWrite;
            int point = Random.Range(0, 7);
            textBox.transform.position = new Vector3(textPoints[point].transform.position.x,
                textPoints[point].transform.position.y, textPoints[point].transform.position.z);
            textBox.transform.rotation = textPoints[point].rotation;
            textBox.SetActive(true);
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
                int shapeChosen = Random.Range(0, 1);
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
        //cube maker]
        void makeCubes()
        { 
            Destroy(cubeParent, 10f);
            //Material material = (Material) materials[Random.Range(0, materials.Length)];


            cubeParent = new GameObject();
            cubeLeft = new GameObject();
            cubeRight = new GameObject();
            

            cubeParent.name = "cube parent";
            cubeLeft.name = "left of cube";
            cubeRight.name = "right of cube";

            cubeLeft.transform.parent = cubeParent.transform;
            cubeRight.transform.parent = cubeParent.transform;

            int cubes = 10;
            for (int i = 0; i < cubes; i++)
            {
                float sizeByVol = newNotePlayed.volume;
                
                GameObject side1 = Instantiate(shapes[2],
                    new Vector3(Random.Range(1, 50), 0, 13f),
                    Quaternion.identity);
                side1.transform.localScale = new Vector3(0.1f, sizeByVol *10, 0.1f);
                Vector3 side1Pos = side1.transform.position;
                GameObject side2 = Instantiate(shapes[2],
                    new Vector3(-side1Pos.x, side1Pos.y, side1Pos.z),
                    Quaternion.identity);
                side2.transform.localScale = new Vector3(0.1f, (sizeByVol * 10), 0.1f);
                side1.transform.parent = cubeLeft.transform;
                side1.transform.DOMove(new Vector3(-1000, 0, 0), 90f);
                side2.transform.DOMove(new Vector3(1000, 0, 0), 90f);
                cubeLeft.transform.DORotate(new Vector3(0, 0, -359), 10f);
                cubeRight.transform.DORotate(new Vector3(0, 0, 359), 10f);
                //side1.transform.DOScale(new Vector3(0.1f, 1, 0.1f), 2f);
                //side2.transform.DOScale(new Vector3(0.1f, 1, 0.1f), 2f);
                side2.transform.parent = cubeRight.transform;
                
                side1.SetActive(true);
                side2.SetActive(true);
                
            }
   



        }
        

        //Orb shaker
        void shakeTheCore()
        {
            float size = 5f;
            //Vector3 defaultSize = core.GetComponent<Transform>().localScale;
           //float size = newNotePlayed.volume / 10f;

            core.transform.DOScale(new Vector3(size, size, size), 0.75f).SetEase(Ease.OutBounce);
            core.transform.DOScale(new Vector3(defaultCoreScale.x, defaultCoreScale.y, defaultCoreScale.z), 0.25f);
        }
        //point lights
        void spinLights()
        {
            float spin = Random.Range(-360, 360);
            lightingRig.transform.DORotate(new Vector3(spin, spin, spin), 2.5f, RotateMode.Fast);
        }
        //spotlights
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