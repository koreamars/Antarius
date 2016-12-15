using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using CielaSpike.Unity.Barcode;
using System.Threading;

public class QRPopup : MonoBehaviour {

    public GameObject ScanScreenView;
    public Text LogTxt;

    WebCamTexture cameraTexture;

    Material cameraMat;
    GameObject plane;

    WebCamDecoder decoder;

    IBarcodeEncoder qrEncoder, pdf417Encoder;

    GUIContent qrImage = new GUIContent();
    GUIContent pdf417Image = new GUIContent();

    GUIContent resultString = new GUIContent();

    Vector2 scroll = Vector2.zero;

    IEnumerator Start()
    {
        cameraMat = ScanScreenView.GetComponent<Image>().material;
        decoder = this.GetComponent<WebCamDecoder>();

        // get encoders;
        qrEncoder = Barcode.GetEncoder(BarcodeType.QrCode, new QrCodeEncodeOptions()
        {
            ECLevel = QrCodeErrorCorrectionLevel.H
        });

        pdf417Encoder = Barcode.GetEncoder(BarcodeType.Pdf417);

        qrEncoder.Options.Margin = 1;
        pdf417Encoder.Options.Margin = 2;

        // init web cam;
        if (Application.platform == RuntimePlatform.OSXWebPlayer ||
            Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        }

        var devices = WebCamTexture.devices;
        var deviceName = devices[0].name;
        cameraTexture = new WebCamTexture(deviceName, 1920, 1080);
        cameraTexture.Play();

        // start decoding;
        yield return StartCoroutine(decoder.StartDecoding(cameraTexture));

        cameraMat.mainTexture = cameraTexture;

        // adjust texture orientation;
        plane.transform.rotation = plane.transform.rotation *
            Quaternion.AngleAxis(cameraTexture.videoRotationAngle, Vector3.up);


    }

    void Update()
    {
        DecodeResult result = decoder.Result;
        bool decoded = false;
        if (result.Success && resultString.text != result.Text)
        {
            resultString.text = result.Text;
            decoded = true;
            Debug.Log(string.Format(
                "Decoded: [{0}]{1}", result.BarcodeType, result.Text));
        }
    }


    public void PopupClose()
    {
        SystemManager.GetInstance().QRCodePopupClose();
    }
}
