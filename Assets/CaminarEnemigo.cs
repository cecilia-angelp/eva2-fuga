using UnityEngine;

public class CaminarEnemigo : MonoBehaviour
{
    public float velocidad = 2f;
    public float distancia = 1.5f;
    private Vector3 posicionInicial;

    [Header("Animacion")]
    public SpriteRenderer elSprite;
    public Sprite camina1;
    public Sprite camina2;

    void Start()
    {
        posicionInicial = transform.position;
        // Esta linea hace el trabajo manual por ti
        elSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float nuevoX = posicionInicial.x + Mathf.Sin(Time.time * velocidad) * distancia;
        transform.position = new Vector3(nuevoX, transform.position.y, transform.position.z);

        if (Mathf.Repeat(Time.time, 0.4f) < 0.2f)
            elSprite.sprite = camina1;
        else
            elSprite.sprite = camina2;

        if (Mathf.Sin(Time.time * velocidad) > 0)
            elSprite.flipX = true;
        else
            elSprite.flipX = false;
    }
}