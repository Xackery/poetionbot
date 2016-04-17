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

        public config config = new config();
        public int maxHP = 0;
        public int curHP = 0;
        public int maxMana = 0;
        public int curMana = 0;
        public int[] clickDelay;
        
        //53576CCC hp max 204
        //53576CD0 hp current 208
        //53576CF0 mana max  240
        //53576CF4 mana current 244

        public bot()
        {
            clickDelay = new int[5];
            LoadIni();
        }
        
        public void ResetDefaults()
        {
            
            config.ManaPointerSet = new pointerset();
            config.ManaPointerSet.ModuleName = "PathOfExileSteam.exe";
            config.ManaPointerSet.Offsets = new int[6];
            config.ManaPointerSet.Offsets[0] = 0x9DD404;
            config.ManaPointerSet.Offsets[1] = 0x44;
            config.ManaPointerSet.Offsets[2] = 0x688;
            config.ManaPointerSet.Offsets[3] = 0x2C8;
            config.ManaPointerSet.Offsets[4] = 0x70;
            config.ManaPointerSet.Offsets[5] = 0x54;

            clickDelay = new int[5];

            config.UpdateRateInMs = 100;

            config.Rules = new potionRule[5];
            for (var i = 0; i < 5; i++)
            {
                config.Rules[i] = new potionRule();
                config.Rules[i].Hotkey = i + 1;
                config.Rules[i].IsHPTrigger = true;
                if (i > 3)
                {
                    config.Rules[i].IsHPTrigger = false;
                }
                config.Rules[i].Percent = 0.5f;
            }
        }

        public void SaveIni()
        {
            File.WriteAllText(@"poetionbot.ini", JsonConvert.SerializeObject(config, Formatting.Indented));
        }

        public void LoadIni()
        {
            try
            {
                config = Newtonsoft.Json.JsonConvert.DeserializeObject<config>(File.ReadAllText(@"poetionbot.ini"));
                if (config == null)
                {
                    ResetDefaults();
                }
                if (config.UpdateRateInMs < 10)
                {
                    config.UpdateRateInMs = 10;
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
            maxHP = w32.ReadProcessMemoryOffset(handle, config.ManaPointerSet, -40);
            curHP = w32.ReadProcessMemoryOffset(handle, config.ManaPointerSet, -36);
            maxMana = w32.ReadProcessMemoryOffset(handle, config.ManaPointerSet, -4);
            curMana = w32.ReadProcessMemoryOffset(handle, config.ManaPointerSet, 0);            
            return curHP + "/" + maxHP + " HP | " + curMana + "/" + maxMana + " MP";
        }

        public string CheckHealth()
        {            
            var retString = "";
            maxHP = w32.ReadProcessMemoryOffset(handle, config.ManaPointerSet, -40);
            curHP = w32.ReadProcessMemoryOffset(handle, config.ManaPointerSet, -36);
            if (curHP < 1 || maxHP < 1) {
                retString = "HP is invalid";
                return retString;
            }

            for (var i =0; i < config.Rules.Length; i++) {
                var rule = config.Rules[i];
                if (!rule.IsHPTrigger) continue;
                
                if (((float)curHP / (float)maxHP) < rule.Percent)
                {
                    UsePotion(i+1);
                    retString += "Used heal potion "+(i+1)+" due to " + curHP + "/" + maxHP + Environment.NewLine;
                }
            }
            return retString;
        }

        public string CheckMana()
        {
            var retString = "";
            maxMana = w32.ReadProcessMemoryOffset(handle, config.ManaPointerSet, -4);
            curMana = w32.ReadProcessMemoryOffset(handle, config.ManaPointerSet, 0);
            if (curMana < 1 || maxMana < 1)
            {
                retString = "Mana is invalid";
                return retString;
            }

           
            for (var i = 0; i < config.Rules.Length; i++)
            {
                var rule = config.Rules[i];
                if (rule.IsHPTrigger) continue;
                if (((float)curMana / (float)maxMana) < rule.Percent)
                {
                    UsePotion(i + 1);
                    retString += "Used mana potion " + (i + 1) + " due to " + curMana + "/" + maxMana + Environment.NewLine;
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
