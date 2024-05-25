using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int pointsGiven = 50;
    public int pointsLost = 25;
    public float jumpForce = 10f;
    public bool isJumping = false;
    public float spikeActivationTime = 5f;
    public AudioSource jumpEffect;
    public AudioSource spikesEffect;


    private Rigidbody2D rigidBody;
    private Collider2D playerCollider;
    private bool isOnPlatform = false;
    private float timeOnPlatform = 0f;
    private GameObject currentPlatform = null;
    private bool spikeActivated = false;

    private Animator animator;
    private Vector3 initialPosition;

    //[SerializeField] private Animator animator;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!GameManager.Instance.RoundFinished() && GameManager.Instance.playing)
        {
            if (!isJumping && isOnPlatform && !spikeActivated)
            {
                timeOnPlatform += Time.deltaTime;
                //Debug.Log(timeOnPlatform);
                if (timeOnPlatform >= spikeActivationTime)
                {
                    spikeActivated = true;
                    ActivateSpikes();
                    spikesEffect.Play();
                    timeOnPlatform = 0;
                    GameManager.Instance.AddPoints(-pointsLost);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                JumpToNextPlatform();
            }
        }

    }

    public void JumpToNextPlatform()
    {
        animator.SetBool("isJumping", true);
        jumpEffect.Play();
        spikeActivated = false;

        GameManager.Instance.AddPoints(pointsGiven);
        GameManager.Instance.numJumps++;
        Debug.Log($"Saltos: {GameManager.Instance.numJumps}");

        playerCollider.enabled = false;
        isJumping = true;
        isOnPlatform = false;
        timeOnPlatform = 0;

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        StartCoroutine(EnablePlatformCollision());
    }

    IEnumerator EnablePlatformCollision()
    {
        yield return new WaitForSeconds(0.5f);
        playerCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            animator.SetBool("isJumping", false);
            isJumping = false;
            isOnPlatform = true;
            currentPlatform = collision.gameObject;

            if (GameManager.Instance.RoundFinished())
            {
                GameManager.Instance.EndRound();

                if (!GameManager.Instance.GameFinished())
                    Invoke("ResetPlayerPosition", 2.0f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && GameManager.Instance.playing)
        {
            isOnPlatform = false;
            DisableSpikes();
            currentPlatform = null;
        }
    }

    private void ActivateSpikes()
    {
        if (currentPlatform != null)
        {
            SpikeController spikeController = currentPlatform.GetComponent<SpikeController>();
            if (spikeController != null)
            {
                spikeController.ActivateSpikes();
            }
        }
    }

    private void DisableSpikes()
    {
        if (currentPlatform != null)
        {
            SpikeController spikeController = currentPlatform.GetComponent<SpikeController>();
            if (spikeController != null)
            {
                spikeController.DesactivateSpikes();
            }
        }
    }

    public void ResetPlayerPosition()
    {
        transform.position = initialPosition;
    }
}
