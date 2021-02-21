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
                rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
                if (!audioSource.isPlaying)
                {
                  audioSource.PlayOneShot(engineSoundClip, 0.5f);
                }
            }
            else
            {
                audioSource.Stop();
            }
        }

        void ProcessRotation()
        {
            if (Input.GetKey(KeyCode.A))
            {
                ApplyRotation(rotationThrust);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                ApplyRotation(-rotationThrust);
            }
        }

        private void ApplyRotation(float rotationThisFrame)
        {
            rb.freezeRotation = true; //freezing rotation so we can manually rotate
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
            rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
        }
    }
}
