using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MFMath {

	public static float OneDecimal(float value) {        
        return (float)(int)(value * 10) / 10;
    }
}
