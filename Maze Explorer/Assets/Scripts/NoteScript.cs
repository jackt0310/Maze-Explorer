using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public bool note = true;
    public bool chicken = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(note)
        {
            transform.Rotate(0, 50 * Time.deltaTime, 0);
        } else if(chicken)
        {
            transform.Rotate(50 * Time.deltaTime, 0, 0);
        } else
        {
            transform.Rotate(0, 0, 50 * Time.deltaTime);
        }
        
    }
}
