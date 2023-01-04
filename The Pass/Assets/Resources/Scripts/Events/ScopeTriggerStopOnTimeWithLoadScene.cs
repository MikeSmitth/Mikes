using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScopeTriggerStopOnTimeWithLoadScene : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("Scoped Object")]
    [SerializeField] GameObject scopedObject;

    [Header("Move Blocker")]
    [SerializeField] GameObject blocker;

    [Header("Time to Stop")]
    [SerializeField] float timeToStop;

    [Header("Interactive Distance Bust")]
    [SerializeField] float interactiveDistanceBust;

    [Header("Scope Boost")]
    [SerializeField] float scope;

    [Header("Scene to Load")]
    [SerializeField]  string SceneName;


    bool allowToLookAt = true;


    CameraController cc;
    GlobalManager gm;
    void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
        gm = GameObject.Find("Managers").GetComponent<GlobalManager>();
    }

    private void OnTriggerEnter(Collider other)
    {       
        
        if (other.tag == "Player")
        {
            //cc.Scope(scope);
            cc.interactiveDistance += interactiveDistanceBust;
            blocker.transform.position += new Vector3(0, 1f, 0);
            timeToStop += gm.inGameTime;
            //Debug.Log(timeToStop + " : " + gm.inGameTime);
        }


    }

 


    private void OnTriggerStay(Collider other)
{
       

            
       if(other.tag == "Player")
        {
  
            //Debug.Log(startTime + " : " + gm.inGameTime);
            // Determine which direction to rotate towards
            
            Vector3 targetDirection = scopedObject.transform.position - other.transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = 1f * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(other.transform.forward, targetDirection, singleStep, 0.0f);
            Vector3 directionDone = Vector3.RotateTowards(other.transform.forward, targetDirection, 1, 0.0f);

            // Draw a ray pointing at our target in
            //Debug.DrawRay(other.transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            other.transform.rotation = Quaternion.LookRotation(newDirection);
            
            if(directionDone == newDirection && allowToLookAt)
            {
                //Debug.Log(" Lookat ");

                cc.lookAt(scopedObject.transform.position);
                allowToLookAt = false;
                cc.isLook = false;

            }


            //Debug.Log(" newDirection: " + newDirection);
            //Debug.Log(" targetDirection: " + targetDirection);
            //Debug.Log(" singleStep: " + singleStep);
            if (timeToStop <= gm.inGameTime)
            {
                //cc.Scope(scope*(-1));
                EventManager.current.SaveGame();
                SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
                Destroy(this);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
