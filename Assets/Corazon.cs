using UnityEngine;

public class Corazon : MonoBehaviour
{
    private bool recolectado = false;

    private void OnTriggerEnter2D(Collider2D otro)
    {
        VerificarToque(otro.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D otro)
    {
        VerificarToque(otro.gameObject);
    }

    void VerificarToque(GameObject obj)
    {
        if (recolectado) return;

        if (obj.CompareTag("Player") || obj.GetComponent<Jugador>() != null || obj.name.Contains("character"))
        {
            recolectado = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.SumarVida(1);
            }

            Destroy(gameObject);
        }
    }
}