using UnityEngine;

public class MusicLayer : MonoBehaviour
{
    [SerializeField]
    private string Name;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip[] musicIntensities = null;
    public AudioSource[] audioSources = null;
    public int currentIntensity = 0;
    void Awake()
    {
        audioSources = new AudioSource[musicIntensities.Length];
        for (int i = 0; i <= musicIntensities.Length - 1; i++)
        {
            AudioSource audioSoruce = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            audioSoruce.loop= true;
            audioSoruce.playOnAwake = false;
            audioSources[i] = audioSoruce;
            audioSources[i].clip = musicIntensities[i]; // Sets everything to 0
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
