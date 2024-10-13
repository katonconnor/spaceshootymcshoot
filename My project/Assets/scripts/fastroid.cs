using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastroid : MonoBehaviour

{
    //random direction
    public Vector2 RandomVector2(float angle, float angleMin)
    {
        float random = Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }
    public float speed = 1f;
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
        transform.Translate(RandomVector2(3.1415f, 3.1415f) * speed * Time.deltaTime);
    }
}