using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatReference 
{
    public bool useConstant;
    public float constantValue;
    public FloatVariable variableSOValue;

    public float Value { 
        get { return useConstant ? constantValue : variableSOValue.Value; }
    
        set
        {
            if (useConstant)
                value = constantValue;
            else
                value = variableSOValue.Value;
        }

    }


}
