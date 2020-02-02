using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Player : MonoBehaviour
{
    private GameObject shipObject;
    private MeshCollider collider;
    private Rigidbody body;
    private bool Dead;
    private bool boosted;
    private Image healthbarimage;
    private Image ammobarimage;
    private TextMeshProUGUI lapText;
    private Text partsText;
    private Text lifeText;

    public enum EnumShipType
    {
        Red,
        Blue
    }

    public EnumShipType shipType;
    public List<GameObject> ships;
    public float speed = 10f;
    public float boostSpeed = 50f;
    public float rotationSpeed = 30f;
    public float maxspeed = 1f;
    public GameObject Spawn;
    public GameObject Explosion;
    public GameObject CountDown;
    public GameObject LapCount;
    public GameObject healthbar;
    public GameObject ammobar;
    public GameObject PartsCount;
    public GameObject LifeCount;
    public GameObject damaged;
    public GameObject WeaponPort;
    public GameObject Bullet;
    public AudioSource audioSource;
    public List<AudioClip> clips;
    public float maxhealth = 100f;
    public float health = 100f;
    public float ammo = 5f;
    public float maxammo = 5f;
    public float damage = 25f;
    public float bulletSpeed = 5000f;
    public float fireRate = 1f;
    public float lastFireTime;
    public bool Shooting;
    public int currentCheckpoint = 0;
    public List<bool> PassedCheckpoints;
    public int lap = 1;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var ship in ships)
        {
            ship.SetActive(false);
        }


        if(shipType == EnumShipType.Red)
        {
            shipObject = ships[LevelManager.Player1Ship];

            ammo = LevelManager.Player1MaxAmmo;
            maxammo = LevelManager.Player1MaxAmmo;
            health = LevelManager.Player1MaxHealth;
            maxhealth = LevelManager.Player1MaxHealth;
            LevelManager.Player1Win = false;
            LevelManager.Player1Powerups = 0;
            Dead = LevelManager.Player1Life <= 0;
        }
        else
        {
            shipObject = ships[LevelManager.Player2Ship];

            ammo = LevelManager.Player2MaxAmmo;
            maxammo = LevelManager.Player2MaxAmmo;
            health = LevelManager.Player2MaxHealth;
            maxhealth = LevelManager.Player2MaxHealth;
            LevelManager.Player2Win = false;
            LevelManager.Player2Powerups = 0;
            Dead = LevelManager.Player1Life <= 0;
        }

        shipObject.SetActive(true);

        LevelManager.RoundOver = true;
        body = GetComponent<Rigidbody>();
        collider = GetComponent<MeshCollider>();
        
        healthbarimage = healthbar.GetComponent<Image>();
        ammobarimage = ammobar.GetComponent<Image>();
        lapText = LapCount.GetComponent<TextMeshProUGUI>();
        partsText = PartsCount.GetComponent<Text>();
        lifeText = LifeCount.GetComponent<Text>();

        UpdateLife();
        UpdateAmmo();
        UpdateHealthBar();

        StartCoroutine(GameStart());
    }

    private void FixedUpdate()
    {
        if (Dead || LevelManager.RoundOver) return;

        if (shipType == EnumShipType.Red)
        {
            if (Input.GetKey(KeyCode.W))
            {
                body.AddForce(transform.forward * speed);
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
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
        if (Dead || LevelManager.RoundOver) return;

        if (shipType == EnumShipType.Red)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, -1 * rotationSpeed * Time.deltaTime);
            }
            if(Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(Shoot());

            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.up, -1 * rotationSpeed * Time.deltaTime);
            }
            if(Input.GetKey(KeyCode.Keypad0))
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

        UpdateAmmo();
        audioSource.PlayOneShot(clips.FirstOrDefault(c => c.name == "shoot"));
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
        audioSource.PlayOneShot(clips.FirstOrDefault(c => c.name == "hit"));
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
            if(shipType == EnumShipType.Red)
            {
                ++LevelManager.Player1Powerups;
            }
            else
            {
                ++LevelManager.Player2Powerups;
            }
            audioSource.PlayOneShot(clips.FirstOrDefault(c => c.name == "powerup"));
        }

        if(other.tag == "pickup")
        {
            if(shipType == EnumShipType.Red)
            {
                ++LevelManager.Player1Parts;
                partsText.text = LevelManager.Player1Parts.ToString();
            }
            else
            {
                ++LevelManager.Player2Parts;
                partsText.text = LevelManager.Player2Parts.ToString();
            }
            audioSource.PlayOneShot(clips.FirstOrDefault(c => c.name == "pickup"));

            Destroy(other.gameObject);
        }

        if(other.tag == "checkpoint")
        {
            print("checkpoint");

            var index = CheckpointManager.Checkpoints.IndexOf(other.gameObject);
            if(index == currentCheckpoint)
            {
                print($"checkpoint passed: {currentCheckpoint}");
                if(currentCheckpoint == 0)
                {
                    StartCoroutine(ShowLap());
                }

                ++currentCheckpoint;
                                
                if(currentCheckpoint >= CheckpointManager.Checkpoints.Count)
                {
                    currentCheckpoint = 0;
                    ++lap;

                    if(lap == 3)
                    {
                        StartCoroutine(NextRound());
                    }
                }
            }
        }
    }

    private IEnumerator ShowLap()
    {
        lapText.text = $"Lap {lap}";
        yield return new WaitForSeconds(3f);
        lapText.text = "";
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
                        UpdateAmmo();
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

    private void UpdateAmmo()
    {
        if (shipType == EnumShipType.Red)
        {
            ammobarimage.fillAmount = ammo / LevelManager.Player1MaxAmmo;
        }
        else
        {
            ammobarimage.fillAmount = ammo / LevelManager.Player2MaxAmmo;

        }
    }

    private void UpdateLife(int? life = null)
    {
        if (shipType == EnumShipType.Red)
        {
            LevelManager.Player1Life -= life ?? 0;
            lifeText.text = LevelManager.Player1Life.ToString();
        }
        else
        {
            LevelManager.Player2Life -= life ?? 0;
            lifeText.text = LevelManager.Player2Life.ToString();
        }
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
        UpdateLife(1);

        collider.enabled = false;
        body.detectCollisions = false;
        body.velocity = Vector3.zero;
        shipObject.SetActive(false);

        TextMeshProUGUI countDownText = CountDown.GetComponent<TextMeshProUGUI>();
        if (Explosion != null)
        {
            var prefab = Instantiate(Explosion, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(clips.FirstOrDefault(c => c.name == "explosion"));

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

        shipObject.SetActive(true);
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
        audioSource.PlayOneShot(clips.FirstOrDefault(c => c.name == "count"));

        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        audioSource.PlayOneShot(clips.FirstOrDefault(c => c.name == "count"));

        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        audioSource.PlayOneShot(clips.FirstOrDefault(c => c.name == "count"));

        yield return new WaitForSeconds(1f);
        countDownText.text = "Go!";
        audioSource.PlayOneShot(clips.FirstOrDefault(c => c.name == "count"));
        LevelManager.RoundOver = false;

        yield return new WaitForSeconds(1f);
        countDownText.text = "";
    }

    private IEnumerator NextRound()
    {
        LevelManager.RoundOver = true;
        if (shipType == EnumShipType.Red)
        {
            LevelManager.Player1Win = true;
        }
        else
        {
            LevelManager.Player2Win = true;
        }

        body.velocity = Vector3.zero;
        collider.enabled = false;
        lapText.text = "You Win - Next Course in 3";
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1f);
        lapText.text = "You Win - Next Course in 2";
        yield return new WaitForSeconds(1f);
        lapText.text = "You Win - Next Course in 1";
        yield return new WaitForSeconds(1f);
        LevelManager.LoadNext();
    }

    private IEnumerator Boost()
    {
        if(boosted) yield break;

        boosted = true;
        yield return new WaitForSeconds(.5f);
        boosted = false;
    }
}
