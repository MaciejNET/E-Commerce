using System.Threading.Channels;
using ECommerce.Shared.Abstractions.Messaging;

namespace ECommerce.Shared.Infrastructure.Messaging.Dispatchers;

public interface IMessageChannel
{
    ChannelReader<IMessage> Reader { get; }
    ChannelWriter<IMessage> Writer { get; }
}