using UnityEngine;

public class PatrullaGuardia : MonoBehaviour
{
    [Header("Ruta de patrullaje")]
    [SerializeField] private Transform[] puntos;

    private int indiceActual;

    public bool TienePuntos =>
        puntos != null && puntos.Length > 0;

    public Transform PuntoActual =>
        TienePuntos ? puntos[indiceActual] : null;

    public Transform PuntoMasCercano(Vector3 posicion)
    {
        if (!TienePuntos)
            return null;

        Transform masCercano = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (Transform punto in puntos)
        {
            if (punto == null)
                continue;

            float distancia =
                Vector3.Distance(posicion, punto.position);

            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                masCercano = punto;
            }
        }

        return masCercano;
    }

    public void EstablecerPuntoActual(Transform punto)
    {
        if (puntos == null)
            return;

        int indice = System.Array.IndexOf(puntos, punto);

        if (indice >= 0)
            indiceActual = indice;
    }

    public void AvanzarAlSiguiente()
    {
        if (TienePuntos)
            indiceActual = (indiceActual + 1) % puntos.Length;
    }
}
