using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Turret Prefabs")]
    [SerializeField] GameObject xbowTurretPrefab;
    [SerializeField] GameObject fireTurretPrefab;
    [SerializeField] GameObject laserTurretPrefab;
    [SerializeField] GameObject slowTurretPrefab;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseTurret(int turretType)
    {

        switch ((TurretEnum)turretType)
        {
            case TurretEnum.XBOW:
                PurchaseXBow();
                break;
            case TurretEnum.FIRE:
                PurchaseFire();
                break;
            case TurretEnum.LASER:
                PurchaseLaser();
                break;
            case TurretEnum.SLOW:
                PurchaseSlow();
                break;
            default:
                Debug.LogError("Invalid turret selected");
                break;
        }
    }

    private void PurchaseXBow()
    {
        Debug.Log("XBOW Selected");
        buildManager.SetTurretToBuild(xbowTurretPrefab);
    }

    private void PurchaseFire()
    {
        Debug.Log("Fire Selected");
        buildManager.SetTurretToBuild(fireTurretPrefab);
    }

    private void PurchaseLaser()
    {
        Debug.Log("Mele Selected");
        buildManager.SetTurretToBuild(laserTurretPrefab);
    }

    private void PurchaseSlow()
    {
        Debug.Log("Slow Selected");
        buildManager.SetTurretToBuild(slowTurretPrefab);
    }
}
