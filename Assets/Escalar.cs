using UnityEngine;

public class Escalar : MonoBehaviour
{
    private float entradaVertical;
    private bool enEscalera;
    private bool escalando;
    private Rigidbody2D rb;
    private float gravedadNormal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravedadNormal = rb.gravityScale;
    }

    void Update()
    {
        entradaVertical = Input.GetAxisRaw("Vertical");
        if (enEscalera && Mathf.Abs(entradaVertical) > 0f) escalando = true;
    }

    void FixedUpdate()
    {
        if (escalando)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, entradaVertical * 5f);
        }
        else rb.gravityScale = gravedadNormal;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Escalera")) enEscalera = true;
    }

    void OnTriggerExit2D(Collider2D otro)
    {
        if (otro.CompareTag("Escalera"))
        {
            enEscalera = false;
            escalando = false;
        }
    }
}