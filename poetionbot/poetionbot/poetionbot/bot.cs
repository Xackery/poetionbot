using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace poetionbot
{
    class bot
    {
        public IntPtr handle;

        public int maxHP = 0;
        public int curHP = 0;
        public int maxMana = 0;
        public int curMana = 0;
        public int[] clickDelay;
        
        public pointerset ps;



        public potionRule[] rules;
        //53576CCC hp max 204
        //53576CD0 hp current 208
        //53576CF0 mana max  240
        //53576CF4 mana current 244

        public bot()
        {
            LoadIni();
        }
        
        public void ResetDefaults()
        {
            ps = new pointerset();
            ps.offsets = new int[6];
            ps.offsets[0] = 0x9DD404;
            ps.offsets[1] = 0x44;
            ps.offsets[2] = 0x688;
            ps.offsets[3] = 0x2C8;
            ps.offsets[4] = 0x70;
            ps.offsets[5] = 0x54;

            clickDelay = new int[5];
            rules = new potionRule[5];
            for (var i = 0; i < 5; i++)
            {
                rules[i] = new potionRule();
            }
        }

        public void SaveIni()
        {
            File.WriteAllText(@"poetionbot.ini", JsonConvert.SerializeObject(rules));
        }

        public void LoadIni()
        {
            try
            {
                rules = Newtonsoft.Json.JsonConvert.DeserializeObject<potionRule[]>(File.ReadAllText(@"poetionbot.ini"));
                if (rules == null)
                {
                    ResetDefaults();
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                ResetDefaults();
                SaveIni();
            }
        }
        public string GetStats()
        {
            maxHP = w32.ReadProcessMemoryOffset(handle, ps, -40);
            curHP = w32.ReadProcessMemoryOffset(handle, ps, -36);
            maxMana = w32.ReadProcessMemoryOffset(handle, ps, -4);
            curMana = w32.ReadProcessMemoryOffset(handle, ps, 0);            
            return curHP + "/" + maxHP + " " + curMana + "/" + maxMana;
        }

        public string CheckHealth()
        {


            var retString = "";
            maxHP = w32.ReadProcessMemoryOffset(handle, ps, -40);
            curHP = w32.ReadProcessMemoryOffset(handle, ps, -36);
            if (curHP < 1 || maxHP < 1) {
                throw new Exception("Memory Failure");                
            }

            for (var i =0; i < rules.Length; i++) {
                var rule = rules[i];
                if (!rule.isHP) continue;

                if (((float)curHP / (float)maxHP) < rule.percent)
                {
                    UsePotion(i+1);
                    retString += "Used heal potion "+(i+1)+" due to " + curHP + "/" + maxHP + "\n";
                }
            }
            return retString;
        }

        public string CheckMana()
        {
            maxMana = w32.ReadProcessMemoryOffset(handle, ps, -4);
            curMana = w32.ReadProcessMemoryOffset(handle, ps, 0);
            if (curMana < 1 || maxMana < 1)
            {
                throw new Exception("Memory Failure");
            }

            var retString = "";
            for (var i = 0; i < rules.Length; i++)
            {
                var rule = rules[i];
                if (rule.isHP) continue;
                if (((float)curMana / (float)maxMana) < rule.percent)
                {
                    UsePotion(i + 1);
                    retString += "Used mana potion " + (i + 1) + " due to " + curMana + "/" + maxMana + "\n";
                }
            }
            return retString;
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
