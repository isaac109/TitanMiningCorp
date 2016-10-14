using UnityEngine;
using System.Collections;

public class ShipComponentMenu : MonoBehaviour
{
    public enum ComponentType { Chassis, Wings, Cockpit}
    public ComponentType myComponentType;

    public string Component;
    public string secondaryComponent; //Only used if the object is Wings, this contains the 'Right' wing
    public ShipConfiguration shipConfigureScript;


    void Start()
    {
        shipConfigureScript = GameObject.Find("ShipConfigurationManager").GetComponent<ShipConfiguration>();
    }

    void OnMouseDown()
    {
        if(myComponentType == ComponentType.Chassis)
        {
            shipConfigureScript.ReconfigureShip(Component);
        }
        else if (myComponentType == ComponentType.Wings)
        {
            shipConfigureScript.ReconfigureWings(Component, secondaryComponent);
        }
        else if (myComponentType == ComponentType.Cockpit)
        {
            shipConfigureScript.ReconfigureCockpit(Component);
        }
    }
}
