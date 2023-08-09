using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instanse;

	void Awake ()
    {
        if (instanse == null)
            instanse = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
	}

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, so => so.name == name);

        if (s == null)
        {
            Debug.LogError("Sound: '" + name + "' not found!");
            return;
        }

        s.source.Play();
    }
}
