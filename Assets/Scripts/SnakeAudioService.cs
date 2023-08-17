using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SnakeAudioService : MonoBehaviour
{
    private AudioSource _audioSource;
    
    [SerializeField] private Snake _snake;

    [SerializeField, BoxGroup("Sound clips")] private AudioClip _turnClip;
    [SerializeField, BoxGroup("Sound clips")] private AudioClip _growClip;
    [SerializeField, BoxGroup("Sound clips")] private AudioClip _dieClip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _snake.onTurn += PlayTurnSound;
        _snake.onGrow += PlayGrowSound;
        _snake.onDie += PlayDieSound;
    }

    private void PlayTurnSound()
    {
        _audioSource.PlayOneShot(_turnClip);
    }

    private void PlayGrowSound()
    {
        _audioSource.PlayOneShot(_growClip);
    }

    private void PlayDieSound()
    {
        _audioSource.PlayOneShot(_dieClip);
    }
}
