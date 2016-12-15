using UnityEngine;
using System.Collections;

public class SystemManager : MonoBehaviour {

    private static SystemManager instance;
    private static GameObject container;
    public static SystemManager GetInstance()
    {
        if (!instance)
        {
            container = new GameObject();
            container.name = "SystemManager";
            instance = container.AddComponent(typeof(SystemManager)) as SystemManager;
        }
        return instance;
    }

    private GameObject _QRCodePopObj = null;

    public void QRCodePopupOpen()
    {
        if(_QRCodePopObj == null)
        {
            _QRCodePopObj = Instantiate(Resources.Load("prefab/QRPopup", typeof(GameObject))) as GameObject;

            GameObject cavasObj = GameObject.Find("Canvas").gameObject;
            _QRCodePopObj.transform.parent = cavasObj.transform;

            RectTransform recttransform = _QRCodePopObj.transform as RectTransform;
            recttransform.localScale = new Vector3(1f, 1f, 1f);
            recttransform.localPosition = new Vector3(0f, 0f, 0f);
            recttransform.offsetMax = new Vector2(0, 0);
            recttransform.offsetMin = new Vector2(0, 0);
        }
    }

    public void QRCodePopupClose()
    {
        if (_QRCodePopObj != null)
        {
            Destroy(_QRCodePopObj);
            _QRCodePopObj = null;

        }
    }
}
