using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroopyHealth : MonoBehaviour
{
    public Slider healthBar;
    public GameObject DroopyGameObject;
    public SceneLoadManager sceneLoader;

    public void TakeDamage(float receivedDamage)
    {
        if (healthBar.value - receivedDamage > 0)
        {
            healthBar.value -= receivedDamage;
            DroopyGameObject.transform.localScale = new Vector3(0.5f + healthBar.value / 100, 0.5f + healthBar.value / 100, 0.5f + healthBar.value / 100);
        }
        else
        {
            sceneLoader.OpenGameOverMenu();
        }
    }

    public void RestoreHealth(float restoreAmount)
    {
        int reverser = 1;
        if (DroopyGameObject.transform.localScale.x < 0)
        {
            reverser = -1;
        }
        if (healthBar.value + restoreAmount < 100)
        {
            healthBar.value += restoreAmount;
            DroopyGameObject.transform.localScale = new Vector3((0.5f + healthBar.value / 100) * reverser, 0.5f + healthBar.value / 100, 0.5f + healthBar.value / 100);
        }
        else
        {
            healthBar.value = 100;
            DroopyGameObject.transform.localScale = new Vector3((0.5f + healthBar.value / 100) * reverser, 0.5f + healthBar.value / 100, 0.5f + healthBar.value / 100);
        }
    }

}