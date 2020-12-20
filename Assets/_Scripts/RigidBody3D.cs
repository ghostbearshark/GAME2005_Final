using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyType
{
   STATIC,
    DYNAMIC
}


[System.Serializable]
public class RigidBody3D : MonoBehaviour
{
    [Header("Gravity Simulation")]
    public float gravityScale;
    public float mass;
    public BodyType bodyType;
    public float timer;
    public bool isFalling;

    [Header("Attributes")]
    public Vector3 velocity;
    public Vector3 acceleration;
    private float gravity;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        gravity = -0.001f;
        velocity = Vector3.zero;
        acceleration = new Vector3(0.0f, gravity * gravityScale, 0.0f);
        if (bodyType == BodyType.DYNAMIC)
        {
            isFalling = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBehaviour.isPlaying)
        {
            if (bodyType == BodyType.DYNAMIC)
            {
                if (isFalling)
                {
                    timer += Time.deltaTime;

                    if (gravityScale < 0)
                    {
                        gravityScale = 0;
                    }

                    if (gravityScale > 0)
                    { 
                        velocity += acceleration * 0.5f * timer * timer;
                        transform.position += velocity;
                    }
                }

            }
        }
    }

    public void Stop()
    {
        timer = 0;
        isFalling = false;
    }

    public void AddForce(string Direction)
    {
        if (Direction == "Left")
        {
            transform.position += Vector3.left * 0.1f;
        }

        else if(Direction == "Right")
        {
            transform.position += Vector3.right * 0.1f;
        }

        else if (Direction == "Forward")
        {
            transform.position += Vector3.forward * 0.1f;
        }

        else if (Direction == "Back")
        {
            transform.position += Vector3.back * 0.1f;
        }


    }
}
