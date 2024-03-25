# HsMod

**Hearthstone Modify** Based on BepInEx Based on BepInEx's Hearthstone modification, the plugin source code is located at [github.com/Pik-4/HsMod](https://github.com/Pik-4/HsMod), the plugin does not collect your any information; the project follows `AGPL-3.0` and is used for learning and research only.

### Functions realized

1. supports gearing fast and slow 8x speed (settings allow expansion to fast and slow 32x)
2. allow login with VerifyWebCredentials (also supports command line startup, no need to start Battle.net).
3. block error reporting, no error messages will be reported to Blizzard when an exception occurs.
4. disable dropouts, allow long periods of inactivity
5. Allow error reporting and auto quit
6. Allow removing window focus
7. Remove window resizing restrictions
8. Intercept pop-up windows (e.g. can't match, etc.).
9. Remove Chinese character tips
10. Remove hints of nerf patches, advertisements, and ladder rewards.
11. Allow blocking the upgrade tips and settlement tips at the end of the game.
12. allow shielding of battle orders, achievements and other rewards to receive tips
13. Allow quick opening of packs, with a space to open 5 packs at a time.
14. allow automatic decomposition of fully decomposed cards when opening packs
15. allow displaying game frame rate information
16. allow modifying game frame rate
17. support displaying Dbid when right clicking on selected cards in collection, hero, card back, hit effects, pub panel, etc. 18. support displaying 9+ cards in collection
18. support displaying 9+ actual number of cards in favorites
19. Allow to give up duel at 0-0 (no decks).
20. Allow to automatically collect rewards from Arena, Duel, etc. (click package at the end)
21. Allow access to Hearthstone Developer Mode
22. Friends watching the game automatically rotate the cards, automatically watch both sides of the game.
23. Support Hearthside party simulation location
24. Allow you to automatically block your opponent's emotes or set the limit of your opponent's emotes; support blocking thinking emotes; support blocking Bob's voice; support skipping hero introductions in the match.
25. support emote without cooldown (minimum interval of 1.5 seconds between emotes)
26. support emoticon shortcut keys
27. support fast combat (skip part of the animation, more silky smooth than gears, shielding end effects when opened, this option can be effective in the tavern with mercenaries (PVE), mercenaries may have lag in the final death settlement,)
28. Support Hearthstone auto gold and diamond cards.
29. allow to block opponent's card effects individually
30. Allow to display opponent's full Battle.net nicknames.
31. allow clicking on the avatar to get the tavern player's nickname
32. Allow adding opponents to a match
33. allow opponent's ladder level to be displayed before legend
34. support marking opponent's known cards
35. allow mute Hearthstone with shortcut key
36. allow auto-reporting of opponents; when auto-reporting of opponents is enabled, game logs can be generated automatically
37. support analog unplugging (need to enable shortcut key)
38. support one-click auto-decompose fully-decomposed cards (requires shortcut key)
39. support one key to remove `new! ` (need to open the shortcut key, may need to re-enter the collection, mercenaries may not work after restarting)
40. Support modifying the skin information of Matchmaking Hero Skin, Tavern Hero Skin, Finale Effect, Matchmaking Panel, Tavern Panel, Lucky Coin, etc. (You need to configure `HsSkins.cfg` or modify it in settings, and update in matchmaking needs to simulate unplugging after pressing `F4` to save)
41. support modifying card backs (automatically take effect in the game)
42. support mercenary random skin, forced diamond skin, etc. 43. support shield mercenary treasure chest
43. support shielding mercenary treasure chest, ladder reward popup window
44. support shielding mercenaries against the interface zoom
45. support simulation of opening packs (support for random results, support for customizing the type of card packs, quantity, rarity, quality and other information; support for simulation of fixed results)
46. support device simulation (allow to collect card pack card backs from iOS, Android and other devices, may need a game of matchmaking)
47. support gold to buy adventures such as Naxamas, Blackrock Mountain, Explorer's Guild, etc. (also support Karazhan, but can't play the prologue)
48. allow strong open Karazhan (can't play prologue, can't skip levels before passing)
49. support information display (showinfo, need to enable the plugin, default HTTP, port 58744); support to display the progress of mercenary raising, open pack history information, etc.
50. support receiving Hearthstone startup parameters, such as specifying resolution size, etc.
51. support Webshell, the path is /shell, you need to enable it in the settings, at present, the Chinese display may have garbled code.
52. allow to read local files via web, i.e. parse static pages. This function is still under development, currently using `Hearthstone\website` as the root directory. 53.
53. Allow to unlock set recognition restriction to enable Manning Hearthstone.

### Installation instructions

#### Windows

1. Compile `HsMod` or download `HsMod.dll` from `Releases`.

2. Configure `BepInEx`.

   2.1. Download [BepInEx_x86](https://github.com/BepInEx/BepInEx/releases) and extract it to the Hearthstone root directory `Hearthstone\`.

   2.2. create a directory `Hearthstone\BepInEx\unstripped_corlib\`; ~~ download [Mono](https://unity.bepinex.dev/corlibs/2021.3.25.zip) and [Unity](https:// unity.bepinex.dev/libraries/2021.3.25.zip) and unzip them in that directory ~~; copy all the dlls under the project directory HsMod/LibUnityMono to the unstripped_corlib directory.

   2.3 Modify `Hearthstone\doorstop_config.ini` by replacing `dllSearchPathOverride=` with `dllSearchPathOverride=BepInEx\unstripped_corlib`.

3. Store `HsMod.dll` in `Hearthstone\BepInEx\plugins`.

4. Install BepInEx ConfigManager [BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases), unzip it to `Hearthstone\BepInEx\`. ; Enter the game and `F5` for related controls.

#### Mac

1. Download the latest version of [BepInEx_unix](https://github.com/BepInEx/BepInEx/releases) and extract it to `Hearthstone/'

2. ~~Download original [Mono](https://unity.bepinex.dev/corlibs/2021.3.25.zip) and [Unity](https://unity.bepinex.dev/libraries/2021.3. 25.zip) libraries and unpack to Hearthstone/BepInEx/unstripped_corlib~~. 25.zip) libraries and unpack to Hearthstone/BepInEx/unstripped_corlib~~. Copy all `dll` which under the project folder `HsMod/LibUnityMonoUnix` (`cp HsMod/LibUnityMonoUnix/* Hearthstone/BepInEx/unstripped_corlib/ ` ) . ( PS. Mono and Unity version must be same as Hearthstone ).

3. Edit the `run_bepinex.sh` file replacing the line `export DOORSTOP_CORLIB_OVERRIDE_PATH=""`with `DOORSTOP_CORLIB_OVERRIDE_PATH="$BASEDIR/ BepInEx/unstripped_corlib"`

4. Edit the file `run_bepinex.sh` replacing the line `executable_name=""` with `executable_name="Hearthstone.app"`

5. Run command in console `chmod u+x run_bepinex.sh`

6. Get the [token](https://www.battlenet.com.cn/login/zh-cn/?app=wtcg) here and copy after `http://localhost:0/?ST=` and before `&accountId=`.

   ```
   # Some verify url
   https://www.battlenet.com.cn/login/zh-cn/?app=wtcg
   https://us.battle.net/login/en/?app=wtcg
   https://tw.battle.net/login/zh/?app=wtcg
   https://kr.battle.net/login/zh/?app=wtcg
   https://eu.battle.net/login/zh/?app=wtcg
   ...
   ``

7. Create a `client.config` file with the following content, instead of `token` - insert the token obtained in the previous step. Env value `xx.actual. battle.net`; `xx` same as the token first two characters. E.g

   ``
   [Config]
   Version = 3
   [Aurora]
   VerifyWebCredentials = "token"
   ClientCheck = 0
   Env.Override = 1
   Env = cn.actual.battle.net
   ```

8. Download the HsMod [Releases](https://github.com/Pik-4/HsMod/releases) and unzip to `Hearthstone/BepInEx/plugins`.

9. Download the [BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases) and unzip to `Hearthstone/BepInEx `After entering the game, press `F5` to control HsMod.

Now the game needs to be launched only through `. /run_bepinex.sh`.

If the token becomes obsolete and the game stops opening, then you just need to update it in the `client.config`.

The first run on Mac may prompt a Battle.net login error, please find HsMod.cfg and modify to activate the plugin, please refer to [#8](https://github.com/Pik-4/HsMod/issues/8#issuecomment-1344470389) for details.

#### Linux

1. Compile `HsMod` or download `HsMod.dll` from `Releases`.

2. Refer to [0xf4b1/hearthstone-linux](https://github.com/0xf4b1/hearthstone-linux) to install Hearthstone for Linux. (Theoretically, client.config will be configured at this point.)

3. Download [BepInEx_unix](https://github.com/BepInEx/BepInEx/releases) (Note: BepInEx5 is currently used) and unzip it into the Hearthstone root directory `hearthstone/`.

4. Create a directory `hearthstone/BepInEx/unstripped_corlib/`;

   1. Download [Mono](https://unity.bepinex.dev/corlibs/2021.3.25.zip) and [Unity](https://unity.bepinex.dev/libraries/2021.3.25.zip), unzip them to extract the dlls, and copy all the dlls to the directory under the directory.

   2. Copy all the .dlls starting with `UniTask` from the project directory `HsMod/LibUnityMonoUnix` to that directory.

      ````sh
      cp HsMod/LibUnityMonoUnix/UniTask* hearthstone/BepInEx/unstripped_corlib/
      ````

   Note: You can also directly copy all `.dll`s under the project directory `HsMod/LibUnityMonoUnix` to this directory

5. Modify `unix_bepinex.sh`.

   1. Replace `export DOORSTOP_CORLIB_OVERRIDE_PATH=""` with `DOORSTOP_CORLIB_OVERRIDE_PATH="$BASEDIR/BepInEx/unstripped_corlib"`.
   2. Replace `executable_name=""` with `executable_name="Bin/Hearthstone.x86_64"`.
   3. After the replacement is complete, run `sed -i "s/\r/ /g" . /run_bepinex.sh` to change the line breaks at the end of the file to match the Linux filesystem.

6. If configured correctly, the directory structure at this point should look like this.

   ```
   hsmod@hsmod:~/hearthstone-linux/hearthstone$ ls -alh
   drwxrwxr-x 9 a a 4.0K Jan 12 12:07 .
   drwxrwxr-x 9 a a 4.0K Jan 12 09:27 .
   drwxrwxr-x 4 a a 4.0K Jan 12 11:52 BepInEx
   drwxrwxr-x 3 a a 4.0K Jan 12 12:07 Bin
   -rw-rw-r-- 1 a a 1.4K Aug 30 02:53 changelog.txt
   -rw-rw-r-- 1 a a 103 Jan 12 11:16 client.config
   drwxrwxr-x 3 a a 4.0K Jan 12 10:46 Data
   drwxrwxr-x 2 a a 4.0K Jan 12 11:46 doorstop_libs
   -rw-rw-r-- 1 a a 5 Jan 12 09:27 .locale
   -rwxrwxr-x 1 a a 295K Jan 12 11:16 login
   drwxrwxr-x 7 a a 4.0K Jan 12 12:07 Logs
   drwxrwxr-x 5 a a 4.0K Jan 12 09:27 .ngdp
   -rw-rw-r-- 1 a a 3 Jan 12 09:27 .region
   -rwxrwxr-x 1 a a 4.8K Jan 12 12:07 run_bepinex.sh
   drwxrwxr-x 3 a a 4.0K Jan 12 10:47 Strings
   -rw-rw-r-- 1 a a 48 Jan 12 11:23 token
   -rw-rw-r-- 1 a a 12 Jan 12 11:16 .unity
   -rw-rw-r-- 1 a a 21 Jan 12 10:47 .version
   ``

7. If `client.config` is not configured, refer to steps 6-7 of the macOS installation instructions to configure client.config.

8. Store `HsMod.dll` in the `hearthstone/BepInEx/plugins` directory (if the plugins directory does not exist, you will need to create it manually).

9. Install BepInEx ConfigManager [BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases), unzip it to `hearthstone/BepInEx/`. ; enter the game and `F5` for the relevant controls.

10. Grant execution privileges to `run_bepinex.sh`.

    ```sh
    chmod u+x run_bepinex.sh
    ```

11. Execute `. /run_bepinex.sh` and enjoy Hearthstone.

### Imprint

If HsMod version `3.0.0.0`:

First 3 => Hearthstone major version. For example: 3 => 26

The second digit 0 => the number of times Hearthstone has been updated in that version, which does not correspond to Hearthstone mini-versions; furthermore, this number is not updated when an update to Hearthstone occurs, but no changes are made to `Assembly-CSharp.dll` or other files. For example: 0 => 26.x.y.z

Third bit 0 => +1 to this number when HsMod has a new feature for that Hearthstone version; set to zero when the second bit changes.

The fourth bit 0 => compile version. This is the number of bugs fixed, and corresponds to the third bit.

Hearthstone version updates do not necessarily invalidate HsMod, and can be updated without a Release if the HsMod plugin is functioning properly.HsMod update features can be found in the commit log.

### **Supplementary note**

1. The plug-in must not be placed in a directory containing Chinese characters, i.e. the Hearthstone installation path must not contain Chinese characters.
2. This plugin may conflict with modifications based on `Assembly-CSharp.dll`, modifying `Assembly-CSharp.dll` may lead to abnormal positioning of IL commands, which may result in the relevant Patch not taking effect; it may also conflict with other BepInEx plugins (e.g. Mercenaries, MixMod), the reason is that the same method may Patch exists in both plugins, when there is more than one Patch, the running result may be abnormal, this plugin does not detect whether the original method has been modified.
3. The configuration file for skins is in `Hearthstone\BepInEx\config\HsSkins.cfg`. If not, it is automatically created after running the game.
4. `F4` is a fixed shortcut key, which is used to get some in-game information (related information is stored in the `Hearthstone\BepInEx\` directory), **update the skin configuration**, restart the web service, etc. The rest of the shortcuts can be customized. The rest of the shortcuts can be customized and configured.
5. By default, almost all features of the plugin need to be enabled manually; most of the plugin's features can be found in the configuration, while a few features are only mentioned in the Patch (such as minimizing restrictions).
6. The default port of the plug-in Web Server (i.e. Showinfo) is 58744, in general, listening to all local IPs, when using cloud servers, please pay attention to the configuration of firewalls, security groups and so on.
7. The log file used for logging statistics is `BepInEx\HsMatch.log`, which can be modified in the settings. (Fields are separated by `,`)
8. When a problem occurs first try to delete the relevant `.cfg` configuration file (usually located in `BepInEx\config\`) and reconfigure it; if the problem still exists, bring the `HsMod.cfg` with you and submit it to [Issues](https://github.com/Pik-4/HsMod/issues), but there's no guarantee a timely answer.
9. `GetHsLib.py` is used to update Hearthstone's own runtime libraries, and `install.bat` is used to copy the compiled `HsMod.dll` to the default Hearthstone directory (provided BepInEx has been configured). In addition, [release](https://github.com/Pik-4/HsMod/releases) is automatically generated after push changes the version number (after PluginInfo.cs changes).
10. If there is an anomaly in the skin display, check `HsSkins.cfg` and try deleting `HsMod.cfg` to re-configure it.
11. If the modified settings cannot be saved, please check if other Hearthstone plugins are enabled.
12. For BepInEx, please select **BepInEx 5**, as BepInEx 6 is still in pre-release, it will not be adapted for now.

### client.config

The `client.config` is used to launch Hearthstone bypassing Battle.net, the file is located in the folder where Hearthstone.exe is located and has the following contents

```config
[Config]
Version = 3
[Aurora]
VerifyWebCredentials = "VerifyWebCredentials"
ClientCheck = 0
Env.Override = 1
Env = cn.actual.battle.net
```

Some token acquisition links

```url
https://www.battlenet.com.cn/login/zh-cn/?app=wtcg
https://tw.battle.net/login/zh/?app=wtcg
https://kr.battle.net/login/zh/?app=wtcg
https://us.battle.net/login/en/?app=wtcg
https://eu.battle.net/login/en/?app=wtcg
``

With the plugin enabled, support for the `. /Hearthstone.exe VerifyWebCredentials` command to start Hearthstone (~~ but requires a client.config file~~, which is now not required!) .

### TODO

1. Organize ReadMe, update Wiki, etc.; organize the relationship between configuration and Patch; multi-language support
2. In-game one-click hero skin change, currently can only be updated by simulating dropouts.
3. Reorganize Showinfo related web pages.
4. Adapt to Mac
5. Fix mercenary related functions

### Reference

1. [MixMod_4pda](https://4pda.to/forum/index.php?showtopic=870696&st=4780#entry114865283)
2. [MixMod_github](https://github.com/DeNcHiK3713/MixMod)
3. [Hearthstone Advanced Mod](https://hearthmod.com/)
4. [Teach you to use BepInEx to make plugin mods for unity games from 0](https://mod.3dmgame.com/read/3)
5. [BepInEx Docs](https://docs.bepinex.dev/)
6. [Harmony](https://harmony.pardeike.net/articles/intro.html)
7. [List of CIL instructions](https://en.wikipedia.org/wiki/List_of_CIL_instructions)
8. [hearthstone-linux](https://github.com/0xf4b1/hearthstone-linux)

