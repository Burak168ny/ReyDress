using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.Play("Begining");
        AudioManager.Instance.Play("Background");
    }
}
