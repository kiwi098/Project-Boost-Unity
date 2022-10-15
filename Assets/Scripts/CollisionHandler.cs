using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource my_as;

    [SerializeField] float timeDelay = 0f;

    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip deathSound;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;

    bool doneSomething = false;

    // Start is called before the first frame update
    void Start()
    {
        my_as = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
        if(doneSomething == false)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Safe!");
                    break;
                case "Finish":
                    Debug.Log("Finish!");
                    GetComponent<Movement>().enabled = false;
                    my_as.Stop();
                    my_as.PlayOneShot(successSound);
                    successParticle.Play();
                    doneSomething = true;
                    Invoke("NextLevel", timeDelay);
                    break;
                case "Fuel":
                    Debug.Log("Fuel!");
                    break;
                default:
                    Debug.Log("Boom!");
                    GetComponent<Movement>().enabled = false;
                    my_as.Stop();
                    my_as.PlayOneShot(deathSound);
                    deathParticle.Play();
                    doneSomething = true;
                    Invoke("ReloadLevel", timeDelay);
                    break;
            }
        }
    }

    void ReloadLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    void NextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;

        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }
}
