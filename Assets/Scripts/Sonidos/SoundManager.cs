using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum CLIPS
    {
        DEATH,
        ENEMY_HIT,
        JUMP,
        PLAYER_HIT,
        RESPAWN,
        SHOOT_2,
        SHOOT_3,
        SHOOT_6,
        SHOOT_7,
        TAKE_ELEVATOR
    }

    public List<AudioClip> clips = new List<AudioClip>();


    public static SoundManager instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        audioSource = GetComponent<AudioSource>();

    }

    public void playSound(int soundID)
    {
        audioSource.PlayOneShot(clips[soundID]);
    } 
}
