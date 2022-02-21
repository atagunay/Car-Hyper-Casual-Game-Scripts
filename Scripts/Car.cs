using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    public static int processIndex = 0;

    [SerializeField]
    private GameObject picUpParticle;

    [SerializeField]
    private GameObject obstacleParticle;

    [SerializeField]
    private Transform pickUpParticleSpawnPoint;

    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    private GameObject winPanel;

    [SerializeField]
    private GameObject losePanel;


    private float moveForce;
    bool flag = true;
  

    [SerializeField]
    private GameObject [] carElements = new GameObject[10];

    [SerializeField]
    private GameObject[] levelElements = new GameObject[5];

    private Stack<GameObject> carStack = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 30);
        moveForce = 0.5f;

        levelElements[Random.Range(0,11)].SetActive(true);
        processIndex = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(flag == true)
        {
            
            if(gameObject.transform.position.x <= -5 && gameObject.transform.position.x >= -6)
            {
                gameObject.transform.position += new Vector3(2, 0, 0);
            }
            else if(gameObject.transform.position.x >= 26 && gameObject.transform.position.x <= 27)
            {

                gameObject.transform.position -= new Vector3(2, 0, 0);
            }

            gameObject.GetComponent<Rigidbody>().transform.position += new Vector3(joystick.Horizontal * moveForce, 0, 0);
            gameObject.transform.DORotate(new Vector3(joystick.Horizontal * 22, 90, 0), 1f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickup")
        {
            Destroy(other.gameObject);
            GameObject particle = Instantiate(picUpParticle, pickUpParticleSpawnPoint.position, Quaternion.identity);
            Destroy(particle, 1.2f);

            carStack.Push(carElements[processIndex]);
            carStack.Peek().SetActive(true);

            processIndex = processIndex + 1;

            Debug.Log(processIndex);
        }
        else if (other.gameObject.tag == "finish")
        {
            Debug.Log("finishe girdi");
            //Camera.main.transform.DOMove(new Vector3(-22, 10, 485), 1);

            CamFollow.CanFollow = false;
            //Camera.main.transform.DORotate(new Vector3(30, 45, 0), 0.5f);


            //Destroy(Camera.main.GetComponent<CamFollow>());
            //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //gameObject.transform.DOMove(new Vector3(10, -0.8f, 508), 1f);
            //gameObject.transform.DORotate(new Vector3(0, 45, 0), 1f);

        }
        else if (other.gameObject.tag == "obstacles")
        {
            Debug.Log("girdi");

            if (carStack.Count != 0)
            {
                //    Debug.Log("stack bos degil");
                //    Vector3 position = gameObject.transform.position;
                //    position.y = position.y + 10;

                /*
                GameObject deneme = Instantiate(carStack.Peek(), gameObject.transform.position, Quaternion.identity);
                deneme.AddComponent<Rigidbody>().AddForce(new Vector3(1000, 200, 2000));
                deneme.transform.localScale = new Vector3(5, 5, 5);
                */

                GameObject particle = Instantiate(obstacleParticle, pickUpParticleSpawnPoint.position, Quaternion.identity);
                Destroy(particle, 1.2f);

                carStack.Pop().SetActive(false);
                processIndex = processIndex - 1;
            }

            Debug.Log(processIndex);

        }

        else if (other.gameObject.tag == "duvar")
        {
            Debug.Log("duvara girdi");

            for (int i = 0; i < 23; i++)
            {
                other.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().AddExplosionForce(700f, transform.gameObject.transform.position, 1000);
                other.transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;



                //other.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(Random.Range(-100,100),Random.Range(-200,200),0);
                //other.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100);
            }

            carStack.Pop().SetActive(false);

            if (carStack.Count == 0)
            {
                losePanel.SetActive(true);
                DOTween.KillAll();
            }
        }
        else if (other.gameObject.tag == "finish2")
        {
            Debug.Log("finish2 girdi");
            flag = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.transform.DOMove(new Vector3(10, -0.8f, 905), 1f);
            gameObject.transform.DORotate(new Vector3(0, 45, 0), 1f).OnComplete(() => DOTween.KillAll());
            winPanel.SetActive(true);

        }



    }
}
