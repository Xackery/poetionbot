using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poetionbot
{
    class bot
    {
        public int manaAddress;
        public IntPtr handle;

        public int maxHP = 247;
        public int maxMana = 113;


        public int healDelay;
        public int manaDelay;

        public void CheckStats()
        {
            CheckHealth();
            CheckMana();
        }

        public void CheckHealth()
        {

            var hp = w32.ReadProcessMemory(handle, manaAddress -4);
            if (hp < 0) {
                return;
            }
            if ((hp / maxHP * 100) < 50)
            {
                UseHealPotion();
            }
            return;
        }

        public int CheckMana()
        {            
            var mana = w32.ReadProcessMemory(handle, manaAddress);
            if (mana < 0)
            {
                return 0;
            }
            if ((mana / maxMana * 100) < 50)
            {
                UseManaPotion();
            }

            return mana;
        }

        public void UseHealPotion()
        {

            if (healDelay > (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)) {
                return;
            }
            w32.PressKey(0x002, 1); //1 key
            healDelay = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)+5;
        }

        public void UseManaPotion()
        {

            if (manaDelay > (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds))
            {
                return;
            }
            w32.PressKey(0x006, 1); //5 key
            manaDelay = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds) + 5;
        }
    }
}
