using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody body;

    public enum EnumShipType
    {
        Red,
        Blue
    }

    public EnumShipType shipType;
    public float speed = 10f;
    public float rotationSpeed = 30f;
    public float maxspeed = 1f;
    public GameObject Spawn;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (shipType == EnumShipType.Red)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //transform.Translate(Vector3.forward * -1 * Time.deltaTime * speed);
                body.AddForce(transform.forward * speed);
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //transform.Translate(Vector3.forward * -1 * Time.deltaTime * speed);
                body.AddForce(transform.forward * speed);
            }
        }

        if (body.velocity.magnitude > maxspeed)
        {
            body.velocity = body.velocity.normalized * maxspeed;
        }
    }
    void Update()
    {
        if (shipType == EnumShipType.Red)
        {
            if (Input.GetKey(KeyCode.D))
            {
                //transform.Translate(Vector3.left * Time.deltaTime * speed);
                transform.Rotate(Vector3.up, rotationSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                //transform.Translate(Vector3.right * Time.deltaTime * speed);
                transform.Rotate(Vector3.up, -1 * rotationSpeed);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //transform.Translate(Vector3.left * Time.deltaTime * speed);
                transform.Rotate(Vector3.up, rotationSpeed);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //transform.Translate(Vector3.right * Time.deltaTime * speed);
                transform.Rotate(Vector3.up, -1 * rotationSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (Spawn != null)
        {
            transform.position = new Vector3(Spawn.transform.position.x, transform.position.y, Spawn.transform.position.z);
            body.velocity = Vector3.zero;
        }
    }
}
