using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform FollowObj;

    public float Speed = 3f;


    #region Singleton
    public static Follow Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    #endregion
    // Update is called once per frame
    void Update()
    {
        //transform.position = FollowObj.position;
        transform.position = Vector3.Slerp(transform.position, FollowObj.position, Speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, FollowObj.rotation, Speed * Time.deltaTime);
    }
}
