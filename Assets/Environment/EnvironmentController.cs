using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject imageToDisplayOnAwake;
    // Start is called before the first frame update
    void Start()
    {
        imageToDisplayOnAwake.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
