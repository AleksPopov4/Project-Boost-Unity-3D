using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] private float delayTime = 1f;
    [SerializeField] AudioClip crashAudioClip;
    [SerializeField] AudioClip landAudioClip;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into friendly object");
                break;
            case "Finish":
                StartLoadNextLevelSequence();
                Debug.Log("Landed on finish");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudioClip, 0.2f);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delayTime);
    }

    void StartLoadNextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(landAudioClip, 0.2f);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", delayTime);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = ++currentSceneIndex;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
