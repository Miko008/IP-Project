using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticle : MonoBehaviour
{
    private float live_time,
                 color_hue,
                 scale_change;
    private Renderer rend;
    private Color color;

    public void SetUp(float _color_hue, float _live_time, Vector3 scale, float _scale_change)
    {
        color_hue = _color_hue;
        live_time = _live_time;
        scale_change = _scale_change;
        rend = this.GetComponent<Renderer>();
        color = new Color(color_hue, color_hue, color_hue, 1);
        gameObject.transform.localScale = scale;
        rend.material.color = color;
        StartCoroutine(Fade());
    }

    void SetColor(float rate)
    {
        //Debug.Log(rate);
        //this.GetComponent<Renderer>().material.color = Color.Lerp(new Color(color_hue, color_hue, color_hue, 1), Color.clear, rate);
        color.a = rate;
        rend.material.color = color;
    }

    IEnumerator Fade() 
    {
        float temp;
        for(float timer = 0; timer < live_time; timer += 0.05f) 
        {
            timer += Time.deltaTime;

            temp = 1f - (timer/live_time);

            //Debug.Log(temp);
            SetColor(temp > 0f ? temp : 0f);

            gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);

            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
}
