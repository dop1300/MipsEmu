using MipsEmu.Emulation.Registers;
using MipsEmu.Emulation.Devices;
using MipsEmu.Emulation;

namespace MipsEmu.Emulation.Instructions {

    public interface IInstruction {

        void Run(Hardware hardware, Bits bits);

    }

    public abstract class InstructionIType : IInstruction {
        public static readonly Interval RS_BITS = new Interval(16, 5);
        public void Run(Hardware hardware, Bits bits) {
            var imm = bits.LoadBits(0, 16);
            var rs = bits.GetUnsignedIntFromRange(16, 5);
            var rt = bits.GetUnsignedIntFromRange(21, 5);

            Bits sourceValue = hardware.registers.GetRegisterBits(rs);

            Run(hardware, sourceValue, rt, imm);
        }

        public Bits CalculateStoreAddress(Hardware hardware, Bits rsValue, Bits imm) {
            return Alu.AddSigned(rsValue, imm.SignExtend16());
        }

        public abstract void Run(Hardware hardware, Bits rsValue, int rt, Bits imm);
    }

    public abstract class InstructionRType : IInstruction {


        public void Run(Hardware hardware, Bits bits) {
            var rs = bits.GetUnsignedIntFromRange(21, 5);
            var rt = bits.GetUnsignedIntFromRange(16, 5);
            var rd = bits.GetUnsignedIntFromRange(11, 5);
            Run(hardware, hardware.registers.GetRegisterBits(rs), hardware.registers.GetRegisterBits(rt), rd);
        }

        public abstract void Run(Hardware hardware, Bits rsValue, Bits rtValue, int rd);
    }

    public abstract class InstructionJType : IInstruction {

        public void Run(Hardware hardware, Bits instruction) {
            long highOrder = 0xF00000 & hardware.programCounter.GetBits().GetAsUnsignedLong();
            long psuedoAddress = instruction.LoadBits(0, 26).GetAsSignedLong();
            var address = new Bits(32);
            address.SetFromUnsignedLong((psuedoAddress >> 2) + highOrder);
            RunJump(hardware, address);
        }

        public abstract void RunJump(Hardware hardware, Bits address);
    }

    

    

}