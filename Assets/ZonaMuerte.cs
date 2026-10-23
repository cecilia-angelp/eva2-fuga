using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
    private bool procesandoCaida = false;

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (procesandoCaida) return;

        if (otro.CompareTag("Player") || otro.GetComponent<Jugador>() != null || otro.name.Contains("character"))
        {
            if (GameManager.Instance != null)
            {
                procesandoCaida = true;
                GameManager.Instance.PerderVidaYPonerEnInicio(otro.gameObject);
                Invoke("RestablecerCaida", 0.5f);
            }
        }
    }

    void RestablecerCaida()
    {
        procesandoCaida = false;
    }
}