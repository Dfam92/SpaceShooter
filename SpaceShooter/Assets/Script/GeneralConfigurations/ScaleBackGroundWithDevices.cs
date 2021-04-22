using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackGroundWithDevices : MonoBehaviour
{
    public GameObject backgroundImage;
    public Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        scaleBackgroundImagefitScreenSize();
    }
    
    private void scaleBackgroundImagefitScreenSize()
    {
        // Step 1 : Get Device Screen Aspect

        Vector2 deviceScreenResolution = new Vector2(Screen.width, Screen.height);
        //print(deviceScreenResolution);

        float scrHeight = Screen.height;
        float scrWidth = Screen.width;

        float DEVICE_SCREEN_ASPECT = scrWidth / scrHeight;
        //print("DEVICCE_SCREEN_ASPECT: " + DEVICE_SCREEN_ASPECT.ToString());
        //Step 2 : Set Main Camera Aspect = Device Aspect

        mainCam.aspect = DEVICE_SCREEN_ASPECT;
        // Step 3: Scale Background Image to fit With Camera Size
        float camHeight = 100.0f * mainCam.orthographicSize * 2.0f;
        float camWidht = camHeight * DEVICE_SCREEN_ASPECT;
        //print("canHeight: " + camHeight.ToString());
        //print("camWidht: " + camWidht.ToString());

        //Get Background Image Size
        SpriteRenderer backgroundImageSR = backgroundImage.GetComponent<SpriteRenderer>();
        float bgImgH = backgroundImageSR.sprite.rect.height;
        float bgImgW = backgroundImageSR.sprite.rect.width;
        //print("bgImgH: " + bgImgH.ToString());
        //print("bgImgW: " + bgImgW.ToString());

        // Calculate Ratio for scaling...

        float hgImg_scale_ratio_Height = camHeight / bgImgH;
        float hgImg_scale_ratio_Width = camWidht / bgImgW;

        backgroundImage.transform.localScale = new Vector3(hgImg_scale_ratio_Width, hgImg_scale_ratio_Height,1);

    }
}
