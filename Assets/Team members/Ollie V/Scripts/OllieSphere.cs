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

public class OllieSphere : MonoBehaviour
{
    private Tweener tweener;
    private float duration = 4.9f;
    public Rigidbody rigidbody;
    void Start()
    {
        tweener = DOTween.To(Setter, 0, 20, duration).SetEase(Ease.OutBounce);
        Destroy(gameObject,5f);
    }
    
    private void Setter(float newSize)
    {
        transform.localScale = new Vector3((newSize*2), (newSize/4), 1);
        //transform.rotation = new Quaternion(0, 0, (newSize*5), 90);
        rigidbody.AddTorque(0,0,20);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sphere"))
        {
            if (Random.Range(0,4) == 3)
            {
                Destroy(gameObject);
                tweener.Kill();
            }
        }
    }
}
