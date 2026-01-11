using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;
public class Movment : MonoBehaviour
{
    [SerializeField] InputAction thrust ;
    [SerializeField] InputAction rotation;

    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles; 
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    
    Rigidbody rb;

    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
        
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

   private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    private void StopThrusting()
    {
        mainEngineParticles.Stop();
        audioSource.Stop();
    }

 

    private void ProcessRotation(){
        float rotationValue = rotation.ReadValue<float>();
        if (rotationValue < 0)
        {
            LeftRotation();

        }
        else if(rotationValue > 0)
        {
            RightRotation();
        }
        else
        {
            StopRotation();
        }

    }


    private void LeftRotation()
    {
        ApplyRotation(rotationSpeed);
        if (!leftThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Play();
        }
    }

    private void RightRotation()
    {
        ApplyRotation(-rotationSpeed);
        if (!rightThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Play();
        }
    }
    private void StopRotation()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }



    private void ApplyRotation(float rotationSide)
    {
        rb.freezeRotation=true;
        transform.Rotate(Vector3.forward * Time.fixedDeltaTime * rotationSide);
        rb.freezeRotation=false;

    }
}
