using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ListBtn : MonoBehaviour {

    public delegate void ListBtnCallback(int btnIdx);
    public ListBtnCallback OnListBtnCallback = null;
    public int btnIdx = 0;

    void Start()
    {
        Debug.Log("ListBtn Start");
        
    }

    public void SetBtnData(string btnStr, float posY)
    {
        this.transform.FindChild("Text").gameObject.GetComponent<Text>().text = btnStr;

        RectTransform recttransform = this.transform as RectTransform;
        recttransform.localScale = new Vector3(1f, 1f, 1f);
        recttransform.localPosition = new Vector3(0f, posY, 0f);
        recttransform.sizeDelta = new Vector2(0f, 50f);
        recttransform.offsetMax = new Vector2(-9f, recttransform.offsetMax.y);
        recttransform.offsetMin = new Vector2(9f, recttransform.offsetMin.y);
    }

	public void ListBtnClick()
    {
        if(OnListBtnCallback != null)
        {
            OnListBtnCallback(btnIdx);
        }
    }
}
