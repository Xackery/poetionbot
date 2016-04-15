﻿using System;
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

        public int maxHP = 0;
        public int curHP = 0;
        public int maxMana = 0;
        public int curMana = 0;
        public int[] clickDelay;

        public string CheckStats()
        {
            if (clickDelay == null)
            {
                clickDelay = new int[5];
            }

            var retString = "";
            retString += CheckHealth();
            retString += CheckMana();
            return retString;
        }

        //53576CCC hp max 204
        //53576CD0 hp current 208
        //53576CF0 mana max  240
        //53576CF4 mana current 244

        public string CheckHealth()
        {
            var retString = "";
            maxHP = w32.ReadProcessMemory(handle, manaAddress - 40);
            curHP = w32.ReadProcessMemory(handle, manaAddress - 36);
            if (curHP < 1 || maxHP < 1) {
                return "";
            }
            if (((float)curHP / (float)maxHP) < 0.4)
            {
                UsePotion(2);
                retString += "Used heal potion2 due to " + curHP + "/" + maxHP+ "\n";
            }

            if (((float)curHP / (float)maxHP) < 0.4)
            {
                UsePotion(4);
                retString += "Used heal potion 4 due to " + curHP + "/" + maxHP + "\n";
            }

            if (((float)curHP / (float)maxHP) < 0.5)
            {
                UsePotion(3);
                retString += "Used heal potion 3 due to " + curHP + "/" + maxHP +"\n";
            }


            if (((float)curHP / (float)maxHP) < 0.8)
            { 
                UsePotion(1);
                retString +=  "Used heal potion 1 due to " + curHP + "/" + maxHP + "\n";
            }
            
            return retString;
        }

        public string CheckMana()
        {
            maxMana = w32.ReadProcessMemory(handle, manaAddress - 4);
            curMana = w32.ReadProcessMemory(handle, manaAddress);
            if (curMana < 1 || maxMana < 1)
            {
                return "";
            }
            
            if (((float)curMana / (float)maxMana)  < 0.1)
            {
                UsePotion(5);
                return "Used mana potion due to " + curMana + "/" + maxMana+ "\n";
            }
            return "";
        }

        public void UsePotion(int hotkey)
        {
            if (hotkey > 5 || hotkey < 1)
            {
                return;
            }

            if (clickDelay[hotkey-1] > (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)) {
                return;
            }
            w32.PressKey(GetHotkey(hotkey), 1);
            clickDelay[hotkey-1] = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)+5;
        }
        

        public ushort GetHotkey(int hotkey)
        {
            if (hotkey == 1) return 0x002;
            if (hotkey == 2) return 0x003;
            if (hotkey == 3) return 0x004;
            if (hotkey == 4) return 0x005;
            if (hotkey == 5) return 0x006;
            return 0x000;
        }
    }
}
