using System;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MayaStuff : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;

    //3d stuff
    //dancing shapes
    private GameObject parent;
    private GameObject left;
    private GameObject right; 
    public List<GameObject> shapes;
    public Object[] materials;   
    //background lines
    private GameObject cubeParent;
    private GameObject cubeLeft;
    private GameObject cubeRight;
    //central sphere
    private Vector3 defaultCoreScale;
    public GameObject core;
    //triangles
    [FormerlySerializedAs("triangleController")] public GameObject trianglePrefab;

    //text stuff
    /*public GameObject textBox;
    private GameObject textParent;
    private GameObject textLeft;
    private GameObject textRight;
    public List<String> wordsToUse;*/
    public GameObject textBox;
    public TMP_FontAsset[] fonts;
    public Transform[] textPoints;
    private int pointChosen;
    private int boxChosen;
    private String textToWrite;
    public int textFontSize;
    

    //light stuff
    public GameObject lightingRig;
    public List<GameObject> pointLights;
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
        Debug.Log("instrument = "+newNotePlayed.main.sample +" Vol = "+newNotePlayed.volume);
        if (newNotePlayed.main.sample == 6 && newNotePlayed.volume == 64)
        {
            textToWrite = "snare";
            CreateText();
            CreateCreature();
            //FlashPoints();
        }
        else if (newNotePlayed.main.sample == 6 && newNotePlayed.volume == 48)
            FlashPoints();

        if (newNotePlayed.main.sample == 4 && newNotePlayed.volume == 64)
        {
            textToWrite = "kick";
            ShakeTheCore();
            CreateText();
            //FlashLights();
        }
        if (newNotePlayed.main.sample == 8 && newNotePlayed.muted <= 0)
        {

            //textToWrite = "vwooOOOO";
            //createText();
            FlashPoints();
            //MakeCubes();
            SpinPoints();
        }
        if (newNotePlayed.main.sample == 5 && newNotePlayed.volume == 64)
        {
            textToWrite = "hi-hat";
            CreateText();
            //TriangleSpin();
            FlashLights();
            FlashPoints();
        }
        if (newNotePlayed.main.sample == 19 && newNotePlayed.muted <= 0)
        {
            //FlashLights();
            FlashPoints();
            textToWrite = "crash";
            CreateText();
        }

        if (newNotePlayed.main.sample == 2 && newNotePlayed.volume == 24)
        {
            TriangleSpin();
        }
        if (newNotePlayed.main.sample == 0 && newNotePlayed.volume == 64)
        {
            FlashLights();
            SpinPoints();
        }

        if (newNotePlayed.volume == 0)
        {
            MakeCubes();
        }
    } 
        //textboxes
        void CreateText()
        {
            textFontSize = Random.Range(4, 12);
            pointChosen = Random.Range(0, 9);
            //textToWrite = "something OBVIOUS";
            Quaternion textRot = new Quaternion(Random.Range(-45, 45), Random.Range(-45, 45), Random.Range(-45, 45),
                Random.Range(-45, 45));
            Vector3 textSpin = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
            GameObject textToSpawn = 
                Instantiate(textBox, new Vector3(textPoints[pointChosen].position.x, textPoints[pointChosen].position.y,
                    textPoints[pointChosen].position.z), Quaternion.identity);
            textToSpawn.GetComponent<TextMeshPro>().text = textToWrite;
            textToSpawn.GetComponent<TextMeshPro>().fontSize = textFontSize;
            textToSpawn.transform.DORotate(textSpin, 1.5f, RotateMode.Fast);
            textToSpawn.transform.DOScale((textSpin/10), 1.5f);
        
        
            Destroy(textToSpawn, 0.25f);
        }
    
        //creature spawner 
        void CreateCreature()
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
                    new Vector3(Random.Range(8f, 16f), Random.Range(-2.5f, 6f), Random.Range(8f, 16f)),
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
        void MakeCubes()
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
                float sizeByVol = Random.Range(32, 64);
                
                GameObject side1 = Instantiate(shapes[2],
                    new Vector3(Random.Range(1, 50), 0, 13f),
                    Quaternion.identity);
                side1.transform.localScale = new Vector3(0.1f, sizeByVol *10, 0.1f);
                Vector3 side1Pos = side1.transform.position;
                GameObject side2 = Instantiate(shapes[2],
                    new Vector3(-side1Pos.x, side1Pos.y, (side1Pos.z+0.1f)),
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
        //triangles
        void TriangleSpin()
        {
            int spinFactor = Random.Range(-359, 359);
            GameObject triangleCopy =
                Instantiate(trianglePrefab, new Vector3(0, 0, 8), Quaternion.identity);
            triangleCopy.transform.DOMove(new Vector3(0, 0, -7.75f), 3.5f, false).SetEase(Ease.OutSine);
            triangleCopy.transform.DORotate(new Vector3(0, 0, spinFactor), 3.5f, RotateMode.WorldAxisAdd)
                    .SetEase(Ease.OutSine);
            triangleCopy.SetActive(true);
            Destroy(triangleCopy,4f);
        }

        //Orb shaker
        void ShakeTheCore()
        {
            float size = 5f;
            //Vector3 defaultSize = core.GetComponent<Transform>().localScale;
           //float size = newNotePlayed.volume / 10f;

            core.transform.DOScale(new Vector3(size, size, size), 0.75f);
            core.transform.DOScale(new Vector3(defaultCoreScale.x, defaultCoreScale.y, defaultCoreScale.z), 0.25f);
        }
        //point lights
        void SpinPoints()
        {
            float spin = Random.Range(-360, 360);
            lightingRig.transform.DORotate(new Vector3(spin, spin, spin), 2.5f, RotateMode.Fast);
        }
        //point lights flashing
        void FlashPoints()
        {
            int lightToFlash = Random.Range(0, 5);
            pointLights[lightToFlash].GetComponent<Light>().intensity = 6000;
            pointLights[lightToFlash].GetComponent<Light>().DOIntensity(1750, 0.5f);
        }
        //spotlights flashing
        void FlashLights()
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
