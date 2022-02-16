using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theCreature : MonoBehaviour
{
    public bool auto = false;
    public float autoGenerateTime = 30;

    private GameObject parent;
    private GameObject left;
    private GameObject right;

    public List<GameObject> shapes;
    public Object[] materials;

    // Start is called before the first frame update
    void Start()
    {
        if (auto)
        {
            InvokeRepeating("createCreature", 0.0f, autoGenerateTime);
        }
        
        
    }

    void createCreature()
    {
        Destroy(parent);

        Material material = (Material)materials[Random.Range(0, materials.Length)];


        parent = new GameObject();
        left = new GameObject();
        right = new GameObject();

        parent.name = "creature (parent)";
        left.name = "left";
        right.name = "right";


        left.transform.parent = parent.transform;
        right.transform.parent = parent.transform;

        int bits = Random.Range(10, 30);
        for (int i = 0; i < bits; i++)
        {
            int shapeChosen = Random.Range(0, shapes.Count);
            float size = Random.Range(0.1f, 2.1f);
            float spin = Random.Range(-270, 270);
            
            GameObject piece1 = Instantiate(shapes[shapeChosen], new Vector3(Random.Range(0.35f, 3.0f), Random.Range(-1.0f, 3.0f), Random.Range(-1.0f, 3.0f)), Quaternion.identity);
            GameObject piece2 = Instantiate(shapes[shapeChosen], new Vector3(-piece1.transform.position.x, piece1.transform.position.y, piece1.transform.position.z), Quaternion.identity);
            piece1.GetComponent<Renderer>().material = material;
            piece2.GetComponent<Renderer>().material = material;
            piece1.transform.parent = left.transform;
            piece1.transform.localScale = new Vector3(size, size, size);
            piece2.transform.localScale = new Vector3(-piece1.transform.localScale.x, piece1.transform.localScale.y, piece1.transform.localScale.z);
            piece1.transform.rotation = Quaternion.Euler(0, 0, spin);
            piece2.transform.rotation = Quaternion.Inverse(piece1.transform.rotation);
            piece2.transform.parent = right.transform;
            piece1.SetActive(true);
            piece2.SetActive(true);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
