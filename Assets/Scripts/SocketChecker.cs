using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketChecker : MonoBehaviour
{
    [SerializeField] GameObject chemical;
    [SerializeField] GameObject rope1;
    [SerializeField] Transform targetRope1;
    [SerializeField] Transform targetRope2;
    [SerializeField] GameObject rope2;
    [SerializeField] XRSocketInteractor XRSocket1;
    [SerializeField] XRSocketInteractor XRSocket2;
    [SerializeField] XRSocketInteractor XRSocket3;

    private bool socket1;
    private bool socket2;
    private bool socket3;
    private bool allSocketAdd;

    private void Awake()
    {
        socket1 = false;
        socket2 = false;
        socket3 = false;
        allSocketAdd = false;
    }

    private void Update()
    {
        if(allSocketAdd == true)
        {
            rope1.transform.position = Vector3.Lerp(rope1.transform.position, targetRope1.transform.position, Time.deltaTime * 10f);
            rope2.transform.position = Vector3.Lerp(rope2.transform.position, targetRope2.transform.position, Time.deltaTime * 10f);
            if (Vector3.Distance(rope2.transform.position, targetRope2.transform.position) < 0.001f)
            {
                chemical.transform.SetParent(null);
                chemical.GetComponent<Rigidbody>().isKinematic = false;
                chemical.GetComponent<XRGrabInteractable>().enabled = true;
            }
        }
    }
    public void SocketOne()
    {
        socket1 = true;
        if (XRSocket1 != null)
        {
            if (XRSocket1.interactablesSelected != null)
            {
                var interactable = XRSocket1.interactablesSelected[0];
                GameObject obj = interactable.transform.gameObject;
                Debug.Log($"Socket '{XRSocket1.name}' is currently occupied by '{obj.name}'");
                obj.gameObject.GetComponentInChildren<Collider>().enabled = false;
            }
        }
    }

    public void SocketTwo() 
    { 
        socket2 = true;
        if (XRSocket2 != null)
        {
            if (XRSocket2.interactablesSelected != null)
            {
                var interactable2 = XRSocket2.interactablesSelected[0];
                GameObject obj2 = interactable2.transform.gameObject;
                Debug.Log($"Socket '{XRSocket2.name}' is currently occupied by '{obj2.name}'");
                obj2.gameObject.GetComponentInChildren<Collider>().enabled = false;
            }
        }
    }

    public void SocketThree() 
    {
        socket3 = true;
        if (XRSocket3 != null)
        {
            if (XRSocket3.interactablesSelected != null)
            {
                var interactable3 = XRSocket3.interactablesSelected[0];
                GameObject obj3 = interactable3.transform.gameObject;
                Debug.Log($"Socket '{XRSocket3.name}' is currently occupied by '{obj3.name}'");
                obj3.gameObject.GetComponentInChildren<Collider>().enabled = false;
            }
        }
    }

    public void AllSocketAdded()
    {
        if(socket1 == true && socket2 == true && socket3 == true)
        {
            Debug.Log("All Socket Added");
            allSocketAdd = true;
        }
    }
}
