using Player;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip engineSoundClip;

    private CarController _carController;
    private AudioSource _audioSource;
    private float _maxRpm;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = engineSoundClip;
        _audioSource.pitch = 0.43f;
    }

    private void Start()
    {
        _carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        _maxRpm = _carController.GetMaxRpm();
    }

    private void Update()
    {
        var currentRpm = _carController.CurrentRpm;
        _audioSource.pitch = ((currentRpm/_maxRpm)*1.6f)+0.4f;
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
}
