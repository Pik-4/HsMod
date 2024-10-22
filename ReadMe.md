# HsMod

 **Hearthstone** **Mod**ification Based on BepInEx Hearthstone modification based on BepInEx, the plugin source code is located at [github](https://github.com/Pik-4/HsMod)[.](https://github.com/Pik-4/HsMod)[com](https://github.com/Pik-4/HsMod)[/](https://github.com/Pik-4/HsMod)[Pik](https://github.com/Pik-4/HsMod)[-](https://github.com/Pik-4/HsMod)[4](https://github.com/Pik-4/HsMod)[/](https://github.com/Pik-4/HsMod)[HsMod](https://github.com/Pik-4/HsMod), the plugin will not collect any information about you; the project follows `AGPL-3.0`, and is used for learning and research only.

**Warning: The Hearthstone client in mainland China starts the anti-cheat SDK by default, and the plugin will try to block the relevant anti-cheat functions, but it cannot guarantee the safety of your account.**

The readme was translated by DeepL, please forgive any grammatical errors.

### Realized Functions 

1. Support for Gear Fast and Slow 8x speed (settings allow expansion to Fast and Slow 32x)
2. Allow logging in with VerifyWebCredentials (also supports command line startup, no need to start Battle.net).
3. Block error reporting, no error messages are reported to Blizzard when an exception occurs.
4. Disable dropouts, allow long periods of no action
5. Allow error reporting and auto-exit
6. Allow removing window focus
7. Remove window resizing restrictions
8. Block pop-up window (e.g. can't match, etc.) prompts.
9. Remove Chinese hints
10. Supports removing hints for nerf patches, advertisements, and ladder settlement rewards.
11. Allow blocking of end-of-game promotion tips and settlement tips.
12. Allow to block the prompts of battle orders, achievements and other rewards.
13. Allow quick opening of packs, with space to open 5 packs at a time.
14. Allow automatic decomposition of fully decomposed cards when opening packs.
15. Allow displaying game frame rate information
16. Allow modifying game frame rate
17. Support displaying Dbid when right clicking on a selected card in collection, hero, card back, hit effect, pub panel, etc.
18. Supports displaying 9+ actual number of cards in favorites
19. Allow dueling to be abandoned at 0-0 (with no deck).
20. Allow automatic collection of rewards from Arena, Duel, etc. (tap package at the end)
21. Allow access to Hearthstone Developer Mode
22. Auto rotate decks, auto watch both sides of the game when friends watch the game
23. Supports analog positioning for Hearthside parties
24. Allows you to automatically block your opponent's emotes or set the limit of your opponent's emotes; supports blocking thinking emotes; supports blocking Bob's voice; supports skipping hero introductions in matchmaking.
25. Support emote without cooldown (minimum interval of 1.5 seconds between emotes).
26. Support emote shortcut keys
27. Support fast battle (skip part of the animation, more silky than gears, shield the end effect when open, the option can be effective in the tavern with mercenaries (PVE), mercenaries may have lag in the final death settlement,)
28. Support Hearthstone auto gold and diamond cards
29. Allow to block opponent's card effects individually
30. Allow displaying opponent's full Battle.net nickname
31. Allow tavern player nicknames to be retrieved by clicking on their avatar.
32. Allow to add opponents in the matchmaking
33. Allow opponent's ladder level to be displayed before lore
34. Supports marking opponent's known cards
35. Allow to mute Hearthstone with shortcut keys
36. Allow auto-reporting of opponents; when auto-reporting of opponents is enabled, game logs can be generated automatically
37. Support analog unplugging (requires shortcut keys to be enabled)
38. Supports one-click autodecomposition of fully decomposed decks (requires shortcut key enabled).
39. Support removing `new!` with one click (need to turn on the shortcut key, may need to re-enter the collection, mercenaries may not work after restarting)
40. Support modifying the skin information of Matchmaking Hero Skin, Tavern Hero Skin, Finale Effect, Matchmaking Panel, Tavern Panel, Lucky Coin and so on. (Need to configure `HsSkins.cfg`, or modify it in settings, update in the matchup needs to simulate unplugging after pressing `F4` to save)
41. Support modifying card backs (automatically take effect in the game)
42. Support mercenary random skins, forced diamond skins, etc.
43. Support blocking pop-up windows of mercenary treasure chests, ladder rewards, etc.
44. Support shielding mercenary matchmaking interface zoom
45. Support simulation of opening packs (support for random results, support for customizing the type, quantity, rarity, quality and other information of card packs; support for simulating fixed results)
46. Support device simulation (allow to collect card pack card backs for iOS, Android and other devices, may need a game of matchmaking)
47. Support gold to buy adventures such as Naxxramas, Blackrock Mountain, Explorer's Guild, etc. (also supports Karazhan, but can't play the prologue)
48. Allow force-opening Karazhan (can't play the prologue, can't skip a level until it's cleared)
49. Support information display (showinfo, need to enable the plugin, default HTTP, port 58744); support to display mercenary raising progress, pack opening history information, etc.
50. Support receiving Hearthstone startup parameters, such as specifying resolution size, etc.
51. Support Webshell, the path is /shell. need to be enabled in the settings, the current Chinese display may be garbled.
52. Allow reading local files via web, i.e. parsing static pages. This function is still under development, currently using `Hearthstone\website` as the root directory.
53. ~~Allow lifting the set recognition restriction to open Manning Hearthstone.~~ Has been fixed by Blizzard.
54. Attempt to disable anti-cheat.

### Installation Instructions 

#### **Windows** 

1. Compile `HsMod` or download `HsMod.dll` from `Releases`.
2. Configure `BepInEx`.
3. 2.1. Download [BepInEx_x86](https://github.com/BepInEx/BepInEx/releases) and extract it to the Hearthstone root directory `Hearthstone\`.
4. 2.2. create a directory `Hearthstone\BepInEx\unstripped_corlib\`; Copy all dlls under the project directory HsMod/LibUnityMono to the unstripped_corlib directory. 
5. 2.3. Modify `Hearthstone\doorstop_config.ini` by replacing `dllSearchPathOverride=` with `dllSearchPathOverride=BepInEx\unstripped_corlib`
6. Note: In [BepInEx 5.4.23.2](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2), modify `Hearthstone\doorstop_config.ini` to replace `dll_search_path_override =` with `dll_search_path_override = BepInEx\unstripped_corlib` `corlib`
7. Store `HsMod.dll` in `Hearthstone\BepInEx\plugins`.
8. Install BepInEx [ConfigManager BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases), unzip it to `Hearthstone\BepInEx\` and you're done; enter the game and `F5` for related control.

Note: unity and mono for Windows, extracted from [unity editor](https://unity.com/ja/releases/editor/whats-new/2021.3.40), unity is located at `.\Unity 2021.3.40f1\Editor\Data\PlaybackEngines\windowsstandalonesupport\Variations\win32_player_development_mono\Data\Managed`, mono Located in `.\Unity 2021.3.40f1\Editor\Data\MonoBleedingEdge\lib\mono\unityjit-win32`, some of the files are located in `unityjit-win32\Facades` )

#### Mac

1. Download the latest version of [BepInEx_unix](https://github.com/BepInEx/BepInEx/releases) and extract it to `Hearthstone/`

2. ~~Download original [Mono](https://unity.bepinex.dev/corlibs/2021.3.40.zip) and [Unity](https://unity.bepinex.dev/libraries/2021.3.40.zip) libraries and unpack to Hearthstone/BepInEx/unstripped_corlib~~. Copy all `dll` which under the project folder `HsMod/LibUnityMonoUnix` (`cp HsMod/LibUnityMonoUnix/*  Hearthstone/BepInEx/unstripped_corlib/` ). ( PS. Mono and Unity version must same as Hearthstone ).

3. Edit the `run_bepinex.sh` file replacing the line `export DOORSTOP_CORLIB_OVERRIDE_PATH=""`with `DOORSTOP_CORLIB_OVERRIDE_PATH="$BASEDIR/BepInEx/unstripped_corlib"`

4. Edit the file `run_bepinex.sh` replacing the line `executable_name=""` with `executable_name="Hearthstone.app"`

5. Run command in console `chmod u+x run_bepinex.sh`

6. Get the [token](https://www.battlenet.com.cn/login/zh-cn/?app=wtcg) here and copy after `http://localhost:0/?ST=` and before `&accountId=`

   ```
   # Some verify url
   https://www.battlenet.com.cn/login/zh-cn/?app=wtcg
   https://us.battle.net/login/en/?app=wtcg
   https://tw.battle.net/login/zh/?app=wtcg
   https://kr.battle.net/login/zh/?app=wtcg
   https://eu.battle.net/login/zh/?app=wtcg
   ...
   ```

7. Create a `client.config` file with the following content, instead of `token` - insert the token obtained in the previous step. Env value `xx.actual.battle.net`(parameter for China is `cn.actual.battlenet.com.cn`); `xx` same as the token first two characters. E.g

   ```
   [Config]
   Version = 3
   [Aurora]
   VerifyWebCredentials = "token"
   ClientCheck = 0
   Env.Override = 1
   Env = us.actual.battle.net
   ```

8. Download the HsMod [Releases](https://github.com/Pik-4/HsMod/releases) and unzip to `Hearthstone/BepInEx/plugins`

9. Download the [BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases) and unzip to `Hearthstone/BepInEx`After entering the game, press `F5` to control HsMod.

Now the game needs to be launched only through `./run_bepinex.sh`

If the token becomes obsolete and the game stops opening, then you just need to update it in the `client.config`.

The first run on Mac may prompt a Battle.net login error, please find HsMod.cfg and modify the activation plugin, please refer to [#](https://github.com/Pik-4/HsMod/issues/8#issuecomment-1344470389)[8](https://github.com/Pik-4/HsMod/issues/8#issuecomment-1344470389) for details.

#### **Linux** 

1. Compile `HsMod` or download `HsMod.dll` from `Releases`.

2. Install Hearthstone for Linux by referring to [0xf4b1/hearthstone-linux](https://github.com/0xf4b1/hearthstone-linux). (Theoretically, client.config will be configured at this point)

3. Download [BepInEx_unix](https://github.com/BepInEx/BepInEx/releases) (note: currently using BepInEx5) and extract it to the Hearthstone root directory under `hearthstone/`.

4. Create a directory `hearthstone/BepInEx/unstripped_corlib/`;

5. Note: You can also copy all the `.dlls` under the project directory `HsMod/LibUnityMonoUnix` directly to that directory

6. 1. Download [Mono](https://unity.bepinex.dev/corlibs/2021.3.40.zip) and [Unity](https://unity.bepinex.dev/libraries/2021.3.40.zip), unzip and extract the dlls, copy all dlls to the directory under that directory.

   2. Copy all the .dlls starting with `UniTask` under the project directory `HsMod/LibUnityMonoUnix` to that directory.

      ```
      cp HsMod/LibUnityMonoUnix/UniTask* hearthstone/BepInEx/unstripped_corlib/
      ```

7. Modify `unix_bepinex.sh`

8. 1. Replace `export DOORSTOP_CORLIB_OVERRIDE_PATH="" `with` DOORSTOP_CORLIB_OVERRIDE_PATH="$BASEDIR/BepInEx/unstripped_corlib"`
   2. Replace `executable_name=""` with `executable_name="Bin/Hearthstone.x86_64"`
   3. Once the replacement is complete, run `sed -i "s/\r/ /g" ./run_bepinex.sh` to modify the line breaks at the end of the file to match the Linux filesystem.

9. If configured correctly, the directory structure should look like the following at this point.

10. 

    ```
    hsmod@hsmod:~/hearthstone-linux/hearthstone$ ls -alh
    drwxrwxr-x 9 a a 4.0K Jan 12 12:07 .
    drwxrwxr-x 9 a a 4.0K Jan 12 09:27 ..
    drwxrwxr-x 4 a a 4.0K Jan 12 11:52 BepInEx
    drwxrwxr-x 3 a a 4.0K Jan 12 12:07 Bin
    -rw-rw-r-- 1 a a 1.4K Aug 30 02:53 changelog.txt
    -rw-rw-r-- 1 a a  103 Jan 12 11:16 client.config
    drwxrwxr-x 3 a a 4.0K Jan 12 10:46 Data
    drwxrwxr-x 2 a a 4.0K Jan 12 11:46 doorstop_libs
    -rw-rw-r-- 1 a a    5 Jan 12 09:27 .locale
    -rwxrwxr-x 1 a a 295K Jan 12 11:16 login
    drwxrwxr-x 7 a a 4.0K Jan 12 12:07 Logs
    drwxrwxr-x 5 a a 4.0K Jan 12 09:27 .ngdp
    -rw-rw-r-- 1 a a    3 Jan 12 09:27 .region
    -rwxrwxr-x 1 a a 4.8K Jan 12 12:07 run_bepinex.sh
    drwxrwxr-x 3 a a 4.0K Jan 12 10:47 Strings
    -rw-rw-r-- 1 a a   48 Jan 12 11:23 token
    -rw-rw-r-- 1 a a   12 Jan 12 11:16 .unity
    -rw-rw-r-- 1 a a   21 Jan 12 10:47 .version
    ```

11. If `client.config` is not configured, refer to steps 6-7 in the macOS installation instructions to configure client.config

12. Store `HsMod.dll` in the `hearthstone/BepInEx/plugins` directory (if the plugins directory does not exist, you need to create it manually).

13. Install the BepInEx [ConfigManager BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases) and unzip it to `hearthstone/BepInEx/`; enter the game and `F5` for the relevant controls.

14. Give `run_bepinex.sh` execute permission.

    ```
    chmod u+x run_bepinex.sh
    ```

15. Execute `./run_bepinex.sh` and enjoy Hearthstone.

### Version Description

As in HsMod version `3.0.0.0`:

First 3 => Hearthstone major version. For example: 3 => 26

The second digit 0 => the number of times Hearthstone has been updated in that version, which does not correspond to Hearthstone minor versions; furthermore, this number is not updated when an update to Hearthstone occurs, but there are no changes to files such as `Assembly-CSharp.dll`. For example: 0 => 26.x.y.z

Third bit 0 => This number is +1 when HsMod has new features for that Hearthstone version; this number is set to zero when the second bit changes.

Fourth bit 0 => compile version. Mainly records the number of bug fixes, corresponding to the third bit.

Hearthstone version update does not necessarily cause HsMod to fail, if HsMod plugin functions normally, it can not be updated with Release.HsMod update features can refer to the commit record.

### **Additional Notes** 

1. The plugin may not be placed in a directory containing Chinese characters, i.e. the Hearthstone installation path cannot contain Chinese characters.
2. This plugin may conflict with modifications based on `Assembly-CSharp.dll`, modifying `Assembly-CSharp.dll` may lead to abnormal positioning of IL commands, which in turn may cause the relevant Patch can not take effect; it may also conflict with other BepInEx plug-ins (such as mercenaries, MixMod), the reason is that the same method may exist in both plug-ins. The reason is that the same method may exist in two plugins, and when there are more than one Patch, the running result may be abnormal, and this plugin doesn't detect whether the original method has been modified or not.
3. The configuration file for skins is in `Hearthstone\BepInEx\config\HsSkins.cfg`. If there is none, it will be created automatically after running the game.
4. `F4` is a fixed shortcut key used to get some in-game information (related information is stored in the `Hearthstone\BepInEx\` directory), **update the skin configuration**, restart the web service, etc. The rest of the shortcuts can be customized and configured.
5. By default, almost all features of the plugin need to be enabled manually; most of the plugin's features can be found in the configuration, while a few are only mentioned in the Patch (such as minimizing restrictions).
6. The default port of this plug-in Web Server (i.e. Showinfo) is 58744, in general, listening to all local IPs, when using cloud servers, please pay attention to the configuration of firewalls, security groups and so on.
7. The log file used for logging statistics is `BepInEx\HsMatch.log`, which can be modified in the settings. (Fields are ` separated` by `,`)
8. When a problem occurs first try to delete the relevant `.cfg` configuration file (usually located in `BepInEx\config\` ) and reconfigure; if the problem still exists, please bring `HsMod.cfg` to submit [Issues](https://github.com/Pik-4/HsMod/issues), but there is no guarantee of a timely answer.
9. `GetHsLib.py` is used to update Hearthstone's own runtime libraries, and `install.bat` is used to copy the compiled `HsMod.dll` to the default Hearthstone directory (provided BepInEx has been configured). In addition, after push changes the version number (after PluginInfo.cs changes), [a release](https://github.com/Pik-4/HsMod/releases) is automatically generated.
10. If the skin display is abnormal, please check `HsSkins.cfg` and try to delete `HsMod.cfg` to re-configure it.
11. If the modified settings can not be saved, please check if other Hearthstone plugins are enabled.
12. For BepInEx, please choose **BepInEx 5**. Since BepInEx 6 is still in pre-release, it will not be adapted for now.

### client.config

 `client.config` is used to launch Hearthstone bypassing Battle.net, the file is located in the folder where Hearthstone.exe is located and has the following contents.

```config
[Config]
Version = 3
[Aurora]
VerifyWebCredentials = "VerifyWebCredentials"
ClientCheck = 0
Env.Override = 1
Env = us.actual.battle.net
```

 Some token acquisition links

```url
https://account.battlenet.com.cn/login/zh-cn/?app=wtcg
https://tw.battle.net/login/zh/?app=wtcg
https://kr.battle.net/login/zh/?app=wtcg
https://us.battle.net/login/en/?app=wtcg
https://eu.battle.net/login/en/?app=wtcg
```

With the plugin enabled, the support `.` `/Hearthstone.exe VerifyWebCredentials` command to launch Hearthstone ( ~~but required a client.config file~~, which is now not needed!) .

Note: The `Env` parameter for China is `cn.actual.battlenet.com.cn`.

### TODO 

1. Organize ReadMe, update Wiki, etc.; Organize the relationship between Configuration and Patch; Multi-language support
2. In-game one-click hero skin change, currently can only be updated by simulating dropouts
3. Refactor Showinfo related web pages.
4. Adapt to Mac
5. Fix mercenary related functions

### Reference

1. [MixMod_4pda](https://4pda.to/forum/index.php?showtopic=870696&st=4780#entry114865283)
2. [MixMod_github](https://github.com/DeNcHiK3713/MixMod)
3. [Hearthstone Advanced Mod](https://hearthmod.com/)
4. [Teach you how to use BepInEx to make plugin mods for unity games from scratch](https://mod.3dmgame.com/read/3)
5. [BepInEx Docs](https://docs.bepinex.dev/)
6. [Harmony](https://harmony.pardeike.net/articles/intro.html)
7. [List of CIL instructions](https://en.wikipedia.org/wiki/List_of_CIL_instructions)
8. [hearthstone-linux](https://github.com/0xf4b1/hearthstone-linux)

