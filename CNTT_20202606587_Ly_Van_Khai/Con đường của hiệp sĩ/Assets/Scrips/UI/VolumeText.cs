using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    public string volumeName;
    public string textIntro;
    private Text chu;

    private void Awake()
    {
        chu = GetComponent<Text>();
    }
    private void Update()
    {
        UpdateVolume();
    }
    private void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
        chu.text = textIntro + volumeValue.ToString();
    }
}
