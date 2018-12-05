using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChangeRenderMode
{
    /// <summary>
    /// 0 - Opaque. 1 - Transparent.
    /// </summary>
    /// <param name="pMaterial"></param>
    /// <param name="pMode"></param>
    public static void ChangeMode(Material pMaterial, int pMode)
    {
        switch (pMode)
        {
            case 0:
                pMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                pMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                pMaterial.SetInt("_ZWrite", 1);
                pMaterial.DisableKeyword("_ALPHATEST_ON");
                pMaterial.DisableKeyword("_ALPHABLEND_ON");
                pMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                pMaterial.renderQueue = -1;
                break;
            case 1:
                pMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                pMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                pMaterial.SetInt("_ZWrite", 0);
                pMaterial.DisableKeyword("_ALPHATEST_ON");
                pMaterial.DisableKeyword("_ALPHABLEND_ON");
                pMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                pMaterial.renderQueue = 3000;
                break;
            default:
                pMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                pMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                pMaterial.SetInt("_ZWrite", 1);
                pMaterial.DisableKeyword("_ALPHATEST_ON");
                pMaterial.DisableKeyword("_ALPHABLEND_ON");
                pMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                pMaterial.renderQueue = -1;
                break;
        }
    }
}
