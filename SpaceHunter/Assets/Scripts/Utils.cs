using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    //======================= Methods for working with materials =======================\\
    
    // Returns a list of all materials in the given game object and its children
    static public Material[] GetAllMaterials(GameObject gameObject)
    {
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();

        List<Material> materials = new List<Material>();
        foreach (Renderer rend in renderers)
        {
            materials.Add(rend.material);
        }

        return materials.ToArray();
    }
}
