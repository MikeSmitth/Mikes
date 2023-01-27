using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSwapper : MonoBehaviour
{
    TerrainChecker checker;
    PlayerController pc;
    string currentLayer;
    public FootstepCollection[] terrainFootstepCollections;
    void Start()
    {
        checker = new TerrainChecker();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void CheckLayers()
    {
       

        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down, out hit,3))
        {
            //sptrawdzamy czy teren jest
            if(hit.transform.GetComponent<Terrain>()!=null)
            {
                Terrain t = hit.transform.GetComponent<Terrain>();
                //WYcinamy tylko piewrwsz¹ czêœæ nazwy tereny, bo na SNIEGi mog¹ mieæ ró¿ne podnazwy 
                string[] LayerName = checker.GetLayerName(transform.position, t).Split(char.Parse("_"));

                if (currentLayer != LayerName[0])
                {
                    currentLayer = LayerName[0];
 
                    foreach (FootstepCollection collection in terrainFootstepCollections)
                    {
                        if(currentLayer == collection.name)
                        {
                            pc.SwapFootsteps(collection);
                        }
                    }
                }
            }

            if(hit.transform.GetComponent<SurfaceTypeForSteps>()!=null)
            {
                FootstepCollection collection = hit.transform.GetComponent<SurfaceTypeForSteps>().footstepCollection;
                currentLayer = collection.name;
                pc.SwapFootsteps(collection);
            }
        }
        //Debug.Log("Terrain: " + currentLayer);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
