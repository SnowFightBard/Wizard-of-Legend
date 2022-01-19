using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private Dictionary<int,List<Sprite>> dic;

    public SkillSpawn data;
    public Text text;


    private void Start()
    {
        text.text = "index : " + data.index + "\nskill name : " + data.skillname +"\ncooltime : " + data.cooltime + "\nrange : " + data.range;
        dic.Add(data.index, data.resource);
    }

    private void Update()
    {
        
    }


    private void GetResource(string Key)
    {

    }
}
