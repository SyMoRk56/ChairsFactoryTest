using System;
using UnityEngine;
using UnityEngine.Playables;

public class Wall : Building
{
    public Decor[] decorLevel;
    public override void OnUpgrade()
    {
        foreach(var decor in decorLevel[upgrade - 1].decor)
        {
            decor.SetActive(true);
        }
        
    }
    public override void Setup()
    {
        base.Setup();
    }
}
