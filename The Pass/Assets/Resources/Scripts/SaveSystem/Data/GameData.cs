using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public float inGameTimeToSave;
    public float tirednessToSave;
    public Vector3 playerPosition;
    public string sceneToSave;
    //by nie ³adowaæ sceny z zapisu gdy siê poruszami miêdzy nimi 
    public bool justMoveToSave;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public SerializabeleDictionary<string, bool> observationToSave;
    public SerializabeleDictionary<string, bool> observationEQShowToSave;
    public SerializabeleDictionary<string, bool> dialogueLineToSave;
    //public SerializabeleDictionary<string, bool> obseravationButtonActive;
    public SerializabeleDictionary<string, Vector3> scenesPositionLoadedToSave;
    public SerializabeleDictionary<string, Quaternion> scenesRotationLoadedToSave;
   
    // Start is called before the first frame update


    //There are defoult values
    public GameData()
    {

        this.inGameTimeToSave = 90f;
        this.tirednessToSave = 0;
        playerPosition = new Vector3(500, 5, 478);
        rotationX = 0;
        rotationY = 0;
        rotationZ = 0;
        observationToSave = new SerializabeleDictionary<string, bool>();

        observationEQShowToSave = new SerializabeleDictionary<string, bool>();
        //observationToSave.Add("", false);

        dialogueLineToSave = new SerializabeleDictionary<string, bool>();
        //dialogueLineToSave.Add("", false);

        justMoveToSave = false;
        sceneToSave = "Scene1";

        //obseravationButtonActive = new SerializabeleDictionary<string, bool>();

        scenesPositionLoadedToSave = new SerializabeleDictionary<string, Vector3>();
        scenesPositionLoadedToSave.Add("Scene1", new Vector3(248, 13, 234));
        scenesPositionLoadedToSave.Add("Scene2", new Vector3(242, 3, 240));
        scenesPositionLoadedToSave.Add("Scene3", new Vector3(500, 1, 490));

        scenesRotationLoadedToSave = new SerializabeleDictionary<string, Quaternion>();
        scenesRotationLoadedToSave.Add("Scene1", new Quaternion(0, -0.7f, 0, 0.7f));     
        scenesRotationLoadedToSave.Add("Scene2", new Quaternion(0, 0, 0, 0));
        scenesRotationLoadedToSave.Add("Scene3", new Quaternion(0, 0, 0, 0));
        //scenesRotationLoadedToSave.Add("", new Quaternion());

        //Debug.Log("In Game Time w Game Data= "+playerPosition+" Vector3: "+ playerPosition);
    }
}
