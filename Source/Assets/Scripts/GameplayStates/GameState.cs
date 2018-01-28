using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [HideInInspector]public GameStateController gSC;

    public virtual void Init(GameStateController gSC)
    {
        this.gSC = gSC;
        //yield break;
    }

    public virtual IEnumerator GameStateUpdate()
    {
        yield break;
    }

    public void DestroyChildren(Transform ob, string name = null, string notName = null)
    {
        foreach (Transform child in ob)
        {
            if ((name == null && notName == null) || (name != null && name == child.name) || (notName != null && notName != child.name))
                GameObject.Destroy(child.gameObject);
        }
    }
}
