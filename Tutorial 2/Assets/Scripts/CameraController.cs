using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target;

    //private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = target.transform.position + offset;
        this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, this.transform.position.z);
        //escape function
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
