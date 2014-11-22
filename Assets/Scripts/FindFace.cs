using UnityEngine;
using System.Collections;

public class FindFace : MonoBehaviour
{
    private RaycastHit Hit;
    private int LayerMask = 1 << 8;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private bool lastFire = false;
    void Update()
    {
        
        Debug.DrawRay(transform.position, transform.forward * 50, Color.green);

        var fire = Input.GetButton("Fire1");
        if (fire && !lastFire )
        {
            if (Physics.Raycast(transform.position, transform.forward * 50, out Hit, LayerMask))
            {
                BlockBuilder.Select(Hit);
            }
        }
        lastFire = fire;
    }

}