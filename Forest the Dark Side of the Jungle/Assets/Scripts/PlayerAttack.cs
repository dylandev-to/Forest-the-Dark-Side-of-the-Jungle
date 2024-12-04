using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public float attackCooldown = 0.5f;
    public int attackDamage = 25;
    public Transform attackPoint;
    public float attackRadius = 0.5f;

    private float nextAttackTime = 0f;

    void Update()
    {
        HandleAttack();
    }

    void HandleAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButton("Fire1"))
            {
                PerformAttack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void PerformAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRadius);

        foreach (Collider collider in hitColliders)
        {
            // Add any attack logic here (e.g., deal damage)
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
