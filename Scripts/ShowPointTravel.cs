using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPointTravel : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
