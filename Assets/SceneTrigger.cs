using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private string loadName;
    [SerializeField] private string unloadName;

    private void OnTriggerEnter(Collider other)
    {
        if (loadName != "")
            SceneSwitcher.Instance.LoadScene(loadName);
        if(unloadName != "")
            SceneSwitcher.Instance.UnloadScene(unloadName);
    }
}
