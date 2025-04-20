using UnityEngine;
using System.Collections;
public class AudioMaster : MonoBehaviour
{
    public static AudioMaster Instance;
    public AudioSource AMBSource;
    public AudioSource MetroStop;
    public AudioSource SFX;
    public AudioClip peopleSwitchEmotion;
    public AudioClip playerPickupEmotion;
    public AudioClip plantPickupEmotion;
    public AudioClip PlantPickUp;
    public AudioClip PlantPickDrop;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {

    }
    void StopMetro()
    {
        int randomSample = Random.Range(0, (int)((MetroStop.clip.samples * 0.05f)));
        MetroStop.timeSamples = randomSample;
        StartCoroutine(FadeOut(AMBSource, 1.5f));
        MetroStop.Play();
        StartCoroutine(FadeOut(MetroStop, 4f));

    }
    void ResumeMetro()
    {
        int randomSample = Random.Range(0, (int)((MetroStop.clip.samples * 0.05f))); 
        MetroStop.timeSamples = randomSample;
        MetroStop.Play();
        StartCoroutine(FadeOut(MetroStop, 0.5f));
        StartCoroutine(FadeIn(AMBSource, 3f,0.3f));
    }


    public void PoeopleFlipEmotion()
    {
        SFX.PlayOneShot(peopleSwitchEmotion);
}

    public void PlayerPickpuEmotion()
    {
        SFX.PlayOneShot(playerPickupEmotion);
    }

    public void PlantPickupEmotion()
    {
        SFX.PlayOneShot(plantPickupEmotion);
    }

    public void PlayerPlantPickup()
    {
        SFX.PlayOneShot(PlantPickUp);
    }
    public void PlayerPlantPickDrop()
    {
        SFX.PlayOneShot(PlantPickDrop);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopMetro();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ResumeMetro();
        }
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
