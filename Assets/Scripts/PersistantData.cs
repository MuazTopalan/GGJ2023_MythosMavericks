using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{
    public static PersistantData instance;
    public float volumeLevel;
    public int difficultyLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setVolume(float volume)
    {
        volumeLevel = volume;
    }

    

}
