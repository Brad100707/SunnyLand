using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public Collider2D box;
    private Animator anim;
    private Rigidbody2D rb;
    public Collider2D coll;
    public float Speed;
    public float Jumpforce;
    public LayerMask Ground;
    int Cherry = 0;
    public Text CherryNumber;
    public bool isHurt;

    public Transform CellingCheck,GroundCheck;
    private bool isGround;
    public int ExtraJump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Crouch();
        CherryNumber.text = Cherry.ToString();
        NewJump();

    }

    void FixedUpdate()
    {
        if (!isHurt)
        {
            isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, Ground);
            float Horizontalmove = Input.GetAxis("Horizontal");
            float Facedirection = Input.GetAxisRaw("Horizontal");
            if (Horizontalmove != 0)
            {
                rb.velocity = new Vector3(Horizontalmove * Speed * Time.fixedDeltaTime, rb.velocity.y, 0);
                anim.SetFloat("running", Mathf.Abs(Facedirection));
            }
            if (Facedirection != 0)
            {
                transform.localScale = new Vector3(Facedirection, 1, 1);
            }
            


            ///if (Input.GetButton("Jump") && coll.IsTouchingLayers(Ground))
            //{
            // rb.velocity = new Vector3(rb.velocity.x, Jumpforce * Time.fixedDeltaTime, 0);
            //anim.SetBool("jumping", true);
            //anim.SetBool("falling", false);
            //JumpAudio.Play();
            //}



        }

        SwitchAnim();
    }

    void SwitchAnim()
    {

        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if(coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }


        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Collection")
        {
            SoundManger.Instance.CherryAudio();
            other.GetComponent<Animator>().Play("cherryget");
            //Destroy(other.gameObject);
            //Cherry += 1;
            
        }
        if (other.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 0.5f);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Enemy") 
        { 
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                
                enemy.JumpOn();

                rb.velocity = new Vector3(rb.velocity.x, Jumpforce * Time.deltaTime, 0);
                anim.SetBool("jumping", true);
            }
            else if(transform.position.x < other.gameObject.transform.position.x)
            {
                SoundManger.Instance.HurtAudio();
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > other.gameObject.transform.position.x)
            {
                SoundManger.Instance.HurtAudio();
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
    }

    void Crouch()
    {
        if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, Ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                GetComponent<BoxCollider2D>().enabled = true;

            }
        }
    }

    void NewJump()
    {
        if (isGround)
        {
            ExtraJump = 1;
        }
        if (Input.GetButtonDown("Jump") && ExtraJump > 0)
        {
            rb.velocity = Vector2.up * Jumpforce;
            ExtraJump--;
            anim.SetBool("jumping", true);
            //anim.SetBool("falling", false);
            SoundManger.Instance.JumpAudio();

        }
        if (Input.GetButtonDown("Jump") && ExtraJump == 0 && isGround)
        {
            rb.velocity = Vector2.up * Jumpforce;
            anim.SetBool("jumping", true);
            //anim.SetBool("falling", false);
            SoundManger.Instance.JumpAudio();

        }
    }

    void Restart()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CherryGotto()
    {
        Cherry+=1;
    }
}



