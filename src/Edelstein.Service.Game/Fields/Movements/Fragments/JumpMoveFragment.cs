using System.Drawing;
using Edelstein.Core.Network.Packets;

namespace Edelstein.Service.Game.Fields.Movements.Fragments
{
    public class JumpMoveFragment : ActionMoveFragment
    {
        private Point _vPosition;

        public JumpMoveFragment(MoveFragmentAttribute attribute, IPacketDecoder packet) : base(attribute, packet)
        {
        }

        public override void DecodeData(IPacketDecoder packet)
        {
            _vPosition = packet.DecodePoint();

            base.DecodeData(packet);
        }

        public override void EncodeData(IPacketEncoder packet)
        {
            packet.EncodePoint(_vPosition);

            base.EncodeData(packet);
        }
    }
}