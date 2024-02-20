using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
{
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] int hp;
    public void TakeDamage(int damage)
    {
        hp -= damage;
        rigidbody.AddForce(Vector3.up * 3f, ForceMode.Impulse);
        if(hp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
