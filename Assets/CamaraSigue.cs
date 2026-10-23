using UnityEngine;

public class CamaraSigue : MonoBehaviour
{
    private Transform personaje;

    void Start()
    {
        // Busca a tu personaje automáticamente gracias al script que hicimos antes
        personaje = FindObjectOfType<MovimientoJugador>().transform;
    }

    void LateUpdate()
    {
        // Persigue al personaje hacia los lados y arriba/abajo, manteniendo su distancia (-10)
        if (personaje != null)
        {
            transform.position = new Vector3(personaje.position.x, personaje.position.y, -10f);
        }
    }
}