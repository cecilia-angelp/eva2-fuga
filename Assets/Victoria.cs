using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Victoria : MonoBehaviour
{
    [Header("Configuracion de Victoria")]
    public AudioClip sonidoVictoria;
    public GameObject imagenVictoria; // Esta es la casilla que te faltaba
    public string nombreEscenaMenu = "Menu";

    private bool nivelCompletado = false;

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (nivelCompletado) return;

        if (otro.CompareTag("Player") || otro.name.Contains("character"))
        {
            if (GameManager.Instance != null && GameManager.Instance.llaves > 0)
            {
                nivelCompletado = true;

                GameObject musicaObj = GameObject.Find("MusicaJuego");
                if (musicaObj != null)
                {
                    AudioSource audioMusica = musicaObj.GetComponent<AudioSource>();
                    if (audioMusica != null) audioMusica.Stop();
                }

                if (imagenVictoria != null)
                {
                    imagenVictoria.SetActive(true);
                }

                if (sonidoVictoria != null)
                {
                    AudioSource.PlayClipAtPoint(sonidoVictoria, Camera.main.transform.position, 1f);
                }

                otro.gameObject.SetActive(false);
                StartCoroutine(IrAlMenu());
            }
        }
    }

    IEnumerator IrAlMenu()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}