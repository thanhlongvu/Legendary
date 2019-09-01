using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    DIE_ENEMY,
    DIE_ENEMY_2,
    ATTACK_CHARACTER,
    MOVE_CHARACTER
}
public class SoundManager : Singleton<SoundManager>
{
    [System.Serializable]
    public struct SoundStruct
    {
        public Sound sound;
        public AudioClip audio;
    }

    public SoundStruct[] sounds;

    public void PlaySound(Sound sound)
    {
        var obj = PoolManager.Instance.PopPool(PoolName.SOUND.ToString());
        var audio = obj.GetComponent<AudioSource>();
        audio.clip = GetSound(sound);
        audio.Play();

        ////set volume
        //audio.volume = Random.Range(0.4f, 0.8f);

        StartCoroutine(PushToPool(obj, audio.clip.length));
    }

    private IEnumerator PushToPool(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.Instance.PushPool(obj, PoolName.SOUND.ToString());
    }

    private AudioClip GetSound(Sound sound)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].sound == sound)
                return sounds[i].audio;
        }

        return null;
    }
}
