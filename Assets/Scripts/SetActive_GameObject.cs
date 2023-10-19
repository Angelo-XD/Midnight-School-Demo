using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive_GameObject : MonoBehaviour
{
    Animator Anim;
    int Update;
    private void Start()
    {
        Update = Animator.StringToHash("Update");
        Anim = GetComponent<Animator>();
    }

    public void Off()
    {
        Anim.SetBool(Update, false);
    }
}
