using UnityEngine;
using System.Collections;
using static Unity.VisualScripting.Member;
using UnityEngine.InputSystem.Utilities;
using Unity.VisualScripting;

public class AudioMaster : MonoBehaviour
{
    public static AudioMaster Instance;
    public AudioSource AMBSource;
    public AudioSource MetroStop;
    public AudioSource SFX;
    public AudioSource pickupSounds;
    public AudioClip peopleSwitchEmotion;
    public AudioClip playerPickupEmotion;
    public AudioClip plantPickupEmotion;
    public AudioClip PlantPickUp;
    public AudioClip PlantPickDrop;

    public float sfxPitchVariation = 0.1f;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {

    }
    public void StopMetro()
    {
        int randomSample = Random.Range(0, (int)((MetroStop.clip.samples * 0.1f)));
        MetroStop.timeSamples = randomSample;
        MetroStop.volume = 0.5f;
        StartCoroutine(FadeOut(AMBSource, 3f));
        MetroStop.Play();
        StartCoroutine(FadeOut(MetroStop,6.5f));

    }
    public void ResumeMetro()
    {
        StartCoroutine(FadeOut(MetroStop, 1f));
        StartCoroutine(FadeIn(AMBSource, 6f,0.3f));
    }


    public void PoeopleFlipEmotion()
    {
        SFX.pitch = 1;
        SFX.PlayOneShot(peopleSwitchEmotion);
    }

    void RandimizePitch(AudioSource source)
    {
        source.pitch = 1;
        float randompitch = Random.Range(-sfxPitchVariation, sfxPitchVariation);
        source.pitch += randompitch;
    }

    public void PlayerPickpuEmotion()
    {
        SFX.pitch = 1;
        SFX.PlayOneShot(playerPickupEmotion);
    }

    public void PlantPickupEmotion()
    {
        SFX.pitch = 1;
        SFX.PlayOneShot(plantPickupEmotion);
    }

    public void PlayerPlantPickup()
    {
        RandimizePitch(pickupSounds);
        pickupSounds.PlayOneShot(PlantPickUp);
    }
    public void PlayerPlantPickDrop()
    {
        RandimizePitch(pickupSounds);
        pickupSounds.PlayOneShot(PlantPickDrop);
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopMetro();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ResumeMetro();
        }
        */

    }

    public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime, float targetvolume)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < targetvolume)
        {
            audioSource.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}
