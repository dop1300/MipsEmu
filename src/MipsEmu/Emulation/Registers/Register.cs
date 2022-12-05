using MipsEmu.Emulation;

namespace MipsEmu.Emulation.Registers {


    public class ZeroRegister : Register {

        public override void SetBits(Bits bits) {

        }

    }
    public class Register {
        private Bits data;

        public Register() {
            data = new Bits(32);
        }

        public virtual void SetBits(Bits bits) {
            if (bits.GetLength() != 32) {
                throw new Exception("Size mismatch between register and stored bits.");
            } else {
                data = bits;
            }
        }

        public Bits GetBits() {
            return data;
        }

        public void SetFromSignedInt(int value) {
            // TODO implement
        }
    }
}