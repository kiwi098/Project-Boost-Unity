using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Rigidbody my_rb;
    AudioSource my_as;
    BoxCollider my_bc;
    
    [SerializeField] AudioClip engineThrust;
    [SerializeField] float thrustPower = 1f;
    [SerializeField] float turnPower = 1f;

    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem turnleftParticle;
    [SerializeField] ParticleSystem turnrightParticle;

    // Start is called before the first frame update
    void Start()
    {
        my_rb = GetComponent<Rigidbody>();
        my_as = GetComponent<AudioSource>();
        my_bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        DebugInput();
    }

    void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Thrust");
            my_rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustPower);

            if(!my_as.isPlaying)
            {
                my_as.PlayOneShot(engineThrust);
            }

            if(!thrustParticle.isPlaying)
            {
                thrustParticle.Play();
            }
        }
        else
        {
            my_as.Stop();
            thrustParticle.Stop();
        }

        if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("RotateLeft");
            my_rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * Time.deltaTime * turnPower);
            my_rb.freezeRotation = false;

            if(!turnleftParticle.isPlaying)
            {
                turnleftParticle.Play();
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Debug.Log("RotateRight");
            my_rb.freezeRotation = true;
            transform.Rotate(Vector3.back * Time.deltaTime * turnPower);
            my_rb.freezeRotation = false;

            if(!turnrightParticle.isPlaying)
            {
                turnrightParticle.Play();
            }
        }
        else
        {
            turnleftParticle.Stop();
            turnrightParticle.Stop();
        }
    }

    void DebugInput()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int nextLevel = currentLevel + 1;

            if (nextLevel == SceneManager.sceneCountInBuildSettings)
            {
                nextLevel = 0;
            }
            SceneManager.LoadScene(nextLevel);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(my_bc.enabled == true)
            {
                my_bc.enabled = false;
            }
            else
            {
                my_bc.enabled = true;
            }
        }
    }
}
