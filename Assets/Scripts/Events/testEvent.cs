using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class testEvent : MonoBehaviour
{
    //private Action<EventParam> someListener1;
    //private Action<EventParam> someListener2;
    //private Action<EventParam> someListener3;
    private event Action someListener1;
    private event Action someListener2;
    private event Action someListener3;

    void Awake()
    {
        //someListener1 = new Action<EventParam>(SomeFunction);
        //someListener2 = new Action<EventParam>(SomeOtherFunction);
        //someListener3 = new Action<EventParam>(SomeThirdFunction);
        someListener1 = new Action(SomeFunction);
        someListener2 = new Action(SomeOtherFunction);
        someListener3 = new Action(SomeThirdFunction);

        StartCoroutine(invokeTest());
    }

    IEnumerator invokeTest()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.5f);

        while (true)
        {
            yield return waitTime;
            EventManager.RaiseEvent("test");
            yield return waitTime;
            EventManager.RaiseEvent("Spawn");
            yield return waitTime;
            EventManager.RaiseEvent("Destroy");
        }
    }

    void OnEnable()
    {
        //Register With Action variable
        EventManager.StartListening("test", someListener1);
        EventManager.StartListening("Spawn", someListener2);
        EventManager.StartListening("Destroy", someListener3);

        //OR Register Directly to function
        EventManager.StartListening("test", SomeFunction);
        EventManager.StartListening("Spawn", SomeOtherFunction);
        EventManager.StartListening("Destroy", SomeThirdFunction);
    }

    void OnDisable()
    {
        //Un-Register With Action variable
        EventManager.StopListening("test", someListener1);
        EventManager.StopListening("Spawn", someListener2);
        EventManager.StopListening("Destroy", someListener3);
     
    }

    void SomeFunction()
    {
        Debug.Log("Some Function was called!");
    }

    void SomeOtherFunction()
    {
        Debug.Log("Some Other Function was called!");
    }

    void SomeThirdFunction()
    {
        Debug.Log("Some Third Function was called!");
    }
}