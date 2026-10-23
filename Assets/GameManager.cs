using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[ExecuteAlways]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int vidas = 3;
    public int monedas = 0;
    public int llaves = 0;

    public TextMeshProUGUI textoVidas;
    public TextMeshProUGUI textoMonedas;
    public TextMeshProUGUI textoLlaves;
    public Button botonPausa;
    public Button botonMenu;
    public GameObject pantallaPerdiste;

    public string nombreEscenaMenu = "Menu";

    private Vector3 ultimaPosicionCheckpoint;
    private bool posicionInicialGuardada = false;
    private bool estaPausado = false;
    private bool juegoTerminado = false;
    private AudioSource audioMusicaFondo;

    void Awake()
    {
        if (Application.isPlaying)
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
    }

    void Start()
    {
        ConfigurarTodoAutomatico();

        if (Application.isPlaying)
        {
            Time.timeScale = 1f;
            juegoTerminado = false;

            GameObject musicaObj = GameObject.Find("MusicaJuego");
            if (musicaObj != null) audioMusicaFondo = musicaObj.GetComponent<AudioSource>();

            GameObject jugador = GameObject.FindWithTag("Player");
            if (jugador != null && !posicionInicialGuardada)
            {
                ultimaPosicionCheckpoint = jugador.transform.position;
                posicionInicialGuardada = true;
            }

            if (botonPausa != null)
            {
                botonPausa.onClick.RemoveAllListeners();
                botonPausa.onClick.AddListener(TogglePausa);
            }

            if (botonMenu != null)
            {
                botonMenu.onClick.RemoveAllListeners();
                botonMenu.onClick.AddListener(IrAlMenu);
            }

            if (pantallaPerdiste != null)
            {
                Button btnReiniciar = pantallaPerdiste.GetComponentInChildren<Button>();
                if (btnReiniciar != null)
                {
                    btnReiniciar.onClick.RemoveAllListeners();
                    btnReiniciar.onClick.AddListener(ReiniciarJuego);
                }
                pantallaPerdiste.SetActive(false);
            }
        }
        ActualizarUI();
    }

    void Update()
    {
        if (!Application.isPlaying) return;

        if (juegoTerminado)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) ReiniciarJuego();
            return;
        }

        if (Input.GetKeyDown(KeyCode.P)) TogglePausa();
        if (Input.GetKeyDown(KeyCode.M)) IrAlMenu();
    }

    void ConfigurarTodoAutomatico()
    {
        if (botonPausa != null)
        {
            RectTransform rt = botonPausa.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchorMin = new Vector2(1f, 1f); rt.anchorMax = new Vector2(1f, 1f);
                rt.pivot = new Vector2(1f, 1f); rt.anchoredPosition3D = new Vector3(-20f, -15f, 0f);
            }
        }

        if (botonMenu != null)
        {
            RectTransform rtMenu = botonMenu.GetComponent<RectTransform>();
            if (rtMenu != null)
            {
                rtMenu.anchorMin = new Vector2(1f, 1f); rtMenu.anchorMax = new Vector2(1f, 1f);
                rtMenu.pivot = new Vector2(1f, 1f); rtMenu.anchoredPosition3D = new Vector3(-150f, -15f, 0f);
            }
            TextMeshProUGUI txtMenu = botonMenu.GetComponentInChildren<TextMeshProUGUI>();
            if (txtMenu != null) txtMenu.text = "Menu (M)";
        }
    }

    public void ActualizarUI()
    {
        if (textoMonedas == null) textoMonedas = GameObject.Find("TextMoneda (TMP)")?.GetComponent<TextMeshProUGUI>();
        if (textoVidas == null) textoVidas = GameObject.Find("TextVidas (TMP)")?.GetComponent<TextMeshProUGUI>();
        if (textoLlaves == null) textoLlaves = GameObject.Find("TextLlaves (TMP)")?.GetComponent<TextMeshProUGUI>();

        if (textoMonedas != null) textoMonedas.text = "Monedas: " + monedas;
        if (textoVidas != null) textoVidas.text = "Vidas: " + vidas;
        if (textoLlaves != null) textoLlaves.text = "Llaves: " + llaves;
    }

    public void SumarMoneda(int cantidad) { monedas += cantidad; ActualizarUI(); }
    public void SumarVida(int cantidad) { vidas += cantidad; ActualizarUI(); }
    public void SumarLlave(int cantidad) { llaves += cantidad; ActualizarUI(); }

    public void ActualizarCheckpoint(Vector3 nuevaPosicion)
    {
        ultimaPosicionCheckpoint = nuevaPosicion;
        posicionInicialGuardada = true;
    }

    public void PerderVidaYPonerEnInicio(GameObject jugador)
    {
        if (juegoTerminado) return;

        vidas--;
        ActualizarUI();

        if (vidas <= 0)
        {
            MostrarPerdiste();
        }
        else
        {
            if (posicionInicialGuardada)
            {
                jugador.transform.position = ultimaPosicionCheckpoint;
            }
            Rigidbody2D rb = jugador.GetComponent<Rigidbody2D>();
            if (rb != null) { rb.linearVelocity = Vector2.zero; rb.angularVelocity = 0f; }
        }
    }

    public void MostrarPerdiste()
    {
        juegoTerminado = true;
        Time.timeScale = 0f;
        if (pantallaPerdiste != null)
        {
            pantallaPerdiste.SetActive(true);
            pantallaPerdiste.transform.SetAsLastSibling();
        }
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscenaMenu);
    }

    public void TogglePausa()
    {
        if (juegoTerminado) return;
        estaPausado = !estaPausado;
        Time.timeScale = estaPausado ? 0f : 1f;
        if (audioMusicaFondo != null) { if (estaPausado) audioMusicaFondo.Pause(); else audioMusicaFondo.UnPause(); }
    }
}