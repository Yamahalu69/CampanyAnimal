using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EmissionAlwaysOn : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color emissionColor = Color.cyan;

    [Range(0f, 10f)]
    public float intensity = 5f;

    private Material _material;

    void Start()
    {
        _material = GetComponent<Renderer>().material;

        _material.EnableKeyword("_EMISSION");
        SetEmission(intensity);
    }

    void SetEmission(float strength)
    {
        Color finalColor = emissionColor * strength;
        _material.SetColor("_EmissionColor", finalColor);
    }
}
