using System;
using UnityEngine;
using UnityEngine.UI;
public class LoadingText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro;
    private Text _txt;

    private void Awake()
    {
        _txt = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float volume = PlayerPrefs.GetFloat(volumeName) * 100;
        _txt.text = textIntro + volume;
    }
}
