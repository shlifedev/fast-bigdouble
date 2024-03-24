using System;
using System.Collections;
using System.Collections.Generic;
using LD;
using UnityEngine;
using UnityEngine.Profiling;

public class Profile : MonoBehaviour
{
    public string v;
    private void Update()
    {
        Debug.Log(new BigDouble(v).GetUnit());
    }

    // Start is called before the first frame update
    void Start()
    {

         
        Profiler.BeginSample("ExponentCtor");
        for (int i = 0; i < 10000; i++)
        {
            new BigDouble("1e10000");
        }

        Profiler.EndSample();
        Profiler.BeginSample("AlphabetCtor");
        for (int i = 0; i < 10000; i++)
        {
            new BigDouble("10ZZZ");
        }

        Profiler.EndSample();
        
        Profiler.BeginSample("SmallAlphabetCtor");
        for (int i = 0; i < 10000; i++)
        {
            new BigDouble("1ZZZ");
        } 
        Profiler.EndSample();
        
        
        Profiler.BeginSample("NumberCtor");
        for (int i = 0; i < 10000; i++)
        {
            new BigDouble("100000000000000000000000000000000");
        }

        Profiler.EndSample();
        var bd = new BigDouble("1e10000");
        Profiler.BeginSample("ToString");
        for (int i = 0; i < 10000; i++)
        {
            bd.ToString();
         
        }
        Profiler.EndSample();
        Application.Quit();

    }
}