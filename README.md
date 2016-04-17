# Poetionbot
A Path of Exile Potion Bot (PoEtionbot, get it?). This bot watches your health and mana, and when a certain percent is met, it clicks the hotkey (1-5) for your flasks.

![systray](http://i.imgur.com/GEPGnIw.png)

FAQ
---
<b>Will I get in trouble for using this?</b> Most likely. This is automating/assisting the player in a way that is against the terms of use of Grinding Gear games, by using this product you understand that it is against the EULA, and that the contributors to this tool are not liable for any damages that this program causes. In other words, <i>Use at your own risk</i>.

<b>Do I need to run it as admin?</b> No, it should run fine in the same access level as Path of Exile is running. (Admin only needed if PoE is also ran as admin).

<b>What is this program doing?</b> It is calling a Win32 API method called "ReadProcessMemory" to find your health/mana on Path of Exile, then using the SendInput API to simulate you pressing the # keys. It's a very simple, very non-aggressive tool.

<b>The download is saying it's suspicious</b> This program is open source, you're free to compile it yourself and verify how suspicious it is. Due to the lack of popularity of the tool it's going to give some oddities like this.

<b>My antivirus calls it a trojan/malware etc</b> I know that Norton AV doesn't like it, I believe it's because I'm using ReadProcessMemory and it generically flags any program using that call as potential malware. If you know ways to improve this concern, create an issue to open communication.

<b>I found a bug!</b> You can report bugs in the issues tab.

Usage
---
Start Path of Exile. Log in to your character.

Run poetionbot.exe. 

A system tray icon will appear with an representing different states that will give you hints of how the bot is doing.

<a href="https://github.com/Xackery/poetionbot/releases/download/0.1/poetionbot.exe"><img src="http://i.imgur.com/rogL2SF.png)" width="200"></a>

System Tray Icon Definitions
---
<img src="http://i.imgur.com/Zj3ABjg.png" width="24">(Green) The bot is ready for action and is waiting for something to do.

<img src="http://i.imgur.com/U3M3DQs.png" width="24">(Black) There is a challenge attaching. (The game isn't started, the memory offsets have changed, etc). When this appears, you some times have to detacth/reattach by right clicking the system try icon and choosing the options.

<img src="http://i.imgur.com/uuMVQ5L.png" width="24">(Swirl) The game does not have focus, this should turn to green when you re-focus the game.

<img src="http://i.imgur.com/tNgP8Qa.png" width="24">(Blue) Last used a mana flask.

<img src="http://i.imgur.com/ppJH2b5.png" width="24">(Red) Last used a life flask.

<img src="http://i.imgur.com/TKRfE6c.png" width="24">(Purple) Is reserved.

Testing
---
By default, a clean download will be set where at 50% hp, it will click 1, 2, 3, and 4. At 50% mana, it will click 5.

Make sure a mana potion is on your 5th slot for testing.

Go to a combat field (where you can fight) and spam a skill until your mana is 50% used, then see if your 5th potion is clicked 

If this happens, the bot is working properly, and you can now proceed to changing settings.

Changing Settings
---
Right click the system tray icon and go to Settings.

Inside settings, you can configure the potion bot to trigger based on health/mana for each hotkey designated. 

Once you're done configuring settings, click save.

A config file called poetionbot.ini will be generated with your settings, you can tweak the file manually as well (it's JSON).
