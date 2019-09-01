using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [Range(0f, 5f)]
    public float moveSpeed;
    [Range(10, 100)]
    public int damage;
    [SerializeField]
    [Range(1f, 4f)]
    private float timeDestroy;

    private float timelife;

    private bool isColl = false;
    void Start()
    {
        timelife = timeDestroy;
    }

    void Update()
    {
        Move();

        timelife -= Time.deltaTime;
        if(timelife <= 0)
            PoolManager.Instance.PushPool(gameObject, PoolName.SPEAR.ToString());
    }

    private void OnEnable()
    {
        timelife = timeDestroy;
        isColl = false;
    }

    private void Move()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag.Equals("Enemy") && !isColl)
        {
            isColl = true;
            other.GetComponent<EnemyBase>().TakeDamage(damage);

            //TODO: set effect attacked
            //Effect for ...

            //To pool
            PoolManager.Instance.PushPool(gameObject, PoolName.SPEAR.ToString());
        }
    }
}
