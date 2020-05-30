using System.Threading.Tasks;
using Edelstein.Core.Network.Packets;
using Edelstein.Core.Utils.Packets;
using Edelstein.Service.Game.Fields.Objects.User;

namespace Edelstein.Service.Game.Handlers.Users
{
    public class UserEmotionHandler : AbstractFieldUserHandler
    {
        protected override async Task Handle(
            FieldUser user,
            RecvPacketOperations operation,
            IPacketDecoder packet
        )
        {
            var emotion = packet.DecodeInt();
            var duration = packet.DecodeInt();
            var byItemOption = packet.DecodeBool();

            // TODO: item option checks

            using var p = new OutPacket(SendPacketOperations.UserEmotion);
            p.EncodeInt(user.ID);
            p.EncodeInt(emotion);
            p.EncodeInt(duration);
            p.EncodeBool(byItemOption);
            await user.BroadcastPacket(p);
        }
    }
}