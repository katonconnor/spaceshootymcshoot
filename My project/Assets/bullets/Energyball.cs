using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energyball : bullet
{
    
    public float speed = 1.5f;
   
    void Update()
    {
        Movement();
    }






    public override void  Movement()
    {
        transform.Translate(new Vector3(Mathf.Sin(Time.time * speed), 1, 0) * speed * Time.deltaTime);
    }


}