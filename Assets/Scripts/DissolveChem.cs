using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DissolveChem : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private GameObject chemical;
    [SerializeField] private GameObject dissolveChemical;
    [SerializeField] private GameObject lid;
    [SerializeField] private float duration = 5f;

    private float lerpTime = 0f;
    private float currentValue = 0f;

    private enum State
    {
        Idle,
        DissolvingUp,
        DissolvingDown,
        Finished
    }

    private State state = State.Idle;

    private void Start()
    {
        currentValue = 0f;
        _material.SetFloat("_CutoffHeight", currentValue);
    }

    private void Update()
    {
        switch (state)
        {
            case State.DissolvingUp:
                lerpTime += Time.deltaTime;
                float upT = Mathf.Clamp01(lerpTime / duration);
                currentValue = Mathf.Lerp(0f, 2f, upT);
                _material.SetFloat("_CutoffHeight", currentValue);

                if (currentValue >= 2f - 0.01f)
                {
                    currentValue = 2f;
                    _material.SetFloat("_CutoffHeight", 2f);
                    state = State.Finished;

                    chemical.gameObject.SetActive(true);
                    dissolveChemical.gameObject.SetActive(false);
                    lid.gameObject.SetActive(false);
                    chemical.GetComponent<Rigidbody>().isKinematic = false;
                    chemical.GetComponent<XRGrabInteractable>().enabled = true;
                }
                break;

            case State.DissolvingDown:
                lerpTime += Time.deltaTime;
                float downT = Mathf.Clamp01(lerpTime / duration);
                currentValue = Mathf.Lerp(currentValue, 0f, downT);
                _material.SetFloat("_CutoffHeight", currentValue);

                if (currentValue <= 0.01f)
                {
                    currentValue = 0f;
                    _material.SetFloat("_CutoffHeight", 0f);
                    state = State.Idle;
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("rope"))
        {
            if (state == State.Finished)
                return;
            state = State.DissolvingUp;
            lerpTime = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("rope"))
        {
            if (state == State.Finished)
                return;

            if (state == State.DissolvingUp || state == State.Idle)
            {
                state = State.DissolvingDown;
                lerpTime = 0f;
            }
        }
    }
}