using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Audio : MonoBehaviour
{

    public void muteAudio()
    {

        if(AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
           
        }
        else if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
     
        }
        
    }

}
