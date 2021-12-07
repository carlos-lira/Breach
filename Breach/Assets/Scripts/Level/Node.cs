using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public bool isBuildable = true;

    private Color startColor = Color.white;
    private Color hoverColor = Color.blue;
    private Color unAvailableColor = Color.red;
    private Renderer renderer;

    private GameObject turret = null;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnMouseEnter()
    {
        if (isBuildable && BuildManager.instance.CanBuild())
        {
            renderer.material.color = hoverColor;
        }
        else
        { 
            renderer.material.color = unAvailableColor;
        }
    }

    private void OnMouseExit()
    {
        renderer.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (turret != null || !isBuildable || !BuildManager.instance.CanBuild())
        {
            Debug.Log("Cant build");
            return;
        }

        BuildManager.instance.BuildTurret(this);
        renderer.material.color = startColor;
    }

    public void SetTurret(GameObject turret)
    { 
        this.turret = turret;
        isBuildable = false;
    }
}
