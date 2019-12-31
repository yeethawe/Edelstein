using System.Threading.Tasks;
using Edelstein.Core.Utils;
using Edelstein.Core.Utils.Packets;
using Edelstein.Network.Packets;

namespace Edelstein.Service.Game.Handlers
{
    public class MigrateInHandler : AbstractPacketHandler<GameServiceAdapter>
    {
        protected override async Task Handle(
            GameServiceAdapter adapter,
            RecvPacketOperations operation,
            IPacket packet
        )
        {
            var characterID = packet.Decode<int>();

            packet.Decode<long>(); // MachineID 1
            packet.Decode<long>(); // MachineID 2

            packet.Decode<bool>(); // isUserGM
            packet.Decode<byte>(); // Unk

            var clientKey = packet.Decode<long>();

            try
            {
                await adapter.TryMigrateFrom(characterID, clientKey);
            }
            catch
            {
                await adapter.Close();
            }
        }
    }
}