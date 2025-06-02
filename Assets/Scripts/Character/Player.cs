using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private AudioClip _playSound;
    [SerializeField] private AudioClip _waterStepSound;
    [SerializeField] private float movingSpeed = 5;
    public Animator animator;
    private Vector2 direction;
    private Rigidbody2D rb;
    public VectorValue pos;
    private AudioSource _audioSource;
    private bool isPlayingStepSound = false;
    private bool isInWater = false;

    private void Start()
    {
        pos.initialValue = new Vector3(0, 0, 0);
        transform.position = pos.initialValue;

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogWarning("звука нет");
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
        _audioSource.playOnAwake = false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        float currentSpeed = movingSpeed;
        float soundPitch = 1.0f;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed *= 2;
            soundPitch = 1.5f;
        }

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);

        MovePlayer(currentSpeed);
        HandleStepSound(soundPitch);
    }

    void MovePlayer(float speed)
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void HandleStepSound(float pitch)
    {
        if (_audioSource == null)
        {
            return;
        }

        if (direction.sqrMagnitude > 0)
        {
            if (!isPlayingStepSound)
            {
                _audioSource.loop = true;
                _audioSource.clip = isInWater ? _waterStepSound : _playSound;
                _audioSource.Play();
                isPlayingStepSound = true;
            }

            _audioSource.pitch = pitch;
        }
        else
        {
            if (isPlayingStepSound)
            {
                _audioSource.Stop();
                _audioSource.pitch = 1.0f;
                isPlayingStepSound = false;
            }
        }
    }

    public void SetWaterState(bool inWater)
    {
        isInWater = inWater;
        if (isPlayingStepSound)
        {
            _audioSource.Stop();
            _audioSource.clip = isInWater ? _waterStepSound : _playSound;
            _audioSource.Play();
        }
    }
}
