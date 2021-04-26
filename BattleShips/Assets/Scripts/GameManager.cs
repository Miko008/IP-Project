using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Boats;
    private Controller controller;
    private Bomber bomber;
    bool bombing = false;
    void Start()
    {
        controller = gameObject.GetComponent<Controller>();
        bomber = gameObject.GetComponent<Bomber>();
        controller.enabled = true;
        bomber.enabled = false;
    }

    void Update()
    {
        if(!bombing)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
                controller.StartPlacing(Boats[0]);
            if(Input.GetKeyDown(KeyCode.Alpha2))
                controller.StartPlacing(Boats[1]);
            if(Input.GetKeyDown(KeyCode.Alpha3))
                controller.StartPlacing(Boats[2]);
            if(Input.GetKeyDown(KeyCode.Alpha4))
                controller.StartPlacing(Boats[3]);
            if(Input.GetKeyDown(KeyCode.Alpha5))
                controller.StartPlacing(Boats[4]);
        }



        if(Input.GetKeyDown(KeyCode.Tab)){
            controller.enabled = !controller.enabled;
            bomber.enabled = !bomber.enabled;
            bombing = !bombing;
        }

    }
}
