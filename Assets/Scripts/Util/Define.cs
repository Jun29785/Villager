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
        //다음 고블린 순서대로 작성
    }

    public enum IntroPhase
    {
        Start,
        UserData,
        StaticData,
        ApplicationSetting,
        Complete
    }
}
