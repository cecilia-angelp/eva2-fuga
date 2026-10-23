using UnityEngine;

public class Llave : MonoBehaviour
{
    public AudioClip sonidoLlave; // Opcional: Sonido al recogerla

    private void OnTriggerEnter2D(Collider2D otro)
    {
        // Si el jugador la toca
        if (otro.CompareTag("Player") || otro.name.Contains("character"))
        {
            // Le suma 1 llave al GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SumarLlave(1);
            }

            // Reproduce el sonido de agarrar la llave
            if (sonidoLlave != null)
            {
                AudioSource.PlayClipAtPoint(sonidoLlave, transform.position);
            }

            // Destruye la llave de la pantalla
            Destroy(gameObject);
        }
    }
}