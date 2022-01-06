using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public enum VillagerEnum
    {
        현빈 = 10001,
        승원,
        현승,
        현욱,
        명직,
        시훈,
        동현,
        가성,
        희상,
        현호,
        은성,
        영재,
        진형,
        도현,
        성길,
        성현,
        재만,
        시완,
        승민

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

    public enum CropEnum
    {
        사과=30001,
        귤,
        포도,
        복숭아,
        배
    }

    public enum IntroPhase
    {
        Start,
        StaticData,
        UserData,
        ApplicationSetting,
        Complete
    }

    public enum Growth
    {
        Wait,
        Start,
        Growth,
        Get
    }
}
