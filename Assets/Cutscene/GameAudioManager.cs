using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameAudioManager : MonoBehaviour {

    public Sound[] sounds;

	// Use this for initialization
	void Awake ()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
		
	}

    void PlayIntro()
    {
        Play("TitleMusic");

        Debug.Log("good");
    }

    public void Start()
    {
        Play("Train");

        Invoke("PlayIntro", 5);
        
    }


    // Update is called once per frame
    public void Play (string name)
    {
        Sound s = Array.Find(sounds,sound => sound.name == name);
        if (s == null)
        {
            return;
        }
            s.source.Play();
       

    }
}
