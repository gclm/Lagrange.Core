using System.Text.Json;
using System.Text.Json.Nodes;
using Lagrange.Core;
using Lagrange.Core.Common.Interface.Api;
using Lagrange.OneBot.Core.Entity.Action;
using Lagrange.OneBot.Core.Entity.Action.Response;

namespace Lagrange.OneBot.Core.Operation.Message;

[Operation("send_private_msg")]
public sealed class SendPrivateMessageOperation : IOperation
{
    public async Task<OneBotResult> HandleOperation(BotContext context, JsonObject? payload)
    {
        switch (payload.Deserialize<OneBotPrivateMessageBase>())
        {
            case OneBotPrivateMessage message:
                // List<Message>
                await context.SendMessage(MessageCommon.ParseChain(message).Build());
                break;
            case OneBotPrivateMessageSimple messageSimple:
                // Message
                await context.SendMessage(MessageCommon.ParseChain(messageSimple).Build());
                break;
            case OneBotPrivateMessageText messageText:
                // String
                await context.SendMessage(MessageCommon.ParseChain(messageText).Build());
                break;
            default:
                throw new Exception();
        }
        
        return new OneBotResult(new OneBotMessageResponse(0), 0, "ok");
    }
}