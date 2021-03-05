using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexagonManager : MonoBehaviour
{
    Dictionary<string, Hexagon> hexagons = new Dictionary<string, Hexagon>();
    List<Hexagon> openList = new List<Hexagon>();
    List<Hexagon> closeList = new List<Hexagon>();

    public void Start()
    {
        foreach (Transform child in transform)
        {
            hexagons.Add(child.name, child.GetComponent<Hexagon>());
        }
    }

    public List<Hexagon> SearchRoute(Hexagon startHex, Hexagon targetHex)
    {
        Hexagon nowHexagon = startHex;
        nowHexagon.Reset();

        openList.Add(nowHexagon);
        bool find = false;
        while (!find)
        {
            openList.Remove(nowHexagon);
            closeList.Add(nowHexagon);
            Hexagon[] neighbors = nowHexagon.GetNeighborList();
            foreach (Hexagon neighbor in neighbors)
            {
                if (neighbor == null) continue;

                if (neighbor == targetHex)
                {
                    find = true;
                    neighbor.SetFatherHexagon(nowHexagon);
                }
                if (closeList.Contains(neighbor) || !neighbor.CanPass())
                {
                    continue;
                }

                if (openList.Contains(neighbor))//if node inside the openlist 
                {
                    float assueGValue = neighbor.ComputeGValue(nowHexagon) + nowHexagon.GetgValue();
                    if (assueGValue < neighbor.GetgValue())
                    {
                        openList.Remove(neighbor);
                        neighbor.SetgValue(assueGValue);
                        openList.Add(neighbor);
                    }
                }
                else
                {
                    neighbor.SethValue(neighbor.ComputeHValue(targetHex));
                    neighbor.SetgValue(neighbor.ComputeGValue(nowHexagon) + nowHexagon.GetgValue());
                    openList.Add(neighbor);
                    neighbor.SetFatherHexagon(nowHexagon);
                }
            }
            if (openList.Count <= 0)
                break;
            else
                nowHexagon = openList[0];
        }
        openList.Clear();
        closeList.Clear();

        List<Hexagon> route = new List<Hexagon>();
        if (find)
        {
            Hexagon hex = targetHex;
            while (hex != startHex)
            {
                route.Add(hex);
                Hexagon fatherHex = hex.GetFatherHexagon();
                hex = fatherHex;
            }
            route.Add(hex);
        }
        route.Reverse();
        return route;
    }

}