using UnityEngine;
using TMPro;

public class SimpleFloatingText : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float lifeTime = 1.5f;

    private TextMeshPro textMesh;
    private Color textColor;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        if (textMesh != null)
        {
            textColor = textMesh.color;
        }
    }

    void Start()
    {
        // Se destruye solo tras unos segundos
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Sube hacia arriba
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Hace que siempre mire hacia la cámara principal
        if (Camera.main != null)
        {
            transform.rotation = Camera.main.transform.rotation;
        }

        // Desvanecimiento
        if (textMesh != null)
        {
            textColor.a -= Time.deltaTime / lifeTime;
            textMesh.color = textColor;
        }
    }

    public void SetMessage(string msg)
    {
        if (textMesh != null)
        {
            textMesh.text = msg;
        }
    }
}