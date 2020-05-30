using Edelstein.Core.Network;
using Edelstein.Core.Network.Packets;

namespace Edelstein.Core.Utils.Packets
{
    public class PacketHandlerContext : IPacketHandlerContext
    {
        public RecvPacketOperations Operation { get; }
        public IPacketDecoder Packet { get; }
        public ISocketAdapter Adapter { get; }

        public PacketHandlerContext(RecvPacketOperations header, IPacketDecoder packet, ISocketAdapter adapter)
        {
            Operation = header;
            Packet = packet;
            Adapter = adapter;
        }
    }
}