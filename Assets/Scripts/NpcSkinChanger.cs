using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSkinChanger : MonoBehaviour
{
    public AnimatorOverrideController animTanpaMasker;
    [SerializeField] private bool forNpcBackground;

    public void hilangkanMasker()
    {
        if(forNpcBackground)
        {
            GetComponent<NpcBg>().anim.runtimeAnimatorController = animTanpaMasker as RuntimeAnimatorController;

        }
        else
        {
            for (int i = 0; i < GetComponent<Npc>().animNPC.Length; i++)
            {
                GetComponent<Npc>().animNPC[i].runtimeAnimatorController = animTanpaMasker as RuntimeAnimatorController;
            }
        }
        
    }
}
