using UnityEngine;
using System.Collections;

public class LightPulse : MonoBehaviour {

    public Light pulseLight1;
    public Light pulseLight2;
    public Light pulseLight3;
    public Light pulseLight4;
    public Light pulseLight5;
    private int timer1 = 0;
    private int timer2 = 0;
    private int timer3 = 0;
    private int timer4 = 0;
    private int timer5 = 0;
    private bool pulseUp1 = true;
    private bool pulseUp2 = true;
    private bool pulseUp3 = true;
    private bool pulseUp4 = true;
    private bool pulseUp5 = true;
	void Start () 
    {
        pulseLight1.intensity = 3f;
        pulseLight2.intensity = 2f;
        pulseLight3.intensity = 1f;
        pulseLight4.intensity = 1f;
        pulseLight5.intensity = 3f;
	}
	
	
	void Update () 
    {
        //Light 1
        if (pulseUp1 == true)
        {
            pulseLight1.intensity += 0.03f;
        }
        else if (pulseUp1 == false)
        {
            pulseLight1.intensity -= 0.03f;
        }

        timer1 += 1;
        //Light1

        //Light 2
        if (pulseUp2 == true)
        {
            pulseLight2.intensity += 0.06f;
        }
        else if (pulseUp2 == false)
        {
            pulseLight2.intensity -= 0.06f;
        }

        timer2 += 1;
        //Light2

        //Light 3
        if (pulseUp3 == true)
        {
            pulseLight3.intensity += 0.02f;
        }
        else if (pulseUp3 == false)
        {
            pulseLight3.intensity -= 0.02f;
        }

        timer3 += 1;
        //Light3

        //Light 4
        if (pulseUp4 == true)
        {
            pulseLight4.intensity += 0.1f;
        }
        else if (pulseUp4 == false)
        {
            pulseLight4.intensity -= 0.1f;
        }

        timer4 += 1;
        //Light4

        //Light 4
        if (pulseUp5 == true)
        {
            pulseLight5.intensity += 0.01f;
        }
        else if (pulseUp5 == false)
        {
            pulseLight5.intensity -= 0.01f;
        }

        timer5 += 1;
        //Light4
        
	
	}
    void FixedUpdate()
    {
        if (timer1 >= 250)
        {
            if(pulseUp1 == true)
            {
                pulseUp1 = false;
                timer1 = 0;
            }
            else if (pulseUp1 == false)
            {
                pulseUp1 = true;
                timer1 = 0;
            }
        }
        if (timer2 >= 550)
        {
            if (pulseUp2 == true)
            {
                pulseUp2 = false;
                timer2 = 0;
            }
            else if (pulseUp2 == false)
            {
                pulseUp2 = true;
                timer2 = 0;
            }
        }

        if (timer3 >= 600)
        {
            if (pulseUp3 == true)
            {
                pulseUp3 = false;
                timer3 = 0;
            }
            else if (pulseUp3 == false)
            {
                pulseUp3 = true;
                timer3 = 0;
            }
        }
        if (timer4 >= 300)
        {
            if (pulseUp4 == true)
            {
                pulseUp4 = false;
                timer4 = 0;
            }
            else if (pulseUp4 == false)
            {
                pulseUp4 = true;
                timer4 = 0;
            }
        }

        if (timer5 >= 250)
        {
            if (pulseUp5 == true)
            {
                pulseUp5 = false;
                timer5 = 0;
            }
            else if (pulseUp5 == false)
            {
                pulseUp5 = true;
                timer5 = 0;
            }
        }
    }
}
