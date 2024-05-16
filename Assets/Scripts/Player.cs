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

    //[SerializeField] private Animator animator;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!GameManager.Instance.RoundFinished())
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
            else
            {
                timeOnPlatform = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                isJumping = true;
                jumpEffect.Play();
                spikeActivated = false;
                JumpToNextPlatform();
                //DesactivateSpikes();
                GameManager.Instance.AddPoints(pointsGiven);
                Debug.Log(GameManager.Instance.numJumps);
            }
        }
        else if (GameManager.Instance.RoundFinished())
        {
            GameManager.Instance.EndRound();
        }

    }

    public void JumpToNextPlatform()
    {
        playerCollider.enabled = false;
        isJumping = true;

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        StartCoroutine(EnablePlatformCollision());
    }

    IEnumerator EnablePlatformCollision()
    {
        yield return new WaitForSeconds(0.5f);
        playerCollider.enabled = true;
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            //Debug.Log("sfsdf");
            isJumping = false;
            isOnPlatform = true;
            currentPlatform = collision.gameObject;

            //ActivateSpikes();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
            currentPlatform = null;
            DisableSpikes();
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
    /*public void PerformMovement()
    {
        if (GameManager.Instance.playerAnim) 
            animator.SetBool("AnimationTrigger", true);
        else animator.SetBool("AnimationTrigger", false);
    }*/
}
