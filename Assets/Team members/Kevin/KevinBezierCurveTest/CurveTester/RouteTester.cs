using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteTester : MonoBehaviour
{
    //[SerializeField] private Transform[] checkPoints;
    public int checkpointAmount;
    public GameObject[] checkpointArray;

    public GameObject checkpointPrefab;
    public Transform p1;
    public Transform p2;
    public Transform p3;
    public Transform p4;
    [SerializeField] private float t;

    public GameObject cubeO;

    public bool wrap = false;

    public bool pingPong = false;

    public bool reversable = false;
    // Start is called before the first frame update
    void Start()
    {
        cubeO = Instantiate(cubeO);
        cubeO.transform.position = checkpointPrefab.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
            Vector3 a = Vector3.Lerp(p1.position, p2.position, t);
            Vector3 b = Vector3.Lerp(p2.position, p3.position, t);
            Vector3 c = Vector3.Lerp(p3.position, p4.position, t);
            Vector3 d = Vector3.Lerp(a ,b , t);
            Vector3 e = Vector3.Lerp(b ,c , t);
            cubeO.transform.position = Vector3.Lerp(d, e, t);
            if(t<1 && reversable == false)
            {
                t += Time.deltaTime;
                if (t > 1 && pingPong)
                {
                    reversable = true; 
                    if (t > 1 && reversable)
                    {
                        t -= Time.deltaTime;
                  
                    }
                }
                
                
            }
            if (t > 1 && wrap)
            {
                t = 0;
            }

            if (pingPong && reversable)
            {
                t -= Time.deltaTime;
                if (t < 0 && reversable)
                {
                   reversable = false; 
                }
                
            }
    }
}
