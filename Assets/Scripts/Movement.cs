using UnityEngine;

namespace Assets.Scripts
{
    public class Movement : MonoBehaviour
    {
        Rigidbody rb;
        AudioSource audioSource;

        [SerializeField] float mainThrust = 1000f;
        [SerializeField] float rotationThrust = 150f;
        [SerializeField] AudioClip engineSoundClip;
        [SerializeField] ParticleSystem mainBoosterParticle;
        [SerializeField] ParticleSystem leftBoosterParticle;
        [SerializeField] ParticleSystem rightBoosterParticle;



        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            ProcessThrust();
            ProcessRotation();
        }

        void ProcessThrust()
        {
            if (Input.GetKey(KeyCode.Space))
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
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineSoundClip, 0.5f);
            }

            if (!mainBoosterParticle.isPlaying)
            {
                mainBoosterParticle.Play();
            }
        }

        private void StopThrusting()
        {
            mainBoosterParticle.Stop();
            audioSource.Stop();
        }

        void ProcessRotation()
        {
            if (Input.GetKey(KeyCode.A))
            {
                ApplyLeftRotation();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                ApplyRightRotation();
            }
            else
            {
                StopRotationParticleEffects();
            }
        }

        private void ApplyLeftRotation()
        {
            ApplyRotation(rotationThrust);
            if (!rightBoosterParticle.isPlaying)
            {
                rightBoosterParticle.Play();
            }
        }

        private void ApplyRightRotation()
        {
            ApplyRotation(-rotationThrust);
            if (!leftBoosterParticle.isPlaying)
            {
                leftBoosterParticle.Play();
            }
        }

        private void StopRotationParticleEffects()
        {
            leftBoosterParticle.Stop();
            rightBoosterParticle.Stop();
        }

        private void ApplyRotation(float rotationThisFrame)
        {
            rb.freezeRotation = true; //freezing rotation so we can manually rotate
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
            rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
        }
    }
}
