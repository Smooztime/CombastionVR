using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GameEndCheck : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor chemSocket1;
    [SerializeField] private XRSocketInteractor chemSocket2;

    private bool socket1;
    private bool socket2;
    private bool allChemAdded;

    private void Awake()
    {
        socket1 = false;
        socket2 = false;
        allChemAdded = false;
    }

    public void FirstChemAdded()
    {
        socket1 = true;
        if (chemSocket1 != null)
        {
            var interactable = chemSocket1.interactablesSelected[0];
            GameObject obj = interactable.transform.gameObject;
            Debug.Log($"Socket '{chemSocket1.name}' is currently occupied by '{obj.name}'");
            obj.gameObject.GetComponentInChildren<Collider>().enabled = false;
        }   
    }

    public void SecondChemAdded()
    {
        socket2 = true;
        if(chemSocket2 != null)
        {
            var interactable2 = chemSocket2.interactablesSelected[0];
            GameObject obj2 = interactable2.transform.gameObject;
            Debug.Log($"Socket '{chemSocket2.name}' is currently occupied by '{obj2.name}'");
            obj2.gameObject.GetComponentInChildren<Collider>().enabled = false;
        }
    }

    public void EndGame()
    {
        if(socket1 == true &&  socket2 == true)
        {
            allChemAdded = true;
        }

        if(allChemAdded == true)
        {
            Debug.Log("GameEnd");
        }
        else
        {
            Debug.Log("Another one");
        }
    }
}
