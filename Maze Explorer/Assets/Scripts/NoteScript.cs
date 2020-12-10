using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public bool note = true;
    public bool chicken = false;
    public float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(note)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        } else if(chicken)
        {
            transform.Rotate(speed * Time.deltaTime, 0, 0);
        } else
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
        
    }
}
