using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Variables")]
    public float speed = 50f;

    [Header("Animators")]

    public Animator playerAnimator;

    public Animator cameraAnimator;

    public Animator signAnimator;

    public Animator spaceAnimator;

    private Sign sign;

    private bool isOnSign = false;

    private Collider2D signCollider;

    private bool clicked = false;

    public GameObject spaceBar;

    public GameObject RightKey;
    public GameObject LeftKey;
    public GameObject SpaceKey;

    private SpriteRenderer sr;

    private StartGameManager startGameManager;

    // Start is called before the first frame update
    void Start()
    {
        startGameManager = GameObject.Find("StartGameManager").GetComponent<StartGameManager>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        spaceBar.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOnSign = true;
        if (collision.CompareTag("Sign"))
        {
            signCollider = collision;
        }
        spaceBar.SetActive(true);
        if (startGameManager.IsSpanish)
        {
            signAnimator = collision.GetComponent<Sign>().PanelSpanish.GetComponent<Animator>();
        }
        else
        {
            signAnimator = collision.GetComponent<Sign>().PanelEnglish.GetComponent<Animator>();
        }
        spaceAnimator.SetBool("SignSpace", true);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.isKinematic = false;
            sr.flipX = false;
            transform.Translate(Vector2.right * (Time.deltaTime * speed), Space.Self);
            playerAnimator.SetBool("Ismoving", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.isKinematic = false;
            sr.flipX = true;
            transform.Translate(Vector2.left * (Time.deltaTime * speed), Space.Self);
            playerAnimator.SetBool("Ismoving", true);
        }
        else
        {
            transform.Translate(Vector2.zero, Space.Self);
            rb.isKinematic = true;
            playerAnimator.SetBool("Ismoving", false);
        }

        //Sign Stuff
        if (Input.GetKeyDown(KeyCode.Space) && isOnSign)
        {
            if (!clicked)
            {
                StartCoroutine(WaitClick());
                sign = signCollider.GetComponent<Sign>();
                StartCoroutine(sign.SignAction());
                if (cameraAnimator.GetBool("Pan") == true)
                {
                    cameraAnimator.SetBool("Pan", false);
                    playerAnimator.SetBool("Sign", false);
                    RightKey.SetActive(true);
                    LeftKey.SetActive(true);
                    SpaceKey.SetActive(true);
                }
                else
                {
                    cameraAnimator.SetBool("Pan", true);
                    playerAnimator.SetBool("Sign", true);
                    RightKey.SetActive(false);
                    LeftKey.SetActive(false);
                    SpaceKey.SetActive(false);
                }
                if (signAnimator.GetBool("PanelActive") == true)
                {
                    signAnimator.SetBool("PanelActive", false);
                    playerAnimator.SetBool("Sign", false);
                    RightKey.SetActive(true);
                    LeftKey.SetActive(true);
                    SpaceKey.SetActive(true);
                }
                else
                {
                    signAnimator.SetBool("PanelActive", true);
                    playerAnimator.SetBool("Sign", true);
                    RightKey.SetActive(false);
                    LeftKey.SetActive(false);
                    SpaceKey.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnSign = false;
        spaceAnimator.SetBool("SignSpace", false);
        spaceBar.SetActive(false);
        if (collision == signCollider)
        {
            sign = signCollider.GetComponent<Sign>();
            if (sign.isActive)
            {
                cameraAnimator.SetBool("Pan", false);
                signAnimator.SetBool("PanelActive", false);
                playerAnimator.SetBool("Sign", false);
                sign.TurnOff();
                RightKey.SetActive(true);
                LeftKey.SetActive(true);
                SpaceKey.SetActive(true);
                sign.isActive = false;
            }
            signCollider = null;
        }
    }
    private IEnumerator WaitClick()
    {
        clicked = true;
        yield return new WaitForSeconds(2f);
        clicked = false;
    }

}