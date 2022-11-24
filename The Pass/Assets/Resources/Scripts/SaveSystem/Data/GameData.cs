using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public float inGameTimeToSave;
    public float tirednessToSave;
    public Vector3 playerPosition;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public SerializabeleDictionary<string, bool> observationToSave;
    public SerializabeleDictionary<string, bool> dialogueLineToSave;
    // Start is called before the first frame update


    //There are defoult values
 public GameData()
    {

        this.inGameTimeToSave = 0;
        this.tirednessToSave = 0;
        playerPosition = new Vector3(498, 1, 490);
        rotationX = 0;
        rotationY = 0;
        rotationZ = 0;
        observationToSave = new SerializabeleDictionary<string, bool>();
        dialogueLineToSave = new SerializabeleDictionary<string, bool>();      
        //Debug.Log("In Game Time w Game Data= "+playerPosition+" Vector3: "+ playerPosition);
    }
}
