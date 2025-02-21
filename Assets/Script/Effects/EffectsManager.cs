using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using JogoPlataforma3D.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;
    [SerializeField] private Vignette _vignette;

    public float duration = .2f;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ColorParameter c = new ColorParameter();

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }
    }

}
