using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aaron
{
    public class AaronMusicSceneScript : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public GameObject cube;
        public MusicThing musicThing;
        
        // Start is called before the first frame update
        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            
            //I need a different way to reference the event without using FindObject
            musicThing.Instrument15PlayedEvent += Ins15Action;
            musicThing.Instrument20PlayedEvent += Ins20Action;
            musicThing.Instrument26PlayedEvent += Ins26Action;
            musicThing.NewInstrumentEvent += NewAction;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Ins15Action(int instrumentPlayed)
        {
            
        }

        void Ins20Action(int instrumentPlayed)
        {
            
        }
        
        void Ins26Action(int instrumentPlayed)
        {
            //Instantiate(cube, new Vector3(instrumentPlayed, 0,0), cube.transform.rotation);
            //Instantiate(cube, new Vector3(-instrumentPlayed, 0, 0), cube.transform.rotation);
            //
            //Debug.Log(meshRenderer.material.color);
        }



        void NewAction(int instrumentPlayed)
        {
            meshRenderer.material.SetColor("_Colour", new Color(Random.value, Random.value, Random.value));
        }
    }
}