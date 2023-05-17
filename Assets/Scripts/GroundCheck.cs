using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public GameObject _DroopyParent;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _DroopyParent.GetComponent<DroopyMovement>().SetGround(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _DroopyParent.GetComponent<DroopyMovement>().SetGround(false);
        }
    }
}
