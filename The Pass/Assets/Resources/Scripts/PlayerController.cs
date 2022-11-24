using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDataPresistence
{
    //czy gracz jest w trakcie ruchu
    bool isMoving;
    //czy gracz jest w trakcie obrotu
    bool isRotating;
    //współżędne 3D pozycji aktualnej i docelowej.
    private Vector3 origPos, targetPos;
    //czas wykonania jednego kroku
    public float timeToMove = 0.8f;
    //odległość kroku
    public float tileSize = 2f;
    //czas odejmowany od czasu wykonania kroku, gdy wciskamy w (czyli tryb chodu do przodu) 
    public float forwardSpeed = 0.2f;
    //czas odejmowany od czasu wykonania kroku, gdy wciskamy shift (czyli tryb biegu) 
    public float shiftSpeed = 0.2f;

    CameraController cc;

    void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }
    //Instrukcia wykonywana co kaltkę 
    void Update()
    {
        //Debug.Log(transform.rotation);

        //Debug.Log("transform: "+ transform.position);

        // Wykrywanie wciśnietych klawiszy (nie tylko pojedynczego klinięcia) klawiszy wraz z blokadą wciskania wielokrotnie 
        if (!cc.isLook)
            {
            if (Input.GetKey(KeyCode.W) && !isMoving)
            {


                //sprawdzanie i ustawianie szybkosci do przodu ( sprawdzamy czy biegniemy)
                float finalSpeed = forwardSpeed;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    finalSpeed = forwardSpeed + shiftSpeed;
                    Debug.Log("Speed: " + finalSpeed);
                }

                //Za pomocą Coroutine inicjujemy pojedynczy krok(jeśli przycisk jest wcisnięty po zakończeniu kroku, zaczyna się następny)
                StartCoroutine(MovePlayer(transform.forward, timeToMove - finalSpeed));
                //timeToMove += 0.1f;
            }
            if (Input.GetKey(KeyCode.S) && !isMoving)
                StartCoroutine(MovePlayer(-transform.forward, timeToMove));
            if (Input.GetKey(KeyCode.A) && !isMoving)
                StartCoroutine(MovePlayer(-transform.right, timeToMove));
            if (Input.GetKey(KeyCode.D) && !isMoving)
                StartCoroutine(MovePlayer(transform.right, timeToMove));

            // to samo co wyżej lecz z obrotem
            if (Input.GetKey("e"))
            {
                StartCoroutine(RotateM(Vector3.up * 90, 0.8f));
            }
            if (Input.GetKey("q"))
            {
                StartCoroutine(RotateM(Vector3.up * -90, 0.8f));
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
        data.rotationX=transform.eulerAngles.x ;
        data.rotationY=transform.eulerAngles.y ;
        data.rotationZ=transform.eulerAngles.z ;
        //data.playerRotation = this.transform.rotation;
        //Debug.Log("Seved in global manager = " + data.inGameTimeToSave);
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        transform.rotation = Quaternion.Euler(data.rotationX, data.rotationY, data.rotationZ);
        //this.transform.rotation = data.playerRotation;
        //Debug.Log("Loaded in global position = " + data.playerPosition);
    }
    private IEnumerator MovePlayer(Vector3 direction, float moveSpeed)
    {
        //ustalamy skąd będziemy rysowali promień sprawdzający przeszkody (w tym przypadku jest on z pozycji obiektu, lecz obniżony o 0.5f)
        Vector3 fromDirection = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        //zmienna przechowująca wartości promiania
        RaycastHit hit;
        //czy natrafiliśmy na przeszkodę
        bool hitObstacle = false;

       



        //wykluczenie z preszkód różnych obiekótw (w tym przypadku obiektów o ragach "Ramp" i "RampDonw"
        if (Physics.Raycast(fromDirection, direction, out hit, tileSize) && hit.collider.tag == "Ramp")
        {           
            direction = (new Vector3(direction.x, direction.y + 0.5f, direction.z));
        }
        else if(Physics.Raycast(fromDirection, direction, out hit, tileSize) && hit.collider.tag == "RampDown")
        {
            direction = (new Vector3(direction.x, direction.y - 0.5f, direction.z));
        }

        //wykrycie prszeszkody (czyli wykrycie collidera)
        else if(hit.collider)
        {
            hitObstacle = true;
        }


        //jeśli możemy to się poruszamy
        if (!hitObstacle&&!isRotating&&!isMoving)
        {
            
            //Debug.Log("move");
             
            //ustalamy czy się poruszamy
            isMoving = true;
            //opuźnienie ruchu(ujemne wartości działają)
            float elapsedtime = 0;
            //pozycja początkowa transform.position to to samo co Vector3 lecz nie używamy jej, gdysz nie musimy jej modyfikować(w przypadku "Vector3 fromDirection" wyżej użyliśmy Vector3 gdyż modyfikowaliśmy wartość y)
            var origPos = transform.position;
            //ustalamy pozycje docelową
            var targetPos = origPos + direction * tileSize;

            while (elapsedtime < moveSpeed)
            {
                //Debug.Log("moveing origPos: " + origPos+ " targetPos: "+ targetPos);
                Debug.DrawRay(fromDirection, direction * tileSize, Color.green);
                Debug.DrawRay(fromDirection, Vector3.forward * tileSize, Color.red);
                //zmieniamy pozycję w czasie. 
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedtime / moveSpeed));
                elapsedtime += Time.deltaTime;
                yield return null;
            }

            //na końcu pentli upeniamy się że obiekt jest na miejscu docelowym czyli poruszył się o pełną wartość tileSize (zabezpieczenia) 
            transform.position = targetPos;

            //koniec ruchu
            isMoving = false;
        }
    }

    //zasada działania taka sama jak powyżej tylko do rotacji
    IEnumerator RotateM(Vector3 byAngles, float inTime)
    {
        if (!isMoving&&!isRotating )
        {
            isRotating = true;
            var fromAngle = transform.rotation;
            var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
            for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
            {
                transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                yield return null;
            }
            transform.rotation = toAngle;
            isRotating = false;
        }
    }
}
