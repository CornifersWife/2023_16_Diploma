using UnityEngine;

public class PersistObject : MonoBehaviour {

    public static PersistObject instance = null;     
    void Awake()     {
        if (instance == null) {
            instance = this;
        }         
        else if (instance != this) {
            Destroy(gameObject);
        }          
        DontDestroyOnLoad(gameObject);     
    } 
}