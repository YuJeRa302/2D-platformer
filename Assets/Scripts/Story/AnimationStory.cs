using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class AnimationStory : MonoBehaviour
{
    [Header("[Player]")]
    [SerializeField] private Player _player;
    [Header("[Sound]")]
    [SerializeField] private LevelSounds _levelSounds;
    [Header("[MainCanvas]")]
    [SerializeField] private Canvas _mainCanvas;
    [Header("[StoryFrames]")]
    [SerializeField] private List<StoryFrame> _storyFrames = new List<StoryFrame>();
    [SerializeField] private StoryFrame _endStoryFrame;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _loadConfig;

    private int _delay = 10;
    private IEnumerator _showStoryFrame;
    private IEnumerator _endedStory;

    public void EndStory()
    {
        gameObject.SetActive(true);
        _endedStory = ShowEndStoryFrame();
        StartCoroutine(_endedStory);
    }

    private void Start()
    {
        if (_loadConfig.IsStoryShow == false)
        {
            _showStoryFrame = ShowStoryFrame();
            StartCoroutine(_showStoryFrame);
        }
        else
        {
            StartGameLocation();
        }
    }

    private IEnumerator ShowEndStoryFrame()
    {
        _mainCanvas.gameObject.SetActive(false);
        _endStoryFrame.gameObject.SetActive(true);
        _levelSounds.PlayAudioEndedStory();

        yield return new WaitForSeconds(_delay);

        MainMenu.Load();
    }

    private IEnumerator ShowStoryFrame()
    {
        _levelSounds.ChangeSound(false, false);

        for (int index = 0; index < _storyFrames.Count; index++)
        {
            _storyFrames[index].gameObject.SetActive(true);
            yield return new WaitForSeconds(_delay);
            _storyFrames[index].gameObject.SetActive(false);
        }

        StartGameLocation();
    }

    private void StartGameLocation()
    {
        gameObject.SetActive(false);
        _player.gameObject.SetActive(true);
        _mainCanvas.gameObject.SetActive(true);
        _levelSounds.ChangeSound(true, false);
    }
}