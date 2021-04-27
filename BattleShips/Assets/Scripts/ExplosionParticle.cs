using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour
{
    private float live_time,
                  scale_change;
    private Color startColor,
                  endColor;
    private Renderer rend;


    public void SetUp(Color _startColor, Color _endColor, Vector3 scale,  float _live_time, float _scale_change)
    {
        startColor = _startColor;
        endColor = _endColor;
        live_time = _live_time;
        scale_change = _scale_change;
        rend = this.GetComponent<Renderer>();
        rend.material.color = _startColor;
        gameObject.transform.localScale = scale;
        StartCoroutine(Fade());
    }

    IEnumerator Fade() 
    {
        Color c;
        for(float timer = 0; timer <= live_time + 0.1f; timer += 0.05f) 
        {
            float temp = (timer/live_time) < 1? (timer/live_time) : 1;
            c = Color.Lerp(startColor, endColor, temp);
            c.a -= temp;
            rend.material.color = c;

            gameObject.transform.localScale += new Vector3(scale_change,scale_change,scale_change);

            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
}
