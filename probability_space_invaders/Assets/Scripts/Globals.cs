using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Globals : MonoBehaviour
{
    public Slider sliderval;
    static public float slidervalfloat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slidervalfloat = sliderval.value;
        print(slidervalfloat);
    }
}
