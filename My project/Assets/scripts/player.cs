using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    //random direction
    public Vector2 RandomVector2(float angle, float angleMin)
    {
        float random = Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }
    //Player Data
    public float speed = 5.0f;
    public float firerate = 0.25f;
    public int lives = 3;
    public int shield = 2;
    public float canfire = 0.0f;
    public float shieldDuration = 5.0f;

    public GameObject BulletPref;
    public List<bullet> bullets;

    //to use audio
    public audioManager audioManager;
    public AudioSource actualAudio;

    // list for ship health
    public enum ShipState
    {
        FullHealth,
        SlightlyDamaged,
        Damaged,
        HeavilyDamaged,
        Destroyed
    }

    public ShipState shipState;
    public List<Sprite> ShipSprites = new List<Sprite>();

    void ChangeShipState()
    {
        var currentState = shipState;
        Debug.Log(currentState);

        //search by name
        var newSprite = ShipSprites.Find(x => x.name == currentState.ToString());

        //search by id
        //var newSprite = ShipSprites[(int)currentState]
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;

        switch (currentState)
        {
            case ShipState.FullHealth:
                shipState = ShipState.SlightlyDamaged; break;
            case ShipState.SlightlyDamaged:
                shipState = ShipState.Damaged; break;
            case ShipState.Damaged:
                shipState = ShipState.HeavilyDamaged; break;
            case ShipState.HeavilyDamaged:
                shipState = ShipState.Destroyed; break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        shields.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckBoundaries();
        ChangeWeapon();
        UseShields();
        Fire();

    }

    // teclas
    
    public GameObject shields;

    public void UseShields()
    {
        if (Input.GetKeyDown(KeyCode.Z) && shield > 0)
        {
            shield--;
            shields.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (shields.activeSelf)
        {
            shieldDuration -= Time.deltaTime;
            if (shieldDuration < 0)
            {
                shields.SetActive(false);
                shieldDuration = 5.0f;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
    public void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

    }

    public void CheckBoundaries()
    {
        //Limites de la pantalla , provoca que el jugador pase al otro lado de la pantalla al llegar a un borde
        var cam = Camera.main;
        float xMax = cam.orthographicSize * cam.aspect;
        float yMax = cam.orthographicSize;
        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(-xMax, transform.position.y, 0);
        }

        else if (transform.position.x < -xMax)
        {

            transform.position = new Vector3(xMax, transform.position.y, 0);
        }
        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, -yMax, 0);
        }
        else if (transform.position.y < -yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, 0);
        }

    }
    //player Fire
    public void Fire()

    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canfire)
       {
            /*            Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                        canfire = Time.time + firerate;
                        //play sound
                        actualAudio.Play();
            */
            switch (BulletPref.name) {
                case "Bullet":
                    Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0),Quaternion.identity);
                    canfire = Time.time + firerate;
                    actualAudio.Play();
                    break;
                case "rocket":
                    var bullet1 = Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f,0), Quaternion.identity);
                    bullet1.GetComponent<missile>().direction = Vector2.up;

                    var bullet2 = Instantiate(BulletPref, transform.position + new Vector3(0.5f, 0.8f, 0), Quaternion.identity);
                    bullet2.GetComponent<missile>().direction = new Vector2(0.5f,1);

                    var bullet3 = Instantiate(BulletPref, transform.position + new Vector3(-0.5f, 0.8f, 0), Quaternion.identity);
                    bullet3.GetComponent<missile>().direction = new Vector2(-0.5f, 1);

                    canfire = Time.time + firerate;
                    actualAudio.Play();
                    break;
                case "plasma ball":
                    var bullet4 = Instantiate(BulletPref, transform.position + new Vector3(Mathf.Sin(Time.time * speed), 0.8f, 0), Quaternion.identity);
                    bullet4.GetComponent<Energyball>();

                    var bullet5 = Instantiate(BulletPref, transform.position + new Vector3(Mathf.Sin(Time.time * speed), 0.8f, 0), Quaternion.identity);
                    bullet5.GetComponent<Energyball>();
                    canfire = Time.time + firerate;
                    actualAudio.Play();
                    break;
                case "laser":
                    var bullet6 = Instantiate(BulletPref, transform.position + new Vector3(-0.5f, 0.8f, 0), Quaternion.identity);
                    bullet6.GetComponent<laser>().direction = RandomVector2(1.5f, 0.8f);

                    var bullet7 = Instantiate(BulletPref, transform.position + new Vector3(0.5f, 0.8f, 0), Quaternion.identity);
                    bullet7.GetComponent<laser>().direction = RandomVector2(1.5f, 0.8f);

                    var bullet8 = Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                    bullet8.GetComponent<laser>().direction = RandomVector2(1.5f, 0.8f);

                    var bullet9 = Instantiate(BulletPref, transform.position + new Vector3(-0.3f, 0.8f, 0), Quaternion.identity);
                    bullet9.GetComponent<laser>().direction = RandomVector2(1.5f, 0.8f);

                    var bullet10 = Instantiate(BulletPref, transform.position + new Vector3(0.3f, 0.8f, 0), Quaternion.identity);
                    bullet10.GetComponent<laser>().direction = RandomVector2(1.5f, 0.8f);


                    canfire = Time.time + firerate;
                    actualAudio.Play();
                    break;




            }
                
                



       }
      
    }
    //cambio de armas 

    public void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BulletPref = bullets[0].gameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BulletPref = bullets[1].gameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BulletPref = bullets[2].gameObject;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            BulletPref = bullets[3].gameObject;
        }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                Destroy(collision.gameObject);
                ChangeShipState();
                if (lives > 1)

                {
                    lives--;
                    Debug.Log("lives: " + lives);
                    
                }
                else
                {
                    lives--;
                    Destroy(this.gameObject);
                }

              
                

            }
            if (collision.gameObject.CompareTag("upgrade"))
            {
                Destroy(collision.gameObject);
                shield = shield + 1;
            }

        }
    }
}
