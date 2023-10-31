using Unity.Burst.CompilerServices;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    public float distanciaDelSuelo = 0.25f;
    public LayerMask Ground;
    [SerializeField] private Rigidbody2D rb;
    public float movH = 1f;

    //codigo del curso
    public RaycastHit2D hit;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //ponemos los rayCast para que detecten el suelo
        bool derecha = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y - 0.52f), Vector2.down, distanciaDelSuelo, Ground);
        Debug.DrawRay(new Vector2(transform.position.x + 0.4f, transform.position.y - 0.52f), Vector2.down * 0.25f, Color.red);

        bool izquierda = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y - 0.52f), Vector2.down, distanciaDelSuelo, Ground);
        Debug.DrawRay(new Vector2(transform.position.x - 0.4f, transform.position.y - 0.52f), Vector2.down * 0.25f, Color.red);

        if (derecha == false)
        {
            movH = -1;
        }
        if (izquierda == false)
        {
            movH = 1;
        }
        //choque con pared
        if (Physics2D.Raycast(transform.position, new Vector3(movH, 0, 0), frontCheck, Ground))
        {
            movH = movH * -1;
        }


        //choque con otros enmigos
        hit = Physics2D.Raycast(new Vector2(transform.position.x + movH * frontCheck, transform.position.y),
              new Vector2(movH, 0), frontDist);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            movH *= -1; // Cambiar la dirección de movimiento
        }

        if(Game.obj.paused == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movH * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //dañar a nuestro personaje
        if (collision.gameObject.CompareTag("Player"))
        {
            //golear jugador
            //Debug.Log("Mani te estan clavando corre de ahi bobo");
            Player.obj.getDamage();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //morir
        if (collision.gameObject.CompareTag("Player"))
        {
            //morir
            Dead();
            //Debug.Log("lo mataste Kbron, tenia hijos");
        }
    }

    void Dead()
    {
        AudioManager.obj.PlayEnemyHit();
        FXmanager.obj.verPop(transform.position);
        gameObject.SetActive(false);
    }
}