using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody body;
    private bool Dead;
    private Image healthbarimage;

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
    public GameObject Explosion;
    public GameObject CountDown;
    public GameObject healthbar;
    public GameObject damaged;
    public float maxhealth = 100f;
    public float health = 100f;
    public float damage = 25f;

    // Start is called before the first frame update
    void Start()
    {
        Dead = true;
        body = GetComponent<Rigidbody>();
        healthbarimage = healthbar.GetComponent<Image>();
        StartCoroutine(GameStart());
    }

    private void FixedUpdate()
    {
        if (Dead) return;

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
        if (Dead) return;

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

    private void OnCollisionEnter(Collision collision)
    {
        health = health - damage;
        UpdateHealthBar();
        StartCoroutine(Showdamage());
        if (health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "powerup")
        {
            print($"powerup: {other.name}");
        }
    }

    private void UpdateHealthBar()
    {
        healthbarimage.fillAmount = health / maxhealth;
    }
    private IEnumerator Showdamage()
    {
        damaged.SetActive(true);
        yield return new WaitForSeconds(.3f);
        damaged.SetActive(false);
    }

    private IEnumerator Death()
    {
        if (Dead) yield break;

        Dead = true;
        body.velocity = Vector3.zero;
        GetComponent<Renderer>().enabled = false;
        TextMeshProUGUI countDownText = CountDown.GetComponent<TextMeshProUGUI>();

        if (Explosion != null)
        {
            var prefab = Instantiate(Explosion, transform.position, Quaternion.identity);
            
            
            Destroy(
                prefab,
                .3f
            );

        }

        yield return new WaitForSeconds(1f);
        countDownText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownText.text = "";

        //transform.position = new Vector3(Spawn.transform.position.x, transform.position.y, Spawn.transform.position.z);
        GetComponent<Renderer>().enabled = true;
        Dead = false;
        health = maxhealth;
        UpdateHealthBar();
    }

    private IEnumerator GameStart()
    {
        body.velocity = Vector3.zero;
        TextMeshProUGUI countDownText = CountDown.GetComponent<TextMeshProUGUI>();

        yield return new WaitForSeconds(1f);
        countDownText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownText.text = "";

        Dead = false;
    }
}
