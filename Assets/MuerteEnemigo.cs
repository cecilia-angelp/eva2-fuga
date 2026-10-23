using UnityEngine;

public class MuerteEnemigo : MonoBehaviour
{
    [Header("Sonido")]
    public AudioClip sonidoMuerte; // Esta es la casilla que pediste

    private void OnTriggerEnter2D(Collider2D otro)
    {
        ProcesarGolpe(otro.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D choque)
    {
        ProcesarGolpe(choque.gameObject);
    }

    void ProcesarGolpe(GameObject objetoChocador)
    {
        // Detecta si lo que golpea al enemigo es la estrella/bala
        if (objetoChocador.name.Contains("star") || objetoChocador.CompareTag("Bala") || objetoChocador.GetComponent<BalaEstrella>() != null)
        {
            // Reproduce el sonido
            if (sonidoMuerte != null)
            {
                AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position);
            }

            // Destruye la estrella
            Destroy(objetoChocador);

            // Destruye al enemigo
            Destroy(gameObject);
        }
    }
}