using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player obj;

    public int vidas = 3;
    public bool isGround = false;
    public bool isDead = false;
    public bool isMoving = true;

    public float speed = 5f;
    public float jumpForce = 2f;
    public float movHor;

    public float graundRayDistance = 0.3f;
    public float radius = 0.4f;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private Animator anim;
    public SpriteRenderer sr;
    public float cont=0f;

    //inmunidad
    public bool isImmune = false;
    public float immuneTimeCont = 0f;
    public float immuneTime = 0.5f;

    //COYOTE Time
    [SerializeField] private bool coyoteTime = false;
    [SerializeField] private float tiempoCoyote;
    [SerializeField] private float tiempoCoyoteTime = 0.2f;


    void Awake()
    {
        obj = this;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movHor = Input.GetAxis("Horizontal");
        tocarSuelo();
        tocarCielo();
        isMoving = (movHor != 0);

        if (Game.obj.paused) {
            movHor = 0;
            return;
        }

        cont += Time.deltaTime;

        if (!isGround & coyoteTime) {
            tiempoCoyote += Time.deltaTime;
            if (tiempoCoyote > tiempoCoyoteTime)
            {
                coyoteTime = false;
            }
        }


        if (isImmune){
            sr.enabled = !sr.enabled;
            immuneTimeCont -= Time.deltaTime;

            if(immuneTimeCont <= 0)
            {
                isImmune = false;
                sr.enabled = true;
            }
        }

        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.10f), Vector2.down * 0.3f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x - 0.10f, transform.position.y-0.25f), Vector2.down * 0.3f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + 0.10f, transform.position.y-0.25f), Vector2.down * 0.3f, Color.red);

        Debug.DrawRay(new Vector2(transform.position.x + 0.10f, transform.position.y), Vector2.up*0.3f, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x - 0.10f, transform.position.y), Vector2.up * 0.3f, Color.blue);

        if (Input.GetKeyDown(KeyCode.Space) && cont>=0.6f && (isGround== true || coyoteTime== true)) { 
            jump();
            cont = 0f;
        }

        Debug.DrawRay(transform.position, -transform.up * graundRayDistance, Color.red);

        anim.SetBool("isGround", isGround);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isImmune", isImmune);

        flip(movHor);
    }

    private void tocarCielo()
    {
        RaycastHit2D derechaArriba = Physics2D.Raycast(new Vector2(transform.position.x + 0.10f, transform.position.y),
                    Vector2.up, graundRayDistance, groundMask);

        RaycastHit2D izquierdaArriba = Physics2D.Raycast(new Vector2(transform.position.x - 0.10f, transform.position.y),
            Vector2.up, graundRayDistance, groundMask);

        if (derechaArriba && !izquierdaArriba) {
            transform.position += new Vector3(0.2f,0);
        }
        else if (izquierdaArriba && !derechaArriba)
        {
            transform.position -= new Vector3(0.2f, 0);
        }
    }

    private void tocarSuelo()
    {
        RaycastHit2D hitCenter = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.10f),
            Vector2.down, graundRayDistance, groundMask);

        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(transform.position.x - 0.10f, transform.position.y - 0.25f),
            Vector2.down, graundRayDistance, groundMask);

        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(transform.position.x + 0.10f, transform.position.y - 0.25f),
                    Vector2.down, graundRayDistance, groundMask);

        if (hitCenter.collider != null || hitLeft.collider != null || hitRight.collider != null)
        {
            isGround = true;
            coyoteTime = true;
            tiempoCoyote = 0f;
        }
        else
        {
            isGround = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);
    }

    private void goImune()
    {
        isImmune = true;
        immuneTimeCont = immuneTime;
    }

    public void jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        AudioManager.obj.PlayJump();
    }

    private void flip(float Xvalue)
    {
        Vector3 theScale = transform.localScale;

        if(Xvalue < 0)
        {
            theScale.x = math.abs(theScale.x) * -1;
        }else if (Xvalue > 0)
        {
            theScale.x = math.abs(theScale.x);
        }

        transform.localScale = theScale;
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }*/

    public void addLives(int lives)
    {
        vidas = vidas + lives;
        UImanager.obj.UpdateLives();
        if (vidas > Game.obj.vidaMax) 
        { 
            vidas = Game.obj.vidaMax;
        }
    }

    public void getDamage()
    {
       vidas--;
        UImanager.obj.UpdateLives();
        goImune();
       AudioManager.obj.PlayHit();
       FXmanager.obj.verPop(transform.position);
       if (vidas <= 0)
       {
           this.gameObject.SetActive(false);
           Game.obj.gameOver();
       }
    }

    void OnDestroy()
    {
        obj = null;
    }
}
