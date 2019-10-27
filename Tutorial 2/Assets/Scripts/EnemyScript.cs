using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        //keep track of when movement started
        startTime = Time.deltaTime;

        //calculate journey length between start position and end position
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void Update()
    {
        //rotator
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);

        //distance moved = time * speed
        float distCovered = (Time.time - startTime) * speed;

        //fraction of journey completed = current distance divided by the total distance
        float fractionOfJourney = distCovered / journeyLength;

        //set the position of the object as a fraction of the distance between the two markers, and then ping pong the movement
        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fractionOfJourney, 1));
    }
}
