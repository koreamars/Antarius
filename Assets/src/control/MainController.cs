using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

    public GameObject Logo;
    public GameObject LoginField;
    public GameObject MainMenu;
    public GameObject BlackScreen;
    // Use this for initialization

    void Invoke()
    {
        //iTween.MoveTo(MainMenu, iTween.Hash("y", 1000f, "time", 0f));
    }

    void Start () {
        
        LoginField.SetActive(false);
        //MainMenu.SetActive(false);

        //iTween.MoveTo(MainMenu, iTween.Hash("y", 1000f, "time", 0f));
        iTween.ColorTo(BlackScreen, iTween.Hash("a", 0f, "time", 1.5f, "oncomplete", "ShowLoginInput", "oncompletetarget", this.gameObject));
        
    }
    
    private void ShowLoginInput()
    {
        LoginField.SetActive(true);
        BlackScreen.SetActive(false);
        MainMenu.SetActive(false);
    }

    public void SetLogin()
    {
        LoginField.SetActive(false);
        iTween.ColorTo(Logo, iTween.Hash("a", 0.25f, "time", 1f, "oncomplete", "SetMainMenu", "oncompletetarget", this.gameObject));
        iTween.ScaleTo(Logo, iTween.Hash("x", 1.3f, "y", 1.3f, "time", 2f));
    }

    private void SetMainMenu()
    {
        MainMenu.SetActive(true);
        MenuController menuController = MainMenu.GetComponent<MenuController>();
        menuController.Open();
    }

    void Update()
    {
#if UNITY_STANDALONE
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }
	
}
