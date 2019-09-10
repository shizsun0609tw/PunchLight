using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHealth : MonoBehaviour
{
    public PostProcessVolume post;
    public static PlayerHealth Instance;
    public Image healthBar;
    public float spd;
    Vignette vignette;

    [Range(0, 100)]
    public int bloodAmout;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }
    }

    private void Start()
    {
        post.profile.TryGetSettings(out vignette);
    }
    private void Update()
    {
        vignette.intensity.value = Mathf.Lerp(0, vignette.intensity.value, Time.deltaTime * spd);
    }
    public void Gethitted()
    {
        bloodAmout --;
        healthBar.fillAmount = bloodAmout * 0.01f;
        vignette.intensity.value += 0.2f;
    }
}
