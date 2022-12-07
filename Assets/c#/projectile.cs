using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Vector3 directions;
    public int speed;
    public System.Action destroyed;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += directions * speed * Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyed != null )
        {
        destroyed.Invoke();
        }
        
        Destroy(gameObject);

        
    }
}
