using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
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

    }

    // Update is called once per frame
    void Update()
    {
        if (shipType == EnumShipType.Red)
        {
            if (Input.GetKey(KeyCode.D))
            {
                //transform.Translate(Vector3.left * Time.deltaTime * speed);
                transform.Rotate(Vector3.up, 10f * Time.deltaTime * rotationSpeed, Space.Self);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(transform.forward * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                //transform.Translate(Vector3.right * Time.deltaTime * speed);
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
