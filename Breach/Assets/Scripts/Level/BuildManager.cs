using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    #region Singleton
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one build manager in scene");
            return;
        }
        instance = this;
    }
    #endregion


    public GameObject standardTurretPrefab;

    private GameObject turretToBuild;
    private Turret turretToBuildScript;

    private void Start()
    {
        SetTurretToBuild(standardTurretPrefab);

    }

    public GameObject GetTurretToBuild()
    { 
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turretToBuild) 
    { 
        this.turretToBuild = turretToBuild;
        this.turretToBuildScript = turretToBuild.GetComponent<Turret>();
    }

    public bool CanBuild()
    {
        if (turretToBuild != null && turretToBuildScript?.cost <= Player.instance.MoneyAvailable())
        {
            return true;
        }
        return false;
    }

    public void BuildTurret(Node node)
    {
        // When this is called from the Node function, we are already checking there that we have the money.
        GameObject turret = Instantiate<GameObject>(turretToBuild, node.transform.position, node.transform.rotation);
        Player.instance.WithdrawMoney(turretToBuildScript.cost);

        node.SetTurret(turret);
    }

}
