using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Publisher : MonoBehaviour
{
    public static Publisher Instance;
    public delegate void PublishEvent(int damage);

    public static event PublishEvent Publish;

    void Awake()
    {
        Instance = this;
    }

    public void PublishTheEvent(int damage)
    {
        if (Publish!=null)
        {
            Publish(damage);
        }
    }
}
