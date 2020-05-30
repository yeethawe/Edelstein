using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Edelstein.Core.Gameplay.Migrations.States;
using Edelstein.Core.Network.Packets;
using Edelstein.Core.Utils.Packets;
using Edelstein.Service.Game.Fields.Objects.User;

namespace Edelstein.Service.Game.Handlers.Users
{
    public class UserMigrateToCashShopRequestHandler : AbstractFieldUserHandler
    {
        protected override async Task Handle(
            FieldUser user,
            RecvPacketOperations operation,
            IPacketDecoder packet
        )
        {
            try
            {
                var services = (await user.Service.GetPeers())
                    .Select(p => p.State)
                    .OfType<ShopNodeState>()
                    .Where(s => user.Service.State.Worlds.Any(w => s.Worlds.Contains(w)))
                    .ToImmutableArray();
                var service = services.First();

                if (services.Length > 1)
                {
                    var id = await user.Prompt<int>(target => target.AskMenu(
                        "Which service should I connect to?", services.ToDictionary(
                            s => services.IndexOf(s),
                            s => s.Name
                        ))
                    );
                    service = services[id];
                }

                await user.Adapter.TryMigrateTo(service);
            }
            catch
            {
                using var p = new OutPacket(SendPacketOperations.TransferChannelReqIgnored);
                p.EncodeByte(0x2);
                await user.SendPacket(p);
            }
        }
    }
}