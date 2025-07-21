using UnityEngine;

public class Ignot : MonoBehaviour
{
    [SerializeField] GameObject _light;
    [SerializeField] GameObject _reach;
    [SerializeField] GameObject _lever;

    private void Awake()
    {
        _light.SetActive(false);
        _reach.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("lever"))
        {
            _light.SetActive(true);
            _reach.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("lever"))
        {
            _light.SetActive(false);
            _reach.SetActive(false);
        }
    }
}
