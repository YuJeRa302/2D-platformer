using UnityEngine;

public class LevelSounds : MonoBehaviour
{
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _audioSource;
    [Header("[Audio Clips]")]
    [SerializeField] private AudioClip _firstLevelClip;
    [SerializeField] private AudioClip _secondLevelClip;
    [SerializeField] private AudioClip _storyClip;
    [SerializeField] private AudioClip _endedstoryClip;

    public void ChangeSound(bool enableFirstLevelSound, bool enableSecondLevelSound)
    {
        if (enableFirstLevelSound == true)
        {
            PlayAudio(_firstLevelClip);
        }
        else if (enableSecondLevelSound == true)
        {
            PlayAudio(_secondLevelClip);
        }
        else
        {
            PlayAudio(_storyClip);
        }
    }

    public void SetValueVolume(float value)
    {
        _audioSource.volume = value;
    }

    public void PlayAudioEndedStory()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_endedstoryClip);
    }

    private void PlayAudio(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}