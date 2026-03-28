using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    public string playerTag;

    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            onTriggerEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
            onTriggerExit?.Invoke();
    }

    public void DestroyGameObject(GameObject go)
    {
        Destroy(go);
    }
}
