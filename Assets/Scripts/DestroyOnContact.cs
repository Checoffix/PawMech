using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public void DestroyObj(GameObject gameObject)
    {
        Debug.Log("Deleted");
        Destroy(gameObject);
    }
}
