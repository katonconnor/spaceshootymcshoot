using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float speed = 0.4f;
    public int health = 1;
    private GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        transform.Translate(new Vector3(Mathf.Sin(Time.time * 1.5f), -1, 0) * speed * Time.deltaTime);
    }
}
