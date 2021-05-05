using UnityEngine;
using UnityEngine.EventSystems;

public class UserInput : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public static bool chocolateDown = false;
    public static bool vanilliaDown = false;
    GameObject vanilliaSyphon, chocolateSyphon;

    void Start()
    {
        vanilliaSyphon = GameObject.FindGameObjectWithTag("vanilliaSyphon");
        chocolateSyphon = GameObject.FindGameObjectWithTag("chocolateSyphon");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name.Equals("Chocolate"))
            chocolateDown = true;
        else
            vanilliaDown = true;
    }
    void Update()
    {
        if (chocolateDown)
            SyphonDown(chocolateSyphon);
        else
            SyphonUp(chocolateSyphon);

        if (vanilliaDown)
            SyphonDown(vanilliaSyphon);
        else
            SyphonUp(vanilliaSyphon);

    }
    void SyphonDown( GameObject syphon)
    {

        if (syphon.transform.rotation.eulerAngles.x > 320|| syphon.transform.rotation.eulerAngles.x<=0)
            syphon.transform.Rotate(-1, 0, 0);

    }
    void SyphonUp(GameObject syphon)
    {
        if (syphon.transform.rotation.eulerAngles.x > 0)
            syphon.transform.Rotate(1, 0, 0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        chocolateDown = false;
        vanilliaDown = false;
        CreamGenerator.move = true;  
    }
}
