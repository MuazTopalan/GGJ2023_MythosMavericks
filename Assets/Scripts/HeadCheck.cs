using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{
    public GameObject _DroopyParent;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _DroopyParent.GetComponent<DroopyMovement>().HeadHit(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _DroopyParent.GetComponent<DroopyMovement>().HeadHit(false);
        }
    }
}
