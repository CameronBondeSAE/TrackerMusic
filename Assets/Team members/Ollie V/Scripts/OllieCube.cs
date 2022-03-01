using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class OllieCube : MonoBehaviour
{
    private Vector3 targetPosition = new Vector3(0,0,0);
    private float duration = 4.9f;
    public Vector3 deadCubePosition;
    public GameObject spherePrefab;
    public Ollie ollie;
    
    //declare it as variable here, allows to "kill" individual cubes' Tweens later
    //preventing Null errors
    public Tweener tweener;

    void Start()
    {
        tweener = DOTween.To(Setter, 0, 10, duration);
        Destroy(gameObject,5f);
    }
    
    private void Setter(float newSize)
    {
        //transform.localScale = new Vector3(newSize, newSize, 1);
        transform.position = new Vector3(transform.position.x, transform.position.y, newSize*-10);
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Cube")
        {
            if (Random.Range(0, 2) == 1)
            {
                deadCubePosition = this.transform.position;
                GameObject o = Instantiate(spherePrefab);
                o.transform.position = deadCubePosition;
                tweener.Kill();
                Destroy(gameObject);
            }
        }
    }*/

    private void OnDestroy()
    {
        tweener.Kill();
        ollie.cubes.Remove(gameObject);
    }
}