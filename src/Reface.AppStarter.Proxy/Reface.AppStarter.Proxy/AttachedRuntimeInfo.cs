﻿using System;

namespace Reface.AppStarter.Proxy
{
    public class AttachedRuntimeInfo
    {
        /// <summary>
        /// 附加代理的条件
        /// </summary>
        public IAttachment Attachment { get; set; }

        /// <summary>
        /// 代理的类型
        /// </summary>
        public Type AttachedType { get; set; }
    }
}
