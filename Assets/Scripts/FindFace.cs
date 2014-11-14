using UnityEngine;
using System.Collections;

public class FindFace : MonoBehaviour
{
    private RaycastHit Hit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.DrawRay(transform.position, transform.forward * 50, Color.green);

        if (Physics.Raycast(transform.position, transform.forward * 50, out Hit, 50))
        {
            //Hit.transform.localScale = new Vector3(2, 2, 2);
        }
    }

}