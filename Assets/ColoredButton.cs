using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredButton : MonoBehaviour
{
    [SerializeField] CourroneManager manager;
    [SerializeField] CourroneManager.COLORS Color;

    public void AddColor()
    {
        manager.AddColor(Color);
    }
}
