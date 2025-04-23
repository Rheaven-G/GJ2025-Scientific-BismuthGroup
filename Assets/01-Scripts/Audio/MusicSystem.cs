using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    public MusicLayer[] musicLayers = null;
    float Fadetime = 0.5f;
    int currentPhase = 0;
    public float volume = 0.76f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Making a new loop to ensure the sample start time is accurate
        double timetoPlay = AudioSettings.dspTime + 0.2;
        for (int i = 0; i <= musicLayers.Length - 1; i++)
        {
            int currentIntensity = musicLayers[i].currentIntensity;
            musicLayers[i].audioSources[currentIntensity].volume= volume;
            if (i == 2) // Final Music Hack;
            {
                musicLayers[i].audioSources[currentIntensity].volume = 0.4f;
            }
            musicLayers[i].audioSources[currentIntensity].PlayScheduled(timetoPlay);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (currentPhase != GameManager.Instance.GetCurrentPhase())
        {
            RaiseIntensity(0);
            currentPhase = GameManager.Instance.GetCurrentPhase();
        }
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(RaiseIntensity(0))
            {
                string msg = "Drum Raised: " + GetIntencityLevel(0);
                Debug.Log(msg);
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (LowerIntensity(0)) 
            {
                string msg = "Drum Lowered: " + GetIntencityLevel(0);
                Debug.Log(msg);
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (RaiseIntensity(1))
            {
                string msg = "Melody Raised: " + GetIntencityLevel(0);
                Debug.Log(msg);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (LowerIntensity(1)) 
            {
                string msg = "Melody Lowered: " + GetIntencityLevel(0);
                Debug.Log(msg);
            }
        }
        */
    }
    int GetIntencityLevel(int layer)
    {
        return musicLayers[layer].currentIntensity;;
    }

    bool RaiseIntensity(int layer)
    {
        if (musicLayers[layer].audioSources.Length - 1 >= musicLayers[layer].currentIntensity + 1)
        {
            int currentIntensity = GetIntencityLevel(layer);
            AudioSource currentPlaying = musicLayers[layer].audioSources[currentIntensity];
            AudioSource toPlay = musicLayers[layer].audioSources[currentIntensity + 1];
            toPlay.volume = 0;
            toPlay.timeSamples = currentPlaying.timeSamples;
            double timetoPlay = AudioSettings.dspTime;
            
            toPlay.PlayScheduled(timetoPlay);

            StartCoroutine(AudioMaster.FadeOut(currentPlaying, Fadetime));
            StartCoroutine(AudioMaster.FadeIn(toPlay, Fadetime, volume));
            musicLayers[layer].currentIntensity++;
            return true; 
        }
        return false;

    }
    bool LowerIntensity(int layer)
    {
        if (musicLayers[layer].currentIntensity - 1 >= 0)
        {
            int currentIntensity = GetIntencityLevel(layer);
            AudioSource currentPlaying = musicLayers[layer].audioSources[currentIntensity];
            AudioSource toPlay = musicLayers[layer].audioSources[currentIntensity - 1];
            toPlay.volume = 0;
            toPlay.timeSamples = currentPlaying.timeSamples;
            double timetoPlay = AudioSettings.dspTime;
            toPlay.PlayScheduled(timetoPlay);

            StartCoroutine(AudioMaster.FadeOut(currentPlaying, Fadetime));
            StartCoroutine(AudioMaster.FadeIn(toPlay, Fadetime, volume));

            musicLayers[layer].currentIntensity--;
            return true;
        }
        return false;

    }

}
