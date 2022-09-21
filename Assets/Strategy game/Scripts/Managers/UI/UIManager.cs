using System.Collections;
using System.Collections.Generic;
using AStar._Scripts.Grid;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject Information;
    public Text InformationText;
    public Image InformationImage;
    void Awake() => Instance = this;

    // Start is called before the first frame update
    void Start()=>  EventManager.Instance.UIActivation += InformationUI;
    

 
    public void InformationUI(GameObject obj)
    {
        if (obj.tag == "Barracks")
        {
            if (!Information.activeInHierarchy)
            {
                Information.SetActive(true);
                InformationImage.color = obj.GetComponent<Barracks>().image.color;
                InformationText.text =obj.GetComponent<Barracks>().itemName+" "+obj.GetComponent<Barracks>().Description;
            }
            else
            {
                Information.SetActive(false);
                ProductionManager.Instance.SelectedBuilding = null;
            }


        }
    }
 
    // Update is called once per frame
    void Update()
    {

    }
}
