using UnityEngine;

public class Flotar : MonoBehaviour
{
    public float velocidad = 4f; // Qué tan rápido aletea/flota
    public float altura = 0.3f;  // Qué tanto sube y baja
    private Vector3 posicionInicial;

    void Start()
    {
        // Guarda el punto exacto donde pusiste la abeja en el mapa
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Calcula un movimiento suave de ola (arriba y abajo)
        float nuevoY = posicionInicial.y + Mathf.Sin(Time.time * velocidad) * altura;
        transform.position = new Vector3(posicionInicial.x, nuevoY, posicionInicial.z);
    }
}