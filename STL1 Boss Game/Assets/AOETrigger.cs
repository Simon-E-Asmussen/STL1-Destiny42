using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class AOETrigger : NetworkBehaviour
{
    public bool canDamage = false;
    public bool markerVisible = false;
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Dealing damage");

        if (!markerVisible)
        {
            if (other.transform.TryGetComponent(out PlayerHealth health) && canDamage)
            {
                health.TakeDamage(34);
                canDamage = false;
                gameObject.SetActive(false);
            }
        }
    }
}
