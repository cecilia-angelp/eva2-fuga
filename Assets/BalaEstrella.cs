using UnityEngine;

public class BalaEstrella : MonoBehaviour
{
    public float velocidad = 10f;

    void Update()
    {
        // Esto hace que la estrella avance sola hacia el frente
        transform.Translate(Vector3.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        // Si la estrella choca con algo que tenga la etiqueta Enemigo, se destruyen los dos
        if (otro.CompareTag("Enemigo"))
        {
            Destroy(otro.gameObject);
            Destroy(gameObject);
        }
    }
}