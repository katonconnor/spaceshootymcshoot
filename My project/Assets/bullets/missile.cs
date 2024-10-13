using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class missile : bullet
{
    public float speed = 1.5f;
    public Vector2 direction;

    // Update is called once per frame
    void Update()
    {
        Movement(); 
    }
    public override void Movement()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
