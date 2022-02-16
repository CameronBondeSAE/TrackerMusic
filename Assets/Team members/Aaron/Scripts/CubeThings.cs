using System.Collections;
using System.Collections.Generic;
using Aaron;
using DG.Tweening;
using UnityEngine;

public class CubeThings : MonoBehaviour
{
    private GameObject cube;

    public Material m;

    public int timer = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        cube = this.gameObject;
        StartCoroutine(DestroyCube());
    }

    // Update is called once per frame
    void Update()
    {
        m.DOFade(0, 3 );
    }

    public IEnumerator DestroyCube()
    {
        for (int i = 0; i < timer; i++)
        {
            yield return new WaitForSeconds(1);
        }

        Destroy(cube);
    }
}
