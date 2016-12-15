using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MemberList : MonoBehaviour {

    public GameObject SearchField;
    public GameObject SearchBtn;

    private int _searchType = 0;
    private string[] _btnNames;
    private Text _searchBtnTxt;
    private ArrayList _menuList;

    void Start () {
        _btnNames = new string[3];
        _btnNames[0] = "쿠폰아이디";
        _btnNames[1] = "이름";
        _btnNames[2] = "전화번호";

        GameObject searchBtnTxtObj = SearchBtn.transform.FindChild("Text").gameObject;
        _searchBtnTxt = searchBtnTxtObj.GetComponent<Text>();
        _searchBtnTxt.text = _btnNames[_searchType];

        _menuList = new ArrayList();

        SetMenuList();
    }
	
	public void SearchBtnClick()
    {
        Debug.Log("SearchBtnClick ");

        _searchType++;
        if(_searchType >= 3)
        {
            _searchType = 0;
        }

        _searchBtnTxt.text = _btnNames[_searchType];

    }

    public void SearchData(string data)
    {
        Debug.Log("SearchData " + data);
    }

    private void SetMenuList()
    {
        GameObject menuObj;
        for(int i = 0; i < 5; i++)
        {
            menuObj = Instantiate(Resources.Load("prefab/ListBtn", typeof(GameObject))) as GameObject;

            menuObj.transform.parent = this.transform;
            string btnStr = "9382349823" + i + " | 이소주 | 010-0000-0000 | 카드미발급";
            
            ListBtn listBtn = menuObj.GetComponent<ListBtn>();
            listBtn.OnListBtnCallback = ListBtnClick;
            listBtn.btnIdx = i;
            listBtn.SetBtnData(btnStr, (((i * 60f) * -1) + 160));
        }
    }

    private void ListBtnClick(int idx)
    {
        Debug.Log("ListBtnClick " + idx);
        SystemManager.GetInstance().QRCodePopupOpen();
    }
}
