using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip collectSound;
    public AudioClip ambientNormal;
    public AudioClip ambientTense;
    private AudioSource ambientSource;
    private AudioSource sfxSource;
    private bool switchingToTense = false;

    void Awake()
    {
        Instance = this;
        ambientSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        if (ambientNormal != null)
        {
            ambientSource.clip = ambientNormal;
            ambientSource.loop = true;
            ambientSource.volume = 0.4f;
            ambientSource.Play();
        }
    }

    void Update()
    {
        if (DegradationManager.Instance == null) return;
        float d = DegradationManager.Instance.DegradationLevel;

        if (!switchingToTense)
            ambientSource.volume = Mathf.Lerp(0.4f, 0.15f, d);

        if (d >= 0.9f && ambientTense != null && !switchingToTense)
            StartCoroutine(CrossfadeToTense());
    }

    IEnumerator CrossfadeToTense()
    {
        switchingToTense = true;
        float duration = 1f;
        float elapsed = 0f;
        float startVolume = ambientSource.volume;

        // Fade out normal
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            ambientSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }

        // Switch clip
        ambientSource.clip = ambientTense;
        ambientSource.loop = true;
        ambientSource.Play();
        elapsed = 0f;

        // Fade in tense
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            ambientSource.volume = Mathf.Lerp(0f, 0.4f, elapsed / duration);
            yield return null;
        }

        ambientSource.volume = 0.4f;
    }

    public void PlayCollect()
    {
        if (collectSound != null)
            sfxSource.PlayOneShot(collectSound);
    }
}