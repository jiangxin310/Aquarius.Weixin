﻿namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 消息/事件处理
    /// </summary>
    public interface IAllMessageHandler : INormalMessageHandler, IEventMessageHandler
    { }
}