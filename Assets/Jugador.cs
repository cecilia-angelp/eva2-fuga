using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public int vidas = 3;
    public float limiteCaida = -10f;
    private Vector3 posicionInicial;

    [Header("Sprites del Personaje")]
    public SpriteRenderer elSprite;
    public Sprite spriteIdle;
    public Sprite spriteCamina1;
    public Sprite spriteCamina2;
    public Sprite spriteSalto;
    public Sprite spriteGolpe;

    [Header("Interfaz y Disparo")]
    public TextMeshProUGUI textoMonedas;
    public int contadorMonedas = 0;
    public GameObject moldeBala;

    [Header("Audios")]
    public AudioClip sonidoDisparo; // Aquí va tu audio fall2

    private bool estaLastimado = false;

    void Start()
    {
        posicionInicial = transform.position;
        elSprite.sprite = spriteIdle;
    }

    void Update()
    {
        // Caer al vacío
        if (transform.position.y < limiteCaida && !estaLastimado)
        {
            transform.position = posicionInicial; // Vuelve al último punto seguro
            vidas -= 1;

            if (vidas > 0)
                StartCoroutine(MostrarCaraTriste());
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Muere definitivo
        }

        if (estaLastimado) return;

        // Giro 
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            elSprite.flipX = true;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            elSprite.flipX = false;

        // Disparo
        if (Input.GetKeyDown(KeyCode.W))
            Disparar();

        // Animaciones
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            elSprite.sprite = spriteSalto;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            elSprite.sprite = Mathf.Repeat(Time.time, 0.3f) < 0.15f ? spriteCamina1 : spriteCamina2;
        else
            elSprite.sprite = spriteIdle;
    }

    void Disparar()
    {
        GameObject nuevaBala = Instantiate(moldeBala, transform.position, Quaternion.identity);
        if (elSprite.flipX == true)
            nuevaBala.GetComponent<BalaEstrella>().velocidad = -10f;

        // Reproducir sonido de disparo
        if (sonidoDisparo != null)
        {
            AudioSource.PlayClipAtPoint(sonidoDisparo, transform.position);
        }
    }

    public void RecibirDano()
    {
        vidas -= 1;
        if (vidas > 0)
        {
            transform.position = new Vector3(transform.position.x - 2f, transform.position.y + 1f, transform.position.z);
            StartCoroutine(MostrarCaraTriste());
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator MostrarCaraTriste()
    {
        estaLastimado = true;
        elSprite.sprite = spriteGolpe;
        yield return new WaitForSeconds(1f);
        estaLastimado = false;
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        // Recolectar moneda
        if (otro.CompareTag("Moneda"))
        {
            contadorMonedas++;
            if (textoMonedas != null) textoMonedas.text = contadorMonedas.ToString("00") + " / 50";
            Destroy(otro.gameObject);
        }

        // Guardar nuevo punto de control
        if (otro.CompareTag("Checkpoint"))
        {
            posicionInicial = transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D choque)
    {
        // Detectar golpe con cualquier enemigo
        if (choque.gameObject.CompareTag("Enemigo"))
        {
            RecibirDano();
        }
    }
}