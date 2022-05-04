using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class Triangle : MonoBehaviour
{
    private Transform[] points;
    public Transform point1, point2, point3;
    public Transform initialOrigin;
    private Transform origin;
    private Transform target;
    private Vector3 halfwayPoint;
    public GameObject spherePrefab;
    private GameObject sphere;
    void Start()
    {
        points = new[] {point1, point2, point3};
        origin = initialOrigin;
    }

    //instantiate one sphere (origin) randomly within the triangle
    //first rule is to select one of the three points at random (random)
    //instantiate new sphere halfway between origin and random
    //origin is now new origin, random is new random
    //repeat
    
    //WOO IT WORKS
    //Just need to merge it into the actual music script, and make it spawn based on the music
    
    // void Update()
    // {
    //     target = points[Random.Range(0, points.Length)];
    //     halfwayPoint = (target.position + origin.position) / 2;
    //     sphere = Instantiate(spherePrefab);
    //     sphere.transform.position = halfwayPoint;
    //     origin = sphere.transform;
    // }
}
