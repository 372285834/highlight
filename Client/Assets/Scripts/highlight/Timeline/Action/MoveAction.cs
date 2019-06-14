﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace highlight.tl
{
    [Action("行为/移动", typeof(MoveAction))]
    public class MoveAction : TargetAction
    {
        [Desc("开始挂点")]
        public IPosition start;
        [Desc("结束挂点")]
        public IPosition end;
        [Desc("运动轨迹")]
        public IEvaluateV3 eva;
        public override bool OnTrigger()
        {
            return (start == null || end == null || eva == null) ? false : true;
        }
        public override void OnUpdate()
        {
            Vector3 pos = eva.Evaluate(this.start.pos, this.end.pos, this.timeObject.progress);
            this.role.SetPos(pos,false);
        }
    }
}