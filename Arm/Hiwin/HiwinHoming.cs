﻿using System;
using Arm.Type;
using Basic.Message;
using SDKHrobot;

namespace Arm.Hiwin
{
    public class HiwinHoming : HiwinBasicMotion, IHoming
    {
        public HiwinHoming(CoordinateType coordinateType,
                           int id,
                           IMessage message,
                           ref bool waitingState,
                           bool needWait = true)
            : base(0, 0, 0, 0, 0, 0, id, message, ref waitingState, null)
        {
            NeedWait = needWait;
            int retuenCode;
            switch (coordinateType)
            {
                case CoordinateType.Descartes:
                    retuenCode = HRobot.ptp_pos(_id, 0, Default.DescartesHomePosition);
                    break;

                case CoordinateType.Joint:
                    retuenCode = HRobot.ptp_axis(_id, 0, Default.JointHomePosition);
                    break;

                default:
                    throw new ArgumentException("Unknown coordinator type.");
            }

            WaitForMotionComplete(retuenCode);
        }
    }
}