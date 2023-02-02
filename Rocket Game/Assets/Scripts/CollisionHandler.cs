using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip succes;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem succesParticles;

    private AudioSource audioSource;
    bool isTransitioning = false;   
    bool collisionDisabled=false;    
     void Start()
    {
    audioSource =GetComponent<AudioSource>();    
 
    }
     void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;

        }
    }
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentScene + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You hit the friendly.");
                break;
            case "Finish":
                StartSuccesSequence();
                break;
            default:
                StartCrashSequence();
                break;

        }
        void StartSuccesSequence()

        {
            succesParticles.Play();
            isTransitioning = true; 
            GetComponent<Movement>().enabled = false;
            audioSource.Stop();
            audioSource.PlayOneShot(succes);
            StartCoroutine(loadDelay());


        }
        IEnumerator loadDelay()
        {
            yield return new WaitForSeconds(delay);
            LoadNextLevel();
        }
        void StartCrashSequence()
        {
            crashParticles.Play();
            isTransitioning = true;
            GetComponent<Movement>().enabled = false;
            audioSource.Stop();
            audioSource.PlayOneShot(crash);
            StartCoroutine(waitdelay());
        }
         IEnumerator waitdelay()
        {
            yield return new WaitForSeconds(delay);
            ReloadLevel();
        }
        void ReloadLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
          void LoadNextLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentScene + 1;
            if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
