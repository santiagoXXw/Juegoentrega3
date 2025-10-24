using UnityEngine;
using UnityEngine.UI;

public class ItemAutoCounter : MonoBehaviour
{
    [Header("flor")]
    public Image[] floresIcons;
    private int totalFlores;
    private int floresRestantes;

    [Header("Chocolates")]
    public Image[] chocolatesIcons;
    private int totalChocolates;
    private int chocolatesRestantes;

    [Header("Carta final")]
    public GameObject cartaFinal; // Objeto de la carta (debe estar desactivado al inicio)

    [Header("Colores")]
    public Color colorObtenido = Color.white;
    public Color colorFaltante = new Color(1f, 1f, 1f, 0.3f);

    private bool cartaMostrada = false;

    void Start()
    {
        // Contamos los objetos existentes al inicio
        totalFlores = GameObject.FindGameObjectsWithTag("Flor").Length;
        totalChocolates = GameObject.FindGameObjectsWithTag("Chocolate").Length;
        floresRestantes = totalFlores;
        chocolatesRestantes = totalChocolates;

        if (cartaFinal != null)
            cartaFinal.SetActive(false); // Asegura que esté oculta al principio

        ActualizarUI();
    }

    void Update()
    {
        // Contar en tiempo real los objetos restantes
        int floresActuales = GameObject.FindGameObjectsWithTag("Flor").Length;
        int chocolatesActuales = GameObject.FindGameObjectsWithTag("Chocolate").Length;

        // Si hay un cambio, actualizamos la UI
        if (floresActuales != floresRestantes || chocolatesActuales != chocolatesRestantes)
        {
            floresRestantes = floresActuales;
            chocolatesRestantes = chocolatesActuales;
            ActualizarUI();
        }

        // Si ya no quedan objetos y la carta no se ha mostrado
        if (!cartaMostrada && floresRestantes == 0 && chocolatesRestantes == 0)
        {
            MostrarCartaFinal();
        }
    }

    private void ActualizarUI()
    {
        int floresRecogidas = totalFlores - floresRestantes;
        int chocolatesRecogidos = totalChocolates - chocolatesRestantes;

        // Actualizar íconos de flores
        for (int i = 0; i < floresIcons.Length; i++)
        {
            floresIcons[i].color = (i < floresRecogidas) ? colorObtenido : colorFaltante;
        }

        // Actualizar íconos de chocolates
        for (int i = 0; i < chocolatesIcons.Length; i++)
        {
            chocolatesIcons[i].color = (i < chocolatesRecogidos) ? colorObtenido : colorFaltante;
        }
    }

    private void MostrarCartaFinal()
    {
        cartaMostrada = true;
        if (cartaFinal != null)
        {
            cartaFinal.SetActive(true);
            Debug.Log("🎴 ¡Has recogido todos los ítems! Carta final activada.");
        }
    }
}
