using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform root { get; set; }
        Stack<Poolable> m_PoolStack = new Stack<Poolable>();
        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            root = new GameObject().transform;
            root.name = $"{original.name}_root";

            for (int i = 0; i < count; i++)
                Push(Create());

        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;
            poolable.isUsing = false;
            poolable.transform.parent = root;
            poolable.gameObject.SetActive(false);
            m_PoolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;
            if (m_PoolStack.Count > 0)
                poolable = m_PoolStack.Pop();
            else
                poolable = Create();
            poolable.gameObject.SetActive(true);

            if (parent == null)
                poolable.transform.parent = Manager.Scene_Instance.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.isUsing = true;
            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> m_pools = new Dictionary<string, Pool>();
    Transform m_root;

    public void Init()
    {
        if (m_root == null)
        {
            m_root = new GameObject { name = "@Poola_Manager" }.transform;
            DontDestroyOnLoad(m_root);
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.root.parent = m_root;
        m_pools.Add(original.name, pool);
    }

    public Poolable Pop(GameObject original, Transform parent)
    {
        if (!m_pools.ContainsKey(original.name))
            CreatePool(original);

        return m_pools[original.name].Pop(parent);
    }


    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (!m_pools.ContainsKey(name))
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }
        m_pools[name].Push(poolable);
    }

    public void Clear()
    {
        foreach (Transform tt in m_root)
            Destroy(tt);
        m_pools.Clear();
    }

    public GameObject GetOriginal(string name)
    {
        if (!m_pools.ContainsKey(name))
        {
            return null;
        }
        return m_pools[name].Original;
    }

}





























