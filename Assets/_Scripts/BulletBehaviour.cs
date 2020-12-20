using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletBehaviour : MonoBehaviour
{
    [Header("Bullet Attributes")]
    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public bool isColliding;
    public bool debug;

    public float speed;
    public Vector3 direction;
    public float range;
    public float radius;
    public Vector3 collisionNormal;
    public float penetration;

    public BulletManager bulletManager;

    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
        radius = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z) * 0.5f;
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBehaviour.isPlaying)
        {
            _Move();
            _CheckBounds();
        }
    }

    private void _Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void _CheckBounds() // Checks bounds of  whole world
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > range)
        {
            bulletManager.ReturnBullet(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
