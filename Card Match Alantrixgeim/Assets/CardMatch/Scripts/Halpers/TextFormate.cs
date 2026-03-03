using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFormate
{
    public static string getSecToHour(float _sec)
    {
        
        string time = "";
        float hour = (_sec / 60) / 60;
        float min = (_sec / 60)%60;
        float sec = (_sec % 60);
      
        if (hour < 10)
        {
            if (hour < 1)
            {
                time = "00:";
            }
            else
            {
                time = "0" + ((int)hour) + ":";
            }
        }
        else
        {
            time = ((int)hour) + ":";
        }
        if (min < 10)
        {
            if (min < 1)
            {
                time = time + "00:";
            }
            else
            {
                time = "0" + ((int)min) + ":";
            }
        }
        else
        {
            time = ((int)min) + ":";
        }
        if (sec < 10)
        {
            if (sec < 1)
            {
                time = time + "00";
            }
            else
            {
                time = time + "0" + ((int)sec);
            }
        }
        else
        {
            time = time + ((int)sec);
        }
        // Debug.Log(_sec+":::"+time);
        return time;
    }

    public static string getSecToMin(float _sec) {
        
        string time = "" ;
        float min = _sec / 60;
        float sec = (_sec % 60);
        if (min < 10)
        {
            if (min < 1)
            {
                time = "00:";
            }
            else
            {
                time = "0" + ((int)min) + ":";
            }
        }
        else {
            time =  ((int)min) + ":";
        }
        if (sec < 10)
        {
            if (sec < 1)
            {
                time = time + "00";
            }
            else
            {
                time = time + "0" + ((int)sec);
            }
        }
        else {
            time = time  + ((int)sec);
        }
       // Debug.Log(_sec+":::"+time);
        return time;
    }
    public static string getSecToMinNano(float _sec)
    {
       // Debug.Log("Sec:" + _sec);
        string value= getSecToMin(_sec);
        int _value = (int)((_sec * 100)%100 );
        value += ":" + _value;
        return value;
    }
}
