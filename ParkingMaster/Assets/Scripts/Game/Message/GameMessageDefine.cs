/*******************************************************************
* FileName:     GameMessageDefine.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-8
* Description:  
* other:    
********************************************************************/


namespace Game.MessageCenter
{
    public enum GameMessageDefine
    {
        //10000之前是UFrame用
        SpawnVisitorFromCar = 10001,
        SpawnVisitorFromShip,
        SpawnShuttle,
        SpawnVisitorFromGroundParking,

        /// <summary>
        /// 向入口增加一个游客
        /// </summary>
        AddVisitorToEntryQueue,

        /// <summary>
        /// 向入口增加一个游客结果
        /// </summary>
        AddVisitorToEntryQueueResult,

        /// <summary>
        /// 入口的cd结束
        /// </summary>
        ZooEntryCDFinshed,
        
        /// <summary>
        /// 向动物栏增加一个游客
        /// </summary>
        AddVisitorToLittleZoo,

        /// <summary>
        /// 向动物栏增加一个游客结果
        /// </summary>
        AddVisitorToLittleZooResult,

        /// <summary>
        /// 等待位转观光位
        /// </summary>
        WaitSeatToVisitSeat,


        /// <summary>
        /// 取动物栏信息
        /// </summary>
        LittleZooData,

        /// <summary>
        /// 取动物栏信息回复
        /// </summary>
        LittleZooDataReply,

        /// <summary>
        /// 游客游览时间结束
        /// </summary>
        VisitorVisitCDFinshed,

        /// <summary>
        /// 游客游览时间结束 回复
        /// </summary>
        VisitorVisitCDFinshedReply,

        /// <summary>
        /// 生成载客离开的车
        /// </summary>
        SpawnVisitorCarLeaveZoo,

        /// <summary>
        /// 广播所有动物栏数据
        /// </summary>
        BroadcastAllLittleZooData,

        /// <summary>
        /// 动物园场景加载完毕
        /// </summary>
        LoadZooSceneFinished,

        /// <summary>
        /// 获取解锁动物
        /// </summary>
        GetUnlockAnimals,

        /// <summary>
        /// 获取解锁动物回复
        /// </summary>
        GetUnlockAnimalsReply,

        /// <summary>
        /// 设置玩家数据Coin
        /// </summary>
        SetCoinOfPlayerData,


        /// <summary>
        /// 广播玩家数据Coin
        /// </summary>
        BroadcastCoinOfPlayerData,

        /// <summary>
        /// 多场景多金币 设置玩家数据Coin(可正，可负)
        /// </summary>
        AddCoinOfPlayerDataMSSC,

        /// <summary>
        /// 多场景多金币 广播玩家数据Coin
        /// </summary>
        BroadcastCoinOfPlayerDataMSSC,

        /// <summary>
        /// 设置玩家数据Diamond
        /// </summary>
        SetDiamondOfPlayerData,

        /// <summary>
        /// 广播玩家数据Diamond
        /// </summary>
        BroadcastDiamondOfPlayerData,

        /// <summary>
        /// 设置玩家数据Star
        /// </summary>
        SetStarOfPlayerData,

        /// <summary>
        /// 广播玩家数据Star
        /// </summary>
        BroadcastStarOfPlayerData,

        /// <summary>
        /// 设置停车场等级
        /// </summary>
        SetParkingProfitLevelOfPlayerData,

        /// <summary>
        /// 广播停车场等级
        /// </summary>
        BroadcastParkingProfitLevelOfPlayerData,

        /// <summary>
        /// 设置出口等级
        /// </summary>
        SetExitGateLevelOfPlayerData,

        /// <summary>
        /// 广播出口等级
        /// </summary>
        BroadcastExitGateLevelOfPlayerData,

        

        /// <summary>
        /// 开启新的动物栏
        /// </summary>
        OpenNewLittleZoo,

        /// <summary>
        /// 广播开启新的动物栏
        /// </summary>
        BroadcastOpenNewLittleZoo,

        /// <summary>
        /// 向动物栏增加一个游客(新)
        /// </summary>
        AddVisitorToLittleZooApply,

        /// <summary>
        /// 向动物栏增加一个游客结果(新)
        /// </summary>
        AddVisitorToLittleZooApplyReply,

        /// <summary>
        /// 申请进入出口
        /// </summary>
        AddVisitorToExitGateQueueApply,

        /// <summary>
        /// 申请进入出口回复
        /// </summary>
        AddVisitorToExitGateQueueApplyReply,

        /// <summary>
        /// 场景扩充并且已经改了相关路了
        /// </summary>
        BroadcastAfterExtendSceneAndModifiedPath,

        /// <summary>
        /// 广播在出口排队位中走一步
        /// </summary>
        BroadcastForwardOneStepInExitGateQueue,

        /// <summary>
        /// 发送cd结束
        /// </summary>
        SendExitGateCheckinCDFinish,

        /// <summary>
        /// 发送cd结束回复
        /// </summary>
        SendExitGateCheckinCDFinishReply,

        /// <summary>
        /// 游客数量
        /// </summary>
        BroadcastVisitorNum,

        /// <summary>
        /// 最大游客数量
        /// </summary>
        BroadcastMaxVisitorNum,

        /// <summary>
        /// 摆渡车游客
        /// </summary>
        BroadcastShuttleVisistorNum,

        /// <summary>
        /// 增加buff
        /// </summary>
        AddBuff,

        /// <summary>
        /// 增加buff成功
        /// </summary>
        AddBuffSucceed,

        /// <summary>
        /// 设置入口门票等级
        /// </summary>
        SetEntryGateLevelOfPlayerData,

        /// <summary>
        /// 广播人口门票等级
        /// </summary>
        BroadcastEntryGateLevelOfPlayerData,

        /// <summary>
        /// 游客添加到入口排队占位 申请
        /// </summary>
        AddVisitorToEntryQueuePlaceHolderApply,

        /// <summary>
        /// 游客添加到入口排队占位 回复
        /// </summary>
        AddVisitorToEntryQueuePlaceHolderReply,

        /// <summary>
        /// 游客添加到入口排队正式位 申请
        /// </summary>
        AddVisitorToEntryQueueApply,

        /// <summary>
        /// 游客添加到入口排队正式位 回复
        /// </summary>
        AddVisitorToEntryQueueReply,

        /// <summary>
        /// 获取入口信息请求
        /// </summary>
        GetEntryGateDataApply,

        /// <summary>
        /// 获取入口信息回复
        /// </summary>
        GetEntryGateDataReply,

        /// <summary>
        /// 入口CD时间结束
        /// </summary>
        EntryGateCheckInCDFinshedApply,

        /// <summary>
        /// 入口CD时间结束 回复
        /// </summary>
        EntryGateCheckInCDFinshedReply,

        /// <summary>
        /// 广播在入口排队位中走一步
        /// </summary>
        BroadcastForwardOneStepInEntryGateQueue,

        /// <summary>
        /// 设置开启动物
        /// </summary>
        SetOpenAnimal,

        /// <summary>
        /// 修改动物等级
        /// </summary>
        GetAnimalLevel,

        /// <summary>
        /// 地面停车场申请 请求
        /// </summary>
        AddGroundParkingApply,

        /// <summary>
        /// 地面停车场申请 回复
        /// </summary>
        AddGroundParkingReply,

        /// <summary>
        /// 让地面停车场的车离开
        /// </summary>
        LetGroundParingCarLeave,

        /// <summary>
        /// 广播让地面停车场的车离开
        /// </summary>
        BroadcastLetGroundParingCarLeave,

        /// <summary>
        /// 游客从哪里离开
        /// </summary>
        VisitorWhereLeaveFromApply,

        /// <summary>
        /// 游客从哪里离开 回复
        /// </summary>
        VisitorWhereLeaveFromReply,

        /// <summary>
        /// 停车到地面停车场
        /// </summary>
        ParkingCarInGroundParking,

        /// <summary>
        /// 隐藏部分UI
        /// </summary>
        UIMessage_ActiveButHidePart,

        /// <summary>
        /// 恢复部分UI
        /// </summary>
        UIMessage_ActiveButShowPart,

        /// <summary>
        /// 动物播放升级特效
        /// </summary>
        AnimalPlayLevelUpEffect,

        /// <summary>
        /// 地下停车场数量直接-1
        /// </summary>
        DirectMinusOneUnderParkingNum,

        /// <summary>
        /// 获得道具
        /// </summary>
        GetItem,

        /// <summary>
        /// 使用道具
        /// </summary>
        UseItem,

        /// <summary>
        /// 设置某个入口等级
        /// </summary>
        SetEntryGatePureLevelOfPlayerData,

        /// <summary>
        /// 设置某个入口等级 广播
        /// </summary>
        BroadcastEntryGatePureLevelOfPlayerData,

        /// <summary>
        /// 设置入口数量
        /// </summary>
        SetEntryGateNumOfPlayerData,

        /// <summary>
        /// 设置入口数量 广播
        /// </summary>
        BroadcastEntryGateNumOfPlayerData,



        /// <summary>
        /// 设置停车场停车位数量等级
        /// </summary>
        SetParkingSpaceLevelOfPlayerData,

        /// <summary>
        /// 广播停车场停车位数量等级
        /// </summary>
        BroadcastParkingSpaceLevelOfPlayerData,

        /// <summary>
        /// 设置停车场来客数量等级
        /// </summary>
        SetParkingEnterCarSpawnLevelOfPlayerData,

        /// <summary>
        /// 广播停车场来客数量等级
        /// </summary>
        BroadcastParkingEnterCarSpawnLevelOfPlayerData,


        /// <summary>
        /// 设置动物栏门票等级
        /// </summary>
        SetLittleZooTicketsLevelPlayerData,

        /// <summary>
        /// 设置动物栏门票等级广播
        /// </summary>
        BroadcastLittleZooTicketsLevelPlayerData,

        /// <summary>
        /// 设置动物栏观光数量等级
        /// </summary>
        SetLittleZooVisitorLocationLevelOfPlayerData,

        /// <summary>
        /// 广播动物栏观光数量等级
        /// </summary>
        BroadcastLittleZooVisitorLocationLevelOfPlayerData,

        /// <summary>
        /// 设置动物栏观光游客流量等级
        /// </summary>
        SetLittleZooEnterVisitorSpawnLevelOfPlayerData,

        /// <summary>
        /// 广播动物栏观光游客流量等级
        /// </summary>
        BroadcastLittleZooEnterVisitorSpawnLevelOfPlayerData,

        /// <summary>
        /// 游客获得随机动物栏 申请
        /// </summary>
        VisitorGetRandomLittleZooApply,

        /// <summary>
        /// 游客获得随机动物栏 回复
        /// </summary>
        VisitorGetRandomLittleZooReply,

        /// <summary>
        /// 游客获得观光位 申请
        /// </summary>
        VisitorGetVisitSeatApply,

        /// <summary>
        /// 游客获得观光位 回复
        /// </summary>
        VisitorGetVisitSeatReply,

        /// <summary>
        /// UIPage加到GameManager的Tick中
        /// </summary>
        UIMessage_AddToTick,

        /// <summary>
        /// GameManager的Tick中移除UIPage 
        /// </summary>
        UIMessage_RemoveFromTick,

        /// <summary>
        /// 计算离线
        /// </summary>
        CalcOffline,

        /// <summary>
        /// 打开离线窗口
        /// </summary>
        UIMessage_OpenOfflinePage,

        /// <summary>
        ///发送和任务条件明细相关的消息
        /// </summary>
        SendGameMissionSpecificMessage,

        /// <summary>
        /// 开始了观光人数的任务
        /// </summary>
        BeginVisitorNumberMission,

        /// <summary>
        /// 引导任务状态改变
        /// </summary>
        GuideMissionStateChanged,

        /// <summary>
        /// 光告观看完成
        /// </summary>
        AdWatchComplete,

        /// <summary>
        /// 接待游客完成
        /// </summary>
        VisitorReceiveComplete,

        /// <summary>
        /// 建筑升级
        /// </summary>
        BuildingLevelup,

        /// <summary>
        /// 引导任务动作按钮点击
        /// </summary>
        GuideTaskActionButtonClick,

        /// <summary>
        /// 建筑解锁
        /// </summary>
        BuildingUnlock,

        /// <summary>
        /// 广播非当前场景的金币
        /// </summary>
        BroadcastLeaveSceneCoin,

        /// <summary>
        /// 发送动物图鉴修改的消息
        /// </summary>
        SetAnimalAtlasDataMessage,

        /// <summary>
        /// 发送动物图鉴修改成功的消息
        /// </summary>
        GetAnimalAtlasDataMessage,

        /// <summary>
        /// 立即结束人口大门cd
        /// </summary>
        ImmediateFinishEntryGateCheckInCD,

        /// <summary>
        /// 立即结束观光位cd
        /// </summary>
        ImmediateFinishVisitCD,

        /// <summary>
        /// 汽车碰撞了
        /// </summary>
        CarCollider,

        /// <summary>
        /// 
        /// </summary>
        TriggerLoadRoad,

        /// <summary>
        /// 
        /// </summary>
        StageContinue,

        /// <summary>
        /// 
        /// </summary>
        ParkingSucess,
    }
}

