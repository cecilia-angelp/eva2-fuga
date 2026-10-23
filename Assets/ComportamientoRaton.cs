using UnityEngine;

public class ComportamientoRaton : MonoBehaviour
{
    public bool despierto = false;
    public float velocidad = 3f;
    public float fuerzaSaltoExtra = 14f; // Altura del rebote
    private Vector3 posicionInicial;

    [Header("Animacion")]
    public SpriteRenderer elSprite;
    public Sprite ratonDormido;
    public Sprite camina1;
    public Sprite camina2;

    void Start()
    {
        posicionInicial = transform.position;
        elSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Si está dormido, muestra la imagen de descanso y no hace nada más
        if (!despierto)
        {
            elSprite.sprite = ratonDormido;
            return;
        }

        // Si despierta, comienza a patrullar
        float nuevoX = posicionInicial.x + Mathf.Sin(Time.time * velocidad) * 1.5f;
        transform.position = new Vector3(nuevoX, transform.position.y, transform.position.z);

        // Intercambiar dibujos para caminar
        if (Mathf.Repeat(Time.time, 0.4f) < 0.2f) elSprite.sprite = camina1;
        else elSprite.sprite = camina2;

        elSprite.flipX = Mathf.Sin(Time.time * velocidad) > 0;
    }

    private void OnCollisionEnter2D(Collision2D choque)
    {
        if (choque.gameObject.CompareTag("Player"))
        {
            // Detectar si el jugador está más arriba que el ratón (lo pisa)
            if (choque.transform.position.y > transform.position.y + 0.3f)
            {
                // Impulsar al jugador hacia arriba
                Rigidbody2D rbJugador = choque.gameObject.GetComponent<Rigidbody2D>();
                rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, fuerzaSaltoExtra);

                Destroy(gameObject); // Destruir al ratón
            }
            else
            {
                // Si lo toca por el lado, envía la orden de restar vida a tu script Jugador
                choque.gameObject.SendMessage("RecibirDano", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}