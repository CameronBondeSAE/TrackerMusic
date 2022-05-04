using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEditor.Search;
using UnityEngine;
using Random = UnityEngine.Random;


public class Ollie : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;

    #region OldStuff
    public GameObject prefabNote;
    private GameObject cube;
    public List<GameObject> cubes;
    public List<Byte> notes;
    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private float duration;
    public short volume;
    
    public Vector3 deadCubePosition;
    public GameObject spherePrefabOld;
    public GameObject cylinderPrefab;
    #endregion

    #region Sierpinski Triangle Stuff
    private Transform[] points;
    public Transform point1, point2, point3;
    public Transform initialOrigin;
    private Transform origin;
    private Transform target;
    private Vector3 halfwayPoint;
    public GameObject spherePrefab;
    private GameObject sphere;
    private List<GameObject> sphereList;


    #endregion
    
    void Start()
    {
        // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        // GPG230 stuff
        UnityThread.initUnityThread();
        
        cubes = new List<GameObject>();
        
        points = new[] {point1, point2, point3};
        origin = initialOrigin;
    }

    // GPG230 stuff
    private void ModPlayerOnNoteEvent(MP_CONTROL mpcontrol)
    {
        UnityThread.executeInUpdate(() =>
        {
            NotePlayedEvent(mpcontrol);
        });
    }

    // Your code goes here
    private void NotePlayedEvent(MP_CONTROL newNotePlayed)
    {
        //CubesAndSpheres(newNotePlayed);
        // ^^ Old idea
        
        //instantiate one sphere (origin) randomly within the triangle
        //first rule is to select one of the three points at random (random)
        //instantiate new sphere halfway between origin and random
        //origin is now new origin, random is new random
        //repeat
        
        NewPointInTriangle(newNotePlayed);
    }

    void NewPointInTriangle(MP_CONTROL newNotePlayed)
    {
        if (newNotePlayed.main.sample >= 5)
        {
            target = points[Random.Range(0, points.Length)];
            halfwayPoint = (target.position + origin.position) / 2;
            sphere = Instantiate(spherePrefab);
            sphereList.Add(sphere);
            sphere.transform.position = halfwayPoint;
            origin = sphere.transform;
        }

        if (newNotePlayed.main.sample == 3)
        {
            sphereList[Random.Range(0, sphereList.Capacity-1)].transform
                .DOPunchScale(new Vector3(5f, 5f, 5f), 1f,1,0.5f);
        }
    }

    #region Old Idea For Cubes Spawning and flying at player, exploding on collisions
    
    void CubesAndSpheres(MP_CONTROL newNotePlayed)
    {
        targetPosition = new Vector3(0,0,0);
        //doesn't add to list :(
        notes = new List<byte>(newNotePlayed.main.note);
        notes.Add(newNotePlayed.main.note);

        if (newNotePlayed.main.sample >= 10)
        {
            cube = Instantiate(prefabNote);
            cube.GetComponent<OllieCube>().ollie = this;
            cube.transform.position = new Vector3(Random.Range(-70, 70), Random.Range(-20, 10), 350);
            cubes.Add(cube);
        }

        if (newNotePlayed.main.sample == 3 && cubes.Count>= 1)
        {
            GameObject oneCube = cubes[Random.Range(0, cubes.Count)];
            deadCubePosition = oneCube.transform.position;
            GameObject o = Instantiate(spherePrefab);
            o.transform.position = deadCubePosition;
            cube.GetComponent<OllieCube>().tweener.Kill();
            Destroy(oneCube);
        }
        
        if (newNotePlayed.muted <= 0)
        {
            //list is not adding bytes - maybe an issue with <Byte>?
            //notes = new List<Byte>(newNotePlayed.main.sample);
            for (int i = 0; i < notes.Count; i++)
            {
                //notes.Add(newNotePlayed.main.sample);
            }
            //MuteManagerUI.activeChannels MIGHT work, but cannot access activeChannels
            //for (int j = 0; j < MuteManagerUI.activeChannels.Length; j++)
            {
                GameObject go = Instantiate(cylinderPrefab);
                short mpControlVolume = (short) (newNotePlayed.volume / 40);
                go.transform.position = new Vector3(newNotePlayed.anote, -10, 0) + new Vector3(newNotePlayed.main.sample, -10, 0);
            }
            
            // go.transform.localScale = new Vector3(mpControlVolume, mpControlVolume, mpControlVolume);
            // go.GetComponent<Renderer>().material.color = colours[mpControl.main.sample];
				
            // HDRP renderer doesn't seem to like material.colour as above. So change the shader variable directly
            //go.GetComponent<Renderer>().material.SetVector("_Colour", colours[newNotePlayed.main.sample]);
        }

        //set volume publicly to access from sphere?
        volume = newNotePlayed.volume;
    }
    #endregion
}