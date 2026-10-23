using UnityEngine;

public class Moneda : MonoBehaviour
{
    public AudioClip sonidoMoneda;
    private bool recolectado = false;

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (recolectado) return;

        if (otro.CompareTag("Player") || otro.GetComponent<Jugador>() != null || otro.name.Contains("character"))
        {
            recolectado = true;

            if (GameManager.Instance != null)
            {
                if (gameObject.name.ToLower().Contains("key") || gameObject.name.ToLower().Contains("llave"))
                {
                    GameManager.Instance.SumarLlave(1);
                }
                else
                {
                    GameManager.Instance.SumarMoneda(1);
                }
            }

            // Reproducir el sonido antes de destruir el objeto
            if (sonidoMoneda != null)
            {
                AudioSource.PlayClipAtPoint(sonidoMoneda, transform.position);
            }

            Destroy(gameObject);
        }
    }
}