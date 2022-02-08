using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenMaskAssigner : MonoBehaviour
{
    public Material nonMasked;
    public Material masked;
    public SkinnedMeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNonMasked()
    {
        meshRenderer.material = nonMasked;
    }

    public void SetMasked()
    {
        meshRenderer.material = masked;
    }
}
