using UnityEngine;

[ExecuteInEditMode]
public class ApplyCurvedWorld : MonoBehaviour
{
    [SerializeField]
    public Shader replacementShader;

    [Range(0f, 0.002f)]
    public float curvature = 0.0005f;

    private static readonly int CurvatureID = Shader.PropertyToID("_Curvature");
    private Camera cam;

    private void OnEnable()
    {
        cam = GetComponent<Camera>();
        if (replacementShader != null)
        {
            cam.SetReplacementShader(replacementShader, "RenderType");
        }
    }

    void Update()
    {
        Shader.SetGlobalFloat(CurvatureID, curvature);
    }

    private void OnDisable()
    {
        cam.ResetReplacementShader();
    }
}