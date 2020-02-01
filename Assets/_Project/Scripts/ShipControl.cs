using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    private RigidBody body;

    public enum EnumShipType
    {
        Red,
        Blue
    }

    public EnumShipType shipType;
    public float speed = 10f;
    public float rotationSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<RigidBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shipType == EnumShipType.Red)
        {
            if (Input.GetKey(KeyCode.D))
            {
                //transform.Translate(Vector3.left * Time.deltaTime * speed);
                transform.Rotate(Vector3.up, rotationSpeed);
            }
            if (Input.GetKey(KeyCode.W))
            {
                //transform.Translate(Vector3.forward * -1 * Time.deltaTime * speed);
                body.AddForce(transform.forward * speed);

            }
            if (Input.GetKey(KeyCode.A))
            {
                //transform.Translate(Vector3.right * Time.deltaTime * speed);
                transform.Rotate(Vector3.up, -1 * rotationSpeed);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }
    }
}
