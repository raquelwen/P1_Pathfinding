using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Move : MonoBehaviour
{
    public float speed = .2f;
    public float strength = 9f;
    private float randomOffset;
    // Start is called before the first frame update
    void Start()
    {
        randomOffset = Random.Range(7f, 9f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Sin(Time.time * speed + randomOffset)  * strength; //aprovechamos la funcino sin para que a lo largo del tiempo nos de la posicion del cubo. 
        transform.position = pos;   //actualizamos la posicion. 
    }
}
