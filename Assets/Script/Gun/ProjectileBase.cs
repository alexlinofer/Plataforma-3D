using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;

    public int damageAmount = 1;
    public float speed = 50f;

    public List<string> tagsToHit;

    private bool _hasHit = false;

    private void Awake()
    {
        {
            Destroy(gameObject, timeToDestroy);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasHit) return;
        foreach (var t in tagsToHit)
        {
            if (collision.transform.tag == t)
            {
                var damageable = collision.transform.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    damageable.Damage(damageAmount, dir);
                }
                _hasHit = true;
                Destroy(gameObject);
                break;
            }
        }
    }
}
