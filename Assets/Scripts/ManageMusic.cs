using UnityEngine;
using System.Collections;

public class ManageMusic : MonoBehaviour
{

    [SerializeField] private AudioSource musicFXObject;
    [SerializeField] private AudioSource currentFXObject;


    private AudioClip currentMusic;

    private Coroutine musicCoroutine;
    private Coroutine fadeCoroutine;

    [SerializeField] private AudioClip level1;
    [SerializeField] private AudioClip level2;
    [SerializeField] private AudioClip level3;
    [SerializeField] private AudioClip level4;

    public int currentEnemySpawnSpeed = 0;
    public int targetEnemySpawnSpeed = 0;


    void Start()
    {

        musicCoroutine = StartCoroutine(PlayMusic(transform, 1f));

    }

    public IEnumerator PlayMusic(Transform spawnTransform, float volume)
    {

        while (true)
        {

            if (currentFXObject == null)
            {

                if (currentEnemySpawnSpeed == 0)
                    currentMusic = level1;
                if (currentEnemySpawnSpeed == 1)
                    currentMusic = level2;
                if (currentEnemySpawnSpeed == 2)
                    currentMusic = level3;
                if (currentEnemySpawnSpeed == 3)
                    currentMusic = level4;

                currentFXObject = Instantiate(musicFXObject, spawnTransform.position, Quaternion.identity);

                //fadeCoroutine = StartCoroutine(FadeMusicIn(currentFXObject));

                currentFXObject.clip = currentMusic;

                currentFXObject.volume = volume;

                currentFXObject.Play();

                float clipLength = currentFXObject.clip.length;

                yield return new WaitForSeconds(10f);

                yield return new WaitUntil(() => currentFXObject == null || currentEnemySpawnSpeed != targetEnemySpawnSpeed);


                Destroy(currentFXObject.gameObject);
                currentEnemySpawnSpeed++;

                currentFXObject = null;

            }

            yield return null;

        }

    }


    // Coroutine to fade in audio
    public IEnumerator FadeMusicIn(AudioSource audioToFade)
    {

        audioToFade.Play();
        float startVolume = 0f;
        float timer = 0f;

        while (audioToFade.volume < 1)
        {
            audioToFade.volume = Mathf.Lerp(startVolume, 1, timer / 5);
            timer += Time.deltaTime;
            yield return null;
        }
        audioToFade.volume = 1;

        yield return null;

    }


    // Coroutine to fade out audio
    public IEnumerator FadeMusicOut(AudioSource audioToFade)
    {

        float startVolume = audioToFade.volume;
        float timer = 0f;

        while (audioToFade.volume > 0)
        {

            audioToFade.volume = Mathf.Lerp(startVolume, 0, timer / 5);
            timer += Time.deltaTime;
            yield return null;

        }

        yield return new WaitUntil(() => audioToFade.volume == 0);
        audioToFade.Stop(); // Stop the music once it's faded out

        Destroy(audioToFade.gameObject);
        Destroy(currentFXObject.gameObject);
        
        StopCoroutine(fadeCoroutine);

        fadeCoroutine = null;


        musicCoroutine = StartCoroutine(PlayMusic(transform, 1f));

        yield return null;

    }

}
