using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Audio : MonoBehaviour
{
    public Sprite On;
    public Sprite Off;
    public Button button;

    public void muteAudio()
    {

        if(AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
            button.image.overrideSprite = Off;
        }
        else if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            button.image.overrideSprite = On;
        }
        
    }

}
