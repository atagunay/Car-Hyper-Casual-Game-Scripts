using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField]
    private GameObject[] personArr = new GameObject[5];

   
    private void Dance()
    {
        for (int i = 0; i < personArr.Length; i++)
        {
            personArr[i].GetComponent<Animator>().SetBool("isDance", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dansa girdi");
        if(other.gameObject.tag == "car")
        {

            Dance();
        }
      
    }
}
