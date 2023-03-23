using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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

    private MobileCheck mc;

    private bool movingRight = false;
    private bool movingLeft = false;

    private bool firstTime;

    // Start is called before the first frame update
    void Start()
    {
        startGameManager = GameObject.Find("StartGameManager").GetComponent<StartGameManager>();
        mc = GameObject.Find("MobileCheck").GetComponent<MobileCheck>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        spaceBar.SetActive(false);
        movingRight = false;
        movingLeft = false;
        firstTime = true;
}

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOnSign = true;
        if (collision.CompareTag("Sign"))
        {
            signCollider = collision;
        }
        if (!mc.isMobile())
        {
            spaceBar.SetActive(true);
            spaceAnimator.SetBool("SignSpace", true);
        }
        if (startGameManager.IsSpanish)
        {
            signAnimator = collision.GetComponent<Sign>().PanelSpanish.GetComponent<Animator>();
        }
        else
        {
            signAnimator = collision.GetComponent<Sign>().PanelEnglish.GetComponent<Animator>();
        }
        
    }
    void Update()
    {
        if (!mc.isMobile())
        {
            if (Input.GetKey(KeyCode.D))
            {
                movingRight = true;
                movingLeft = false;
                MoveRight();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movingLeft = true;
                movingRight = false;
                MoveLeft();
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
                Interact();
            }
        }
        else
        {
            if (movingLeft)
            {
                rb.isKinematic = false;
                sr.flipX = true;
                transform.Translate(Vector2.left * (Time.deltaTime * speed), Space.Self);
                playerAnimator.SetBool("Ismoving", true);
            }
            else if (movingRight)
            {
                rb.isKinematic = false;
                sr.flipX = false;
                transform.Translate(Vector2.right * (Time.deltaTime * speed), Space.Self);
                playerAnimator.SetBool("Ismoving", true);
            }
            else
            {
                transform.Translate(Vector2.zero, Space.Self);
                rb.isKinematic = true;
                playerAnimator.SetBool("Ismoving", false);
            }
            if (isOnSign && firstTime)
            {
                firstTime = false;
                sign = signCollider.GetComponent<Sign>();
                StartCoroutine(sign.SignAction());
                if (cameraAnimator.GetBool("Pan") == false)
                {
                    cameraAnimator.SetBool("Pan", true);
                    playerAnimator.SetBool("Sign", true);
                }
                if (signAnimator.GetBool("PanelActive") == false)
                {
                    signAnimator.SetBool("PanelActive", true);
                    playerAnimator.SetBool("Sign", true);
                }
            }
            else if(!isOnSign)
            {
                firstTime = true;
                sign = signCollider.GetComponent<Sign>();
                StartCoroutine(sign.SignAction());
                if (cameraAnimator.GetBool("Pan") == true)
                {
                    cameraAnimator.SetBool("Pan", false);
                    playerAnimator.SetBool("Sign", false);
                }
                if (signAnimator.GetBool("PanelActive") == true)
                {
                    signAnimator.SetBool("PanelActive", false);
                    playerAnimator.SetBool("Sign", false);
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnSign = false;
        if (!mc.isMobile())
        {
            spaceAnimator.SetBool("SignSpace", false);
            spaceBar.SetActive(false);
        }
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
    public void MoveRight()
    {
        rb.isKinematic = false;
        sr.flipX = false;
        transform.Translate(Vector2.right * (Time.deltaTime * speed), Space.Self);
        playerAnimator.SetBool("Ismoving", true);
    }    public void MoveLeft()
    {
        rb.isKinematic = false;
        sr.flipX = true;
        transform.Translate(Vector2.left * (Time.deltaTime * speed), Space.Self);
        playerAnimator.SetBool("Ismoving", true);
    }

    public void PointerDownLeft()
    {
        movingLeft = true;
    }
    public void PointerUpLeft()
    {
        movingLeft = false;
    }
    public void PointerDownRight()
    {
        movingRight = true;
    }
    public void PointerUpRight()
    {
        movingRight = false;
    }
    public void Interact()
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
