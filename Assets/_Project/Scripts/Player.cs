﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private MeshCollider collider;
    private Rigidbody body;
    private bool Dead;
    private bool boosted;
    private Image healthbarimage;
    private Image ammobarimage;

    public enum EnumShipType
    {
        Red,
        Blue
    }

    public EnumShipType shipType;
    public float speed = 10f;
    public float boostSpeed = 50f;
    public float rotationSpeed = 30f;
    public float maxspeed = 1f;
    public GameObject Spawn;
    public GameObject Explosion;
    public GameObject CountDown;
    public GameObject healthbar;
    public GameObject ammobar;
    public GameObject damaged;
    public GameObject WeaponPort;
    public GameObject Bullet;
    public float maxhealth = 100f;
    public float health = 100f;
    public float ammo = 5f;
    public float maxammo = 5f;
    public float damage = 25f;
    public float bulletSpeed = 5000f;
    public float fireRate = 1f;
    public float lastFireTime;
    public bool Shooting;

    // Start is called before the first frame update
    void Start()
    {
        Dead = true;
        body = GetComponent<Rigidbody>();
        collider = GetComponent<MeshCollider>();

        healthbarimage = healthbar.GetComponent<Image>();
        ammobarimage = ammobar.GetComponent<Image>();

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

        if(boosted)
        {
            if (body.velocity.magnitude > maxspeed * 1.5f)
            {
                body.velocity = body.velocity.normalized * (maxspeed * 1.5f);
            }
        }
        else
        {
            if (body.velocity.magnitude > maxspeed)
            {
                body.velocity = body.velocity.normalized * maxspeed;
            }
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
            if(Input.GetKey(KeyCode.LeftShift))
            {
                StartCoroutine(Shoot());

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
            if(Input.GetKey(KeyCode.RightShift))
            {
                StartCoroutine(Shoot());
            }            
        }
    }

    private IEnumerator Shoot()
    {
        if (ammo <= 0) yield break;
        if (Shooting) yield break;
        Shooting = true;

        var bulletPrefab = GameObject.Instantiate(Bullet, WeaponPort.transform.position, Quaternion.identity);
        var bulletBody = bulletPrefab.GetComponent<Rigidbody>();

        ammo = ammo - 1;
        if (ammo < 0)
        {
            ammo = 0;
        }
        ammobarimage.fillAmount = ammo / maxammo;


        bulletBody.AddForce(transform.forward * bulletSpeed);
        Destroy(bulletPrefab, 3f);

        yield return new WaitForSeconds(fireRate);
        Shooting = false;
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
            //print($"powerup: {other.name}");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "powerup")
        {
            switch(other.name)
            {
                case "Boost":
                    boosted = true;
                    body.AddForce(transform.forward * boostSpeed);
                    break;
                case "Heal":
                    if(health <= maxhealth)
                    {
                        health += 1;
                        UpdateHealthBar();
                    }
                    break;
                case "Upgrade":
                    if (ammo <= maxammo)
                    {
                        ammo += 1;
                        ammobarimage.fillAmount = ammo / maxammo;
                    }

                    break;
            }
            print(other.name);
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

        boosted = false;
        Dead = true;

        collider.enabled = false;
        body.detectCollisions = false;
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
        body.detectCollisions = true;
        collider.enabled = true;

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

    private IEnumerator Boost()
    {
        if(boosted) yield break;

        boosted = true;
        yield return new WaitForSeconds(.5f);
        boosted = false;
    }
}
