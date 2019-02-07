using UnityEngine;

//Allows the properties of the crt shader to be modified

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class TVShader : MonoBehaviour
{
    public Shader shader;
    private Material _material;

    //Sliders to change properties of the crt shader
    [Range(0, 1)] public float verts_force = 0.0f;
    [Range(0, 1)] public float verts_force_2 = 0.0f;
    [Range(-3, 20)] public float contrast = 0.0f;
    [Range(-200, 200)] public float brightness = 0.0f;

    //Creates a material for the crt shader
    protected Material material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        }
    }

    //Displays crt shader when camera renders
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (shader == null) return;
        Material mat = material;
        mat.SetFloat("_VertsColor", 1 - verts_force);
        mat.SetFloat("_VertsColor2", 1 - verts_force_2);
        mat.SetFloat("_Contrast", contrast);
        mat.SetFloat("_Br", brightness);
        Graphics.Blit(source, destination, mat);
    }

    void OnDisable()
    {
        if (_material)
        {
            DestroyImmediate(_material);
        }
    }
}