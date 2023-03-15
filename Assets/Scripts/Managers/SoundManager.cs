using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] m_audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> m_audioclip = new Dictionary<string, AudioClip>();


    public void init()
    {
        GameObject go = GameObject.Find("@SoundManager");
        if(go == null)
        {
            go = new GameObject { name = "@SoundManager" };
            UnityEngine.Object.DontDestroyOnLoad(go);
        }
        string[] names = Enum.GetNames(typeof(Define.Sound));
        for (int i = 0; i < names.Length - 1; i++)
        {
            GameObject tempgo = new GameObject { name = names[i] };
            m_audioSources[i] = tempgo.AddComponent<AudioSource>();
            tempgo.transform.parent = go.transform;
        }
        m_audioSources[(int)Define.Sound.BGM].loop = true;


    }

    public void BGMPause()
    {
        m_audioSources[(int)Define.Sound.BGM].Pause();
    }

    public void BGMPlay()
    {
        m_audioSources[(int)Define.Sound.BGM].Play();
    }


    public void Play(AudioSource objectAudioSource, AudioClip temp, Define.Sound type = Define.Sound.Effect, float SpaticalBlend = 1f, float pitch = 1.0f)
    {
        if (temp == null)
            return;
        if (type == Define.Sound.BGM)
        {

            AudioSource audiosource = m_audioSources[(int)Define.Sound.BGM];
            if (audiosource.isPlaying)
                audiosource.Stop();
            audiosource.clip = temp;
            audiosource.pitch = pitch;
            audiosource.Play();
        }
        else
        {
            AudioSource audiosource = objectAudioSource;
            audiosource.spatialBlend = SpaticalBlend;
            audiosource.pitch = pitch;
            audiosource.PlayOneShot(temp);
        }
    }

    public void Play(AudioSource objectAudioSource, string path, Define.Sound type = Define.Sound.Effect, float SpaticalBlend = 1f, float pitch = 1.0f)
    {
        AudioClip cl = GetOrAddAudioClip(path, type);
        Play(objectAudioSource,cl, type, SpaticalBlend, pitch);
    }


    public void Play(AudioClip temp, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {

        if (temp == null)
            return;
        if (type == Define.Sound.BGM)
        {
          
            AudioSource audiosource = m_audioSources[(int)Define.Sound.BGM];
            if (audiosource.isPlaying)
                audiosource.Stop();
            audiosource.clip = temp;
            audiosource.pitch = pitch;
            audiosource.Play();
        }
        else
        {
            AudioSource audiosource = m_audioSources[(int)Define.Sound.Effect];
            audiosource.pitch = pitch;
            audiosource.PlayOneShot(temp);
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch  = 1.0f)
    {
        AudioClip cl =  GetOrAddAudioClip(path, type);
        Play(cl, type,pitch);
    }
    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";
        AudioClip cl = null;

        if (type == Define.Sound.BGM)
        {
            cl = Manager.Resource_Instance.Load<AudioClip>(path);
            if (cl == null)
                Debug.Log("Audioclip is null");
        }
        else
        {
            if (m_audioclip.TryGetValue(path, out cl) == false)
            {
                cl = Manager.Resource_Instance.Load<AudioClip>(path);
                m_audioclip.Add(path, cl);
            }
            if (cl == null)
                Debug.Log("Audioclip is null");
        }
        return cl;
    }

    public void Clear()
    {
        foreach(AudioSource au in m_audioSources)
        {
            au.clip = null;
            au.Stop();
        }
        m_audioclip.Clear();
    }
}
