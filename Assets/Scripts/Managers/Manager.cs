using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager m_instance;
    private static Manager Instance { get { Init(); return m_instance; } }


    private SoundManager m_sound = new SoundManager();
    private ResourceManager m_resource = new ResourceManager();
    private UI_Manager m_UI = new UI_Manager();
    private SceneManagerEx m_Scene = new SceneManagerEx();
    private PoolManager m_Pool = new PoolManager();

    public static SoundManager Sound_Instance  { get{ return Instance.m_sound; } }
    public static ResourceManager Resource_Instance { get { return Instance.m_resource; } }
    public static UI_Manager UI_Instance { get { return Instance.m_UI; } }
    public static SceneManagerEx Scene_Instance { get { return Instance.m_Scene; } }
    public static PoolManager Pool_Instance { get { return Instance.m_Pool; } }

    private static void Init()
    {
        if(m_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject() { name = "@Managers" };
                go.AddComponent<Manager>();
            }
            DontDestroyOnLoad(go);
            m_instance = go.GetComponent<Manager>();
            Sound_Instance.init();
            Pool_Instance.Init();
        }
    }

    public static void Clear()
    {
        Sound_Instance.Clear();
        Scene_Instance.Clear();
        Pool_Instance.Clear();
    }

}
