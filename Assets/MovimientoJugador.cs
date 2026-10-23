using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    [Header("Audios del Personaje")]
    public AudioClip sonidoSalto;
    public AudioClip sonidoDisparo;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(movimientoX * velocidad, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            if (sonidoSalto != null) AudioSource.PlayClipAtPoint(sonidoSalto, transform.position);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DispararEstrella();
        }

        if (transform.position.y < -10f)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PerderVidaYPonerEnInicio(gameObject);
            }
        }
    }

    public void DispararEstrella()
    {
        if (sonidoDisparo != null) AudioSource.PlayClipAtPoint(sonidoDisparo, transform.position);
    }
}