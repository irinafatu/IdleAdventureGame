using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu (fileName = "User", menuName = "UserAppearance")]
public class UserAppearanceSO : ScriptableObject
{
    public List<BusinessLevel> businessLevelList;
}
