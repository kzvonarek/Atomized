using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBonding : MonoBehaviour
{
    // get variable(s) from AtomBonding.cs
    private AtomBonding aBscript;

    void Start()
    {
        aBscript = GetComponent<AtomBonding>();
    }

    void Update()
    {
        // break bond(s)
        if (Input.GetKeyDown(KeyCode.N) && aBscript.bonded)
        {
            aBscript.bonded = false;
        }
    }
}
