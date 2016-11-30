using UnityEngine;
using System.Collections;
using com.google.zxing.qrcode;			//	using to QRcode
using System.Threading;					//	using to Thread

//using com.google.zxing.qrcode;

public class QRcodeScript : MonoBehaviour {
	
	public Texture2D nullTexture2D;	
	public Rect ScreenPropotion;
	
	public bool isFrontCamera = false;
	public WebCamTexture camTexture;
	public GUITexture WebCamOutTexture;

    public Quaternion baseRotation;

    public string DecodeQRText;
	public GUIText qrGuiText;
	QRCodeReader QR;
	Thread qrThread;
	
	int W, H, WxH;
	Color32[] c;
	sbyte[] d;
	int x, y, z;
	
	
	void Start () {		
		WebCamOutTexture.transform.localPosition = new Vector3(ScreenPropotion.x,ScreenPropotion.y, 0.0f);
		Vector2 tempVec2 = new Vector2(Screen.width*ScreenPropotion.width, Screen.height*ScreenPropotion.height);
		WebCamOutTexture.pixelInset = new Rect(-tempVec2.x*0.5f, -tempVec2.y*0.5f, tempVec2.x, tempVec2.y);
		
		QR = new QRCodeReader();
		Application.RequestUserAuthorization(UserAuthorization.WebCam);
		for (int cameraIndex = 0; cameraIndex < WebCamTexture.devices.Length; ++cameraIndex){
			if(isFrontCamera){
				if (WebCamTexture.devices[cameraIndex].isFrontFacing) {
	                camTexture = new WebCamTexture(WebCamTexture.devices[cameraIndex].name);
	                // Here we flip the GuiTexture by applying a localScale transformation
	                // works only in Landscape mode
	                WebCamOutTexture.transform.localScale = new Vector3(-ScreenPropotion.width*2.0f,-ScreenPropotion.height*2.0f,1);
	            }
			}
			else{
				if (!WebCamTexture.devices[cameraIndex].isFrontFacing){
	                camTexture = new WebCamTexture(WebCamTexture.devices[cameraIndex].name);
	                // Here we flip the GuiTexture by applying a localScale transformation
	                // works only in Landscape mode
	                //WebCamOutTexture.transform.localScale = new Vector3(-1,-1,1);
	            }
        	}
		}
	    OnEnable();

        WebCamOutTexture.texture = camTexture;

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = camTexture;
        baseRotation = transform.rotation;
        camTexture.Play();

        if (camTexture!=null){
			c = new Color32[camTexture.width*camTexture.height];
		}
		else{
			WebCamOutTexture.GetComponent<GUITexture>().texture = nullTexture2D;
		}
	    
		qrThread = new Thread(DecodeQR);
	    qrThread.Start();
	}
	void OnEnable () {
	    if(camTexture != null) {
	        camTexture.Play();
	        W = camTexture.width;
	        H = camTexture.height;
	        WxH = W * H;
	    }
	}
	
	void OnDisable () {
	    if(camTexture != null) {
	        camTexture.Pause();
	    }
	}
	
	void OnDestroy () {
	    qrThread.Abort();
	    if(camTexture != null) camTexture.Stop();
	}
	

	void Update () {
		if(camTexture!=null){
		    c = camTexture.GetPixels32();
			qrGuiText.text = DecodeQRText + "-" + camTexture.videoRotationAngle;

            transform.rotation = baseRotation * Quaternion.AngleAxis(camTexture.videoRotationAngle, Vector3.up);
        }
	}
	
	void DecodeQR () {
	    while(true) {
	        try {
	            d = new sbyte[WxH];
	            z = 0;
	            for(y = H - 1; y >= 0; y--) { // flip
	                for(x = 0; x < W; x++) {
	                    d[z++] = (sbyte)(((int)c[y * W + x].r)<<16 | ((int)c[y * W + x].g)<<8 | ((int)c[y * W + x].b));
	                }
	            }
				DecodeQRText = QR.decode(d, W, H).Text;
			}
	        catch {
	            continue;
	        }
	    }
	}
}
