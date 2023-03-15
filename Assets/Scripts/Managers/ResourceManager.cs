using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public T Load<T>(string path)where T:UnityEngine.Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);
            GameObject go = Manager.Pool_Instance.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {

        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"There is no Prefabs in {path}");
            return null;
        }
        if (original.GetComponent<Poolable>() != null)
            return Manager.Pool_Instance.Pop(original, parent).gameObject;

        GameObject go;
        if (parent == null)
            go = Object.Instantiate(original);
        else
            go = Object.Instantiate(original, parent);

        

        int idx = go.name.IndexOf("(Clone)");
        if (idx > 0)
            go.name = go.name.Substring(0, idx);

        return go;
    }
    public void Destroy(GameObject go)
    {
        if(go== null)
        {
            Debug.Log($"There is no {go}");
            return;
        }

        if (go.GetComponent<Poolable>() != null)
        {
            Manager.Pool_Instance.Push(go.GetComponent<Poolable>());
            return;
        }

        Object.Destroy(go);

    }

}
