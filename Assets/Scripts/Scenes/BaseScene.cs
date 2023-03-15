using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{

    public Define.Scene SceneType { get; protected set; } = Define.Scene.UnKnown;


    protected virtual void Init()
    {
        Object go = GameObject.FindObjectOfType((typeof(EventSystem)));
        if (go == null)
            Manager.Resource_Instance.Instantiate("UI/EventSystem").name = "@EventSystem";
    }


    public abstract void Clear();

}
