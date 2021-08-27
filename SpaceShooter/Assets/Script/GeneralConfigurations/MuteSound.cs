using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSound : MonoBehaviour
{
    public static bool isMuted;
    public static void MuteToggle(bool Unmuted)
    {
        
        if(Unmuted)
        {
            AudioListener.volume = 1;
            isMuted = false;
        }
        else
        {
            AudioListener.volume = 0;
            isMuted = true;
        }
    }
}
