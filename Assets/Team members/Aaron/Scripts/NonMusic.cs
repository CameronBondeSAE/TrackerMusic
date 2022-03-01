using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonMusic : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    
    public float scaleMultiplier;
    public float time;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(DoStuff());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    IEnumerator DoStuff()
    {
        while (time < duration)
        {
            float otherChange = time;
            meshRenderer.material.SetFloat("_BasicFloat",time * scaleMultiplier);
            time ++;
            yield return new WaitForSeconds(.2f);
        }
    }
}
