using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    int m_order = 10;
    Stack<UI_Popup> m_popupStack = new Stack<UI_Popup>();
    UI_Scene m_sceneUI = null;
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject() { name = "@UI_Root" };
            return root;
        }
       
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
            canvas.sortingOrder = (m_order++);
        else
            canvas.sortingOrder = 0;
    }


    public T ShowSceneUI<T>(Transform parent = null, string name = null) where T : UI_Scene
    {

        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = Manager.Resource_Instance.Instantiate($"UI/Scene/{name}", parent);
        T sceneUI = Util.GetOrAddComponent<T>(go);
        m_sceneUI = sceneUI;
        if (parent == null)
            go.transform.SetParent(Root.transform);
        return sceneUI;
    }


   


    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {

        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = Manager.Resource_Instance.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        m_popupStack.Push(popup);
        go.transform.SetParent(Root.transform);
        return popup;

    }

    public void ClosePeekUI(UI_Popup popup)
    {
        if (m_popupStack.Count == 0)
            return;

        if (m_popupStack.Peek() != popup)
        {
            Debug.Log("Colse Popup FAILED");
            return;
        }
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (m_popupStack.Count == 0)
            return;

        UI_Popup popup = m_popupStack.Pop();
        Manager.Resource_Instance.Destroy(popup.gameObject);
        popup = null;
        m_order--;
    }

    public void ClosePoppupAll()
    {
        while (m_popupStack.Count > 0)
            ClosePopupUI();
    }



}
