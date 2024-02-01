using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class ClothWeightMapper : MonoBehaviour
{
    [SerializeField] private Cloth cloth;
    [SerializeField] private bool setWeights;
    [SerializeField] private AnimationCurve falloff;

    ClothSkinningCoefficient[] newConstraints;

    private void Update() 
    {
        if(!cloth) return;
        if(Application.isPlaying) return;

        float minYValue = 0f, maxYValue = 0f;


        newConstraints = cloth.coefficients;

        if (setWeights)
        {
            for (int i = 0; i < cloth.vertices.Length; i++)
            {
                minYValue = Mathf.Min(cloth.vertices[i].y, minYValue);
                maxYValue = Mathf.Max(cloth.vertices[i].y, maxYValue);
            }
            
            Debug.Log(minYValue);
            Debug.Log(maxYValue);

            for (int i = 0; i < cloth.vertices.Length; i++)
            {
                newConstraints[i].maxDistance = falloff.Evaluate(1 - cloth.vertices[i].y / maxYValue);
            }

            cloth.coefficients = newConstraints;
            setWeights = false;
        }

    }
}
