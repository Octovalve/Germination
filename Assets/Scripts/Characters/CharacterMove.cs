using UnityEngine;

/*
 * NOTA
 * Todo esta comentado, por favor leer o preguntar si algo no se entiende
 * Este script permite moverse, saltar y saltar doble.
 * Permite usar el salto en muros (identificados con un tag)
 * Y permite usar un knock, que es el impulso que sufre un jugador cuando se le golpea (un impulso contrario al ataque)
 *
 * 
 * By SantiagoIsAFK 
 */





public class CharacterMove : MonoBehaviour
{
    //Commponentes
    #region basic parameters 

    private Rigidbody2D theRB;
    private Transform tf;
    private Animator anim;
    #endregion
    
    //Variables de movimiento
    #region movement parameters
    [SerializeField]
    private float baseMoveSpeed, baseJumpForce;

    [SerializeField]
    private bool canMove=true, canDoubleJump = true, inGround = true; // Con la variable canMove se puede controlar el movimiento externamente
    
    

    [SerializeField]
    private bool inJumpingWall; //Determina si se esta en un muro
    [SerializeField]
    private bool jumpedFromWall; //Determina si acabo de saltar de un muro
    
    [SerializeField]
    private string jumpingWallTag, groundTag; //tag para identificar el terreno saltable
    private Vector2 jumpDirection=Vector2.up; //Indica la normal del punto de colision actual, por ejemplo, en el suelo es un vector hacia arriba
    [SerializeField]
    private float gravity, gravityInWall; //Los muros retrasan la caida vertical
    #endregion

    #region knock parameters
    //Variables para colision con enemigos
    [SerializeField]
    private float knockBackLenght, knockBackForce;
    [SerializeField]
    private bool inKnock; //mientras este nockeado no se puede mover
    #endregion

    #region properties
    public bool CanMove { get => canMove; set => canMove = value; }
    public bool InKnock { get => inKnock; }
    #endregion

    public void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tf = GetComponent<Transform>();
    }
    void Update()
    {
        if (canMove) //el movimiento lo bloquea un nockeo o una habilidad
        {
            if (!inKnock) //si se esta noqueado no se puede mover
            {
                MovePlayer();
            }
            UpdateAnimations();
        }
        else {

            //Si el movimiento esta bloqueado el personaje se movera por inercia en el aire hasta que toque el suelo
            if (inGround || inJumpingWall) 
            {
                theRB.velocity = new Vector2(0, theRB.velocity.y);
            }
        }

    }
    
    
    
    
    
    /// <summary>
    /// Movimiento y Salto del jugador
    /// </summary>
    private void MovePlayer()
    {
        //MOVIMIENTO HORIZONTAL
        //Luego de un salto desde un muro no se puede mover por un corto tiempo (Necesario para un correcto salto entre muros)
        if (!jumpedFromWall) 
        {
            //Moverse en el aire reduce velocidad
            if (inGround)
            {
                theRB.AddForce(Vector2.right * baseMoveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), ForceMode2D.Force);
            }
            else
            {
                theRB.AddForce(Vector2.right * baseMoveSpeed * Time.deltaTime * 0.8f * Input.GetAxis("Horizontal"), ForceMode2D.Force);
            }
        }


        //SALTO
        if (Input.GetButtonDown("Jump"))
        {
            if (inJumpingWall || inGround) //Si se va a realizar el primer salto
            {
                if (inJumpingWall) //Si se esta en un muro
                {
                    if (!jumpedFromWall) //Sin esto se puede tecelar rapido y muchas veces y se produciria un supersalto (bug)
                    {
                        jumpedFromWall = true;
                        CancelInvoke("StopJumpingWall");
                        Invoke("StopJumpingWall", 0.5f); //tiempo para devolver el movimiento

                        theRB.AddForce(jumpDirection * baseJumpForce, ForceMode2D.Impulse);
                        inGround = false;
                    }
                }
                else //Si no se esta en un muro se salta normal
                {
                    theRB.AddForce(jumpDirection * baseJumpForce, ForceMode2D.Impulse);
                    inGround = false;
                }
            }
            else //Si se va a realizar el segundo salto
            {
                //no se puede realizar un segundo salto inmediatamente luego de un salto de muro
                if (canDoubleJump && !jumpedFromWall) 
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, 0); //elimina la velocidad de la gravedad
                    theRB.AddForce(jumpDirection * baseJumpForce, ForceMode2D.Impulse);
                    canDoubleJump = false;
                }
            }
        }
    }
    private void StopJumpingWall() //Se invoca con delay
    {
        jumpedFromWall = false;
    }
    /// <summary>
    /// Se actualiza el animator y el gameobject para que coincida con las fisicas y el contexto
    /// </summary>
    private void UpdateAnimations() {
        
        
        /*if (inGround || inJumpingWall)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }*/
    }

    
    
    
    /// <summary>
    /// Permite nockear el jugador, debe ser llamado por quien produzca el daño con la direccion contraria al ataque
    /// </summary>
    /// <param name="direction"> direccion para lanzar el personaje, se normalizara</param>
    public void KnockBack(Vector2 direction)
    {
        inKnock = true;
        

        theRB.velocity = Vector2.zero;
        theRB.AddForce(NormalizeKnockDirection(direction) * knockBackForce, ForceMode2D.Impulse);
        anim.SetTrigger("hurt");

        CancelInvoke("StopKnock");
        Invoke("StopKnock", knockBackLenght);
    }
    private void StopKnock() //Se invoca con delay 
    {
        inKnock = false;
    }
    




    //LOS EVENTOS DE COLISSION ACTUALMENTE SON SOLO PARA LOS MUROS SALTABLES
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(jumpingWallTag)) //evita q el jugador rebote en un muro saltable, cuando lo toca le reduce la velocidad
        {
            theRB.velocity += new Vector2(0,0);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag) || collision.gameObject.CompareTag(jumpingWallTag)) {
            Vector2 normalDelLaColision = Vector2.zero;
            foreach (ContactPoint2D contact in collision.contacts) //CALCULA LOS PUNTOS DE COLISION
            {
                //encuentra la sumatoria de los puntos de colision
                normalDelLaColision += contact.normal;

                if ((Mathf.Abs(theRB.velocity.y) < 0.1f))
                {
                    
                    //Determina si se esta sobre el suelo
                    if ((int)(Vector2.Dot(contact.normal, Vector2.up) * 10) >= 5 && (int)(Vector2.Dot(contact.normal, Vector2.up) * 10) <= 10)
                    {
                        inGround = true;
                        canDoubleJump = true;

                        jumpDirection = Vector2.up;
                    }
                }
            }
        }

        if (collision.gameObject.CompareTag(jumpingWallTag)) //Permite configurar la direccion del salto para los muros saltables
        {
            jumpDirection = Vector2.zero;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                //encuentra la sumatoria de los puntos de colision
                jumpDirection += contact.normal;
            }

            //Comprueba que la direccion del muro sea lateral y no arriba ni abajo
            if (Mathf.Abs((int)(Vector2.Dot(jumpDirection, Vector2.up) * 10)) <= 5)
            {
                inJumpingWall = true;
                canDoubleJump = true;
                theRB.gravityScale = gravityInWall;


                //Se aplica una pequeña fuerza hacia el muro para no dejarlo despegar de este automaticamente
                theRB.AddForce(NormalizeWallJumpDirection(jumpDirection) * new Vector2(-8f, 0), ForceMode2D.Force);

                //se configura la direcion del salto, relativa al muro sobre el que esta colisionando
                jumpDirection = (NormalizeWallJumpDirection(jumpDirection));
            }
            else
            {
                //si el muro esta arriba o abajo se reinicia las variables
                inJumpingWall = false;
                jumpDirection = Vector2.up;
            }

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            inGround = false;
        }

        if (collision.gameObject.CompareTag(jumpingWallTag)) //si se despega del muro se reinician las variables
        {
            theRB.gravityScale = gravity;
            jumpDirection = Vector2.up;
            inJumpingWall = false;
        }
    }



    //NORMALIZACION DE LAS DIRECCIONES
    private Vector2 NormalizeKnockDirection(Vector2 originalDirection) {

        //El salto de un nockeo solo puede ir hacia los lados y hacia arriba

        Vector2 newDirection = Vector2.zero;

        if (originalDirection.x < 0)
        {
            newDirection += new Vector2(-1, 0);
        }
        else if (originalDirection.x > 0)
        {
            newDirection += new Vector2(1, 0);
        }
        
        if (originalDirection.y > 0)
        {
            newDirection += new Vector2(0, 1);
        }
        return newDirection;
    }

    private Vector2 NormalizeWallJumpDirection(Vector2 originalDirection)
    {
        //Lo saltos desde un muro saltable solo pueden ir diagonalmente hacia arriba

        Vector2 newDirection = Vector2.zero;

        if (originalDirection.x < 0)
        {
            newDirection += new Vector2(-0.9f, 1);
        }
        else if (originalDirection.x > 0)
        {
            newDirection += new Vector2(0.9f, 1);
        }

        return newDirection;
    }

    
}
