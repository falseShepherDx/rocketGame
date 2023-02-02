using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float thrustForce = 3f;
    [SerializeField] private float rotationForce = 1.5f;
    private AudioSource audioSource;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainboosterParticles;
    [SerializeField] ParticleSystem leftboosterParticles;
    [SerializeField] ParticleSystem rightboosterParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        audioSource=GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        StartThrusting();

    }

    private void StartThrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainboosterParticles.isPlaying)
            {
                mainboosterParticles.Play();
            }
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainboosterParticles.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopParticles();
        }

    }

    private void StopParticles()
    {
        rightboosterParticles.Stop();
        leftboosterParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationForce);
     
        if (!leftboosterParticles.isPlaying)
        {
            leftboosterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationForce);
       
        if (!rightboosterParticles.isPlaying)
        {
            rightboosterParticles.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationForce *rotationThisFrame * Time.deltaTime);
        rb.freezeRotation=  false;
    }

}
