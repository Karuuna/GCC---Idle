using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetCookieButtonView : MonoBehaviour {

    public Text CookiesPerClickText;
    public ParticleSystem CookieParticleSystem;
    //emit particles that include text https://answers.unity.com/questions/1320381/is-it-possible-to-create-text-string-particles.html 
    // half filled ones for half cookies


    public void UpdateAmount(double newAmount)
{
    CookiesPerClickText.text = newAmount.ToString();
}

    public void ParticleCookie(float amount)
    {
        CookieParticleSystem.Emit((int)amount);
    }
}
