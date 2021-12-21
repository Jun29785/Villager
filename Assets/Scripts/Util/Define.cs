using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public enum VillagerEnum
    {
        빌 = 10001,
        스티브,
        알렉스,
        안나,
        //다음 순서대로 작성
    }

    public enum ShopEnum
    {
        주민시계=20001,
        마을인구,
        골드획득량,
        주민시계속도,
        농작물성장
    }

    public enum IntroPhase
    {
        Start,
        StaticData,
        UserData,
        ApplicationSetting,
        Complete
    }
}
