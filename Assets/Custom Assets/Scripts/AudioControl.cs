using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioControl : MonoBehaviour {

    public AudioMixer mixer;

    public void Set_BGM_Volume (float volume) {
        mixer.SetFloat("bgm_volume", volume);
    }

    public void Set_BGM_Cutoff (float freq) {
        mixer.SetFloat("bgm_cutoff_freq", freq);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
