using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    void Awake() => Instance = this;
    public event Action<GameObject> UIActivation;
    public void Activation(GameObject obj)
    {
        UIActivation(obj);
    }
    public event Action UnitOnClick;
    public void unitOnClick()
    {
        UnitOnClick();
    }

}
