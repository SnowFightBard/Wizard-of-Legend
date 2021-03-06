using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/Skill")]
public class Skill : ScriptableObject
{
    // 스킬 내부 스탯을 지정해놓은 스크립트 //

    public int index;   // 스킬 고유 인덱스번호
    public int type;    // 1 : 단발성 근접공격 , 2 : 직선형 원거리 스킬 , 3 : 직선형 원거리 연사스킬(5번) , 4 : 부채꼴 분사형 스킬 (1번)
    public string skillName;    // 스킬 이름
    public float damage;    // 데미지
    public float range; // 스킬 사거리
    public float coolTime;  // 스킬 쿨타임
    public float activeTime; // 시전 시간
    public bool isRot; // 스킬을 마우스 위치에따라 회전시킬지 결정하는 변수
    [TextArea] public string info; // 스킬 설명
    public Sprite image; // 스킬 이미지

    /*
     
     0 : 근접 바람공격
     1 : 아이스 로드
     2 : 섀도우 볼

     */

}