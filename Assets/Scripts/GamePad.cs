using UnityEngine;
using System.Collections;

public class GamePad : MonoBehaviour
{
    public float Speed = 10.0f;

    private float x = 0.0f;
    private float y = 0.0f;

	// Use this for initialization
	void Start () 
    {	
	}
	
	// Update is called once per frame
	void Update ()
    {
        x += Input.GetAxis("MovementX") * Speed / 4.0f;
        y += Input.GetAxis("MovementZ") * Speed / 4.0f;

        var rot = Quaternion.Euler(y,x,0);

        transform.rotation = rot;

        transform.position += transform.forward * Time.deltaTime * Input.GetAxis("Vertical") * Speed;
        transform.position += transform.right * Time.deltaTime * Input.GetAxis("Horizontal") * Speed;

        if (transform.position.y < 1) transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }
}

