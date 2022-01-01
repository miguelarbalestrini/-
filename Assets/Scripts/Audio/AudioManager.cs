using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Gets whether or not the audio manager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;

        audioClips.Add(AudioClipName.AtkSwing,
            Resources.Load<AudioClip>("AtkSwing"));
        audioClips.Add(AudioClipName.GameLost,
            Resources.Load<AudioClip>("GameLost"));
        audioClips.Add(AudioClipName.Walk,
            Resources.Load<AudioClip>("Walk"));
        audioClips.Add(AudioClipName.BackgroundSurvival,
         Resources.Load<AudioClip>("BackgroundSurvival"));
        audioClips.Add(AudioClipName.BackgroundBoss1,
         Resources.Load<AudioClip>("BackgroundBoss1"));
        audioClips.Add(AudioClipName.BackgroundBoss2,
         Resources.Load<AudioClip>("BackgroundBoss2"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    public static void Stop()
    {
        audioSource.Stop();
    }

    public static void PlayLoop(AudioClipName name)
    {
        audioSource.clip = audioClips[name];
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.Play();
    }
}
