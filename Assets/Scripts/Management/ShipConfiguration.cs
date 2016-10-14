using UnityEngine;
using System.Collections;

public class ShipConfiguration : MonoBehaviour {

    public GameObject Chassis;
    public GameObject WingL;
    public GameObject WingR;
    public GameObject Cockpit;

    Transform shipParent;

    //SubComponent Transform variable holders. These will be changed depending on what Chassis is selected
    Transform cockpitPosition;
    Transform wingLPosition;
    Transform wingRPosition;

    public float lerpSpeed;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        shipParent = GameObject.Find("ShipParent").transform;
        //Instantiate all Default ship components and connect them to their proper transforms
        //Chassis
        Chassis = Instantiate(Chassis, shipParent.transform.position, shipParent.rotation) as GameObject;
        Chassis.transform.parent = shipParent.transform;
        //Cockpit
        cockpitPosition = Chassis.transform.Find("Cockpit_GO").transform;
        Cockpit = Instantiate(Cockpit, cockpitPosition.position, cockpitPosition.rotation) as GameObject;
        Cockpit.transform.parent = shipParent.transform;
        //Wings
        wingLPosition = Chassis.transform.Find("Wing_L_GO").transform;
        WingL = Instantiate(WingL, wingLPosition.position, wingLPosition.rotation) as GameObject;
        WingL.transform.parent = shipParent.transform;
        wingRPosition = Chassis.transform.Find("Wing_R_GO").transform;
        WingR = Instantiate(WingR, wingRPosition.position, wingRPosition.rotation) as GameObject;
        WingR.transform.parent = shipParent.transform;
    }

    void Update()
    {
        //Lerp the components to their destinations on the Chassis
        //Cockpit
        Cockpit.transform.position = Vector3.Lerp(Cockpit.transform.position, cockpitPosition.position, lerpSpeed * Time.deltaTime);
        Cockpit.transform.rotation = Quaternion.Lerp(Cockpit.transform.rotation, cockpitPosition.rotation, lerpSpeed * Time.deltaTime);
        //Wings
        WingL.transform.position = Vector3.Lerp(WingL.transform.position, wingLPosition.position, lerpSpeed * Time.deltaTime);
        WingL.transform.rotation = Quaternion.Lerp(WingL.transform.rotation, wingLPosition.rotation, lerpSpeed * Time.deltaTime);
        WingR.transform.position = Vector3.Lerp(WingR.transform.position, wingRPosition.position, lerpSpeed * Time.deltaTime);
        WingR.transform.rotation = Quaternion.Lerp(WingR.transform.rotation, wingRPosition.rotation, lerpSpeed * Time.deltaTime);
    }

    public void ReconfigureShip(string componentName)
    {
        Destroy(Chassis);
        Chassis = Instantiate(Resources.Load(componentName), shipParent.transform.position, shipParent.rotation) as GameObject;
        Chassis.transform.parent = shipParent.transform;
        cockpitPosition = Chassis.transform.Find("Cockpit_GO").transform;
        wingLPosition = Chassis.transform.Find("Wing_L_GO").transform;
        wingRPosition = Chassis.transform.Find("Wing_R_GO").transform;
    }

    public void ReconfigureWings(string newLWing, string newRWing)
    {
        Destroy(WingL);
        WingL = Instantiate(Resources.Load(newLWing), wingLPosition.position, wingLPosition.rotation) as GameObject;
        Destroy(WingR);
        WingR = Instantiate(Resources.Load(newRWing), wingRPosition.position, wingRPosition.rotation) as GameObject;

    }

    public void ReconfigureCockpit(string newCockpit)
    {
        Destroy(Cockpit);
        Cockpit = Instantiate(Resources.Load(newCockpit), cockpitPosition.position, cockpitPosition.rotation) as GameObject;
    }

}
