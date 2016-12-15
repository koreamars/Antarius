using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    public GameObject Menu1Obj;
    public GameObject Menu2Obj;
    public GameObject Menu3Obj;
    public GameObject Menu4Obj;

    public GameObject MenuField1;
    public GameObject MenuField2;
    public GameObject MenuField3;
    public GameObject MenuField4;

    // Use this for initialization
    private int _currentIdx = 0;

    private GameObject[] _menuList;

    private GameObject _mainField;
    
    void Start () {

        iTween.MoveTo(Menu1Obj, iTween.Hash("y", -10f, "time", 0f));
        iTween.MoveTo(Menu2Obj, iTween.Hash("y", -10f, "time", 0f));
        iTween.MoveTo(Menu3Obj, iTween.Hash("y", -10f, "time", 0f));
        iTween.MoveTo(Menu4Obj, iTween.Hash("y", -10f, "time", 0f));

        _menuList = new GameObject[4];
        _menuList[0] = Menu1Obj;
        _menuList[1] = Menu2Obj;
        _menuList[2] = Menu3Obj;
        _menuList[3] = Menu4Obj;

        MenuField1.SetActive(false);
        MenuField2.SetActive(false);
        MenuField3.SetActive(false);
        MenuField4.SetActive(false);

    }

    public void Open()
    {
        iTween.MoveTo(Menu1Obj, iTween.Hash("y", 2f, "time", 0.5f));
        iTween.MoveTo(Menu2Obj, iTween.Hash("y", 0.65f, "time", 0.5f, "delay", 0.1f));
        iTween.MoveTo(Menu3Obj, iTween.Hash("y", -0.65f, "time", 0.5f, "delay", 0.2f));
        iTween.MoveTo(Menu4Obj, iTween.Hash("y", -2f, "time", 0.5f, "delay", 0.3f));
    }

    public void SetMainMenu(int index)
    {
        if(_currentIdx > 0)
        {
            _currentIdx = 0;
            iTween.MoveTo(Menu1Obj, iTween.Hash("y", 2f));
            iTween.MoveTo(Menu2Obj, iTween.Hash("y", 0.65f));
            iTween.MoveTo(Menu3Obj, iTween.Hash("y", -0.65f));
            iTween.MoveTo(Menu4Obj, iTween.Hash("y", -2f));

            MenuField1.SetActive(false);
            MenuField2.SetActive(false);
            MenuField3.SetActive(false);
            MenuField4.SetActive(false);

            _mainField = null;

        } else
        {
            int idx = 1;
            foreach(GameObject menuObj in _menuList)
            {
                if(idx == index)
                {
                    iTween.MoveTo(menuObj, iTween.Hash("y", 4f, "oncomplete", "ShowMainField", "oncompletetarget", this.gameObject));
                } else
                {
                    if(idx > index)
                    {
                        iTween.MoveTo(menuObj, iTween.Hash("y", -10f));
                    } else
                    {
                        iTween.MoveTo(menuObj, iTween.Hash("y", 6f));
                    }
                }
                idx++;
            }

            _currentIdx = index;

            
        }
        
    }

    private void ShowMainField()
    {
        MenuField1.SetActive(false);
        MenuField2.SetActive(false);
        MenuField3.SetActive(false);
        MenuField4.SetActive(false);

        switch (_currentIdx)
        {
            case 1:
                MenuField1.SetActive(true);
                break;
            case 2:
                MenuField2.SetActive(true);
                break;
            case 3:
                MenuField3.SetActive(true);
                break;
            case 4:
                MenuField4.SetActive(true);
                break;
        }

        if (_mainField == null) return;
        /*
        _mainField.transform.parent = this.transform.parent;
        RectTransform recttransform = _mainField.transform as RectTransform;
        recttransform.localScale = new Vector3(1f, 1f, 1f);
        recttransform.localPosition = new Vector3(0f, 0f, 0f);
        recttransform.sizeDelta = new Vector2(0f, 0f);
        */

    }
}
