using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip m_runUpSound;
    public AudioClip m_loopSound;
    public AudioClip m_runDownSound;

    // Use this for initialization
    void Start () {
        UIManager.Instance.m_timer.OnTimerFinished += OnTimerFinished;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTimerFinished() {
        StartCoroutine(PlayShutdown());
    }

    public IEnumerator PlayStartup() {
        AudioSource source = this.GetComponent<AudioSource>();
        source.clip = m_runUpSound;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        source.clip = m_loopSound;
        source.Play();
        source.loop = true;
    }

    public IEnumerator PlayShutdown() {
        AudioSource source = this.GetComponent<AudioSource>();
        source.loop = false;
        source.clip = m_runDownSound;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        source.Stop();
    }

}
