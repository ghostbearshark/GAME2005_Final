using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;
    static public bool isPlaying;


    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;


    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;

    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBehaviour.isPlaying)
        {
            _Fire();
            _Move();

        }
    }

    private void _Move()
    {
        if (isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                // move right
                body.velocity = playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                // move left
                body.velocity = -playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                // move forward
                body.velocity = playerCam.transform.forward * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") < 0.0f) 
            {
                // move Back
                body.velocity = -playerCam.transform.forward * speed * Time.deltaTime;
            }

            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.9f);
            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); // remove y
            

            if (Input.GetAxisRaw("Jump") > 0.0f )
            {
                body.velocity = transform.up * speed * 0.1f * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Jump") > 0.0f && Input.GetAxisRaw("Vertical") > 0.0f)
            {
                body.velocity = playerCam.transform.up * speed * 0.1f * Time.deltaTime;
            }

            transform.position += body.velocity;
        }
    }


    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {

                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
            }
        }
    }

    void OnCollisionEnter(Collision c)
    {
        // force is how forcefully we will push the player away from the enemy.
        float force = 10;

        // If the object we hit is the enemy
        if (c.gameObject.tag == "player")
        {
            // Calculate Angle Between the collision point and the player
            Vector3 dir = c.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * force);
            Debug.Log(gameObject.name + " has collided with " + c.gameObject.name);
        }
    }


    void FixedUpdate()
    {
        GroundCheck();

    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

}
