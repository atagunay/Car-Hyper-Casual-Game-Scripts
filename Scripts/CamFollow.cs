using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;

    public Vector3 offset = new Vector3(0, 20f, -30);
    public Quaternion quaternion = new Quaternion();

    public static bool CanFollow = true;


    void Update()
    {
        if (CanFollow == true)
        {
            Vector3 position = this.transform.position;

            position.x = Player.transform.position.x + offset.x;

            position.z = Player.transform.position.z + offset.z;
            position.y = offset.y;
            this.transform.position = position;
            //this.transform.rotation = quaternion;
            //Debug.Log(quaternion);
        }
        else if(CanFollow == false)
        {
            offset = new Vector3(-40, 40, -35);

            Vector3 position = this.transform.position;

            position.x = Player.transform.position.x + offset.x;

            position.z = Player.transform.position.z + offset.z;
            position.y = offset.y;
            this.transform.position = position;
            this.transform.rotation = quaternion;
        }
        

    }

    
}

