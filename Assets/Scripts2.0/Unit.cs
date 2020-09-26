using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public virtual void RecieveDamage(float opponentPosition = 1)
    {
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
