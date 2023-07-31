using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImovement : BattleSystem
{
    AIPerception _perception = null;
    protected AIPerception myPerception
    {
        get
        {
            if (_perception == null)
            {
                _perception = GetComponent<AIPerception>();
                if (_perception == null)
                {
                    _perception = GetComponentInChildren<AIPerception>();
                }
            }
            return _perception;
        }
    }
}

