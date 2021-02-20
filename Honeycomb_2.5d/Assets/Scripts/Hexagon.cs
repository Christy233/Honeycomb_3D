using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    Hexagon father = null;
    public Hexagon[] neighbors = new Hexagon[6];
    public bool isPass = true;
    public bool inUse = false;
    private float gValue = 999f;
    private float hValue = 999f;
    
    public void Reset()
    {

    }
    public void HexagonInUse()
    {
        

    }
    public Hexagon[] GetNeighborList()
    {
        return neighbors;
    }

    public void SetFatherHexagon(Hexagon f)
    {
        father = f;
    }

    public Hexagon GetFatherHexagon()
    {
        return father;
    }
    //able to pass
    public void SetCanPass(bool f)
    {
        isPass = f;
    }

    public bool CanPass()
    {
        return isPass;
    }

    public float ComputeGValue(Hexagon hex)
    {
        return 1f;
    }

    public void SetgValue(float v)
    {
        gValue = v;
    }

    public float GetgValue()
    {
        return gValue;
    }

    public void SethValue(float v)
    {
        hValue = v;
    }

    public float GethValue()
    {
        return hValue;
    }

    public float ComputeHValue(Hexagon hex)
    {
        return Vector3.Distance(transform.position, hex.transform.position);
    }
}
