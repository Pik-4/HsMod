# HsMod

**H**earth**s**tone **Mod**ify Based on BepInEx 基于BepInEx的炉石修改

### 安装说明

#### Windows

1. 编译`HsMod`或从`Releases`下载`HsMod.dll`。

2. 配置`BepInEx`。

   2.1. 下载[BepInEx_x86](https://github.com/BepInEx/BepInEx/releases)，并将其解压到炉石根目录`Hearthstone\`下。

   2.2. 创建一个目录`Hearthstone\BepInEx\unstripped_corlib\`；下载[Mono](https://unity.bepinex.dev/corlibs/2019.4.37.zip)和[Unity](https://unity.bepinex.dev/libraries/2019.4.37.zip)，并将其解压在该目录。 

   2.3. 修改`Hearthstone\doorstop_config.ini`，将`dllSearchPathOverride=` 替换成`dllSearchPathOverride=BepInEx\unstripped_corlib`

3. 将`HsMod.dll`存放在`Hearthstone\BepInEx\plugins`。

4. 安装BepInEx配置管理[BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases)，解压到`Hearthstone\BepInEx\`即可；进入游戏后`F5`进行相关控制。

#### Mac

1. Download the latest version of [BepInEx_unix](https://github.com/BepInEx/BepInEx/releases) and extract it to `Hearthstone\`

2. Download original [Mono](https://unity.bepinex.dev/corlibs/2019.4.37.zip) and [Unity](https://unity.bepinex.dev/libraries/2019.4.37.zip) libraries and unpack to `Hearthstone\BepInEx\unstripped_corlib\`. ( PS. Mono and Unity version must same as Hearthstone )

3. Edit the `run_bepinex.sh` file replacing the line `export DOORSTOP_CORLIB_OVERRIDE_PATH=""`with `DOORSTOP_CORLIB_OVERRIDE_PATH="$BASEDIR/BepInEx/unstripped_corlib"`

4. Edit the file `run_bepinex.sh` replacing the line `executable_name=""` with `executable_name="Hearthstone.app"`

5. Run command in console `chmod u+x run_bepinex.sh`

6. Get the [token](https://eu.battle.net/login/en-us/?app=wtcg) here and copy after `http://localhost:0/?ST=` and before `&accountId=`

   ```
   # Some verify url
   https://www.battlenet.com.cn/login/zh-cn/?app=wtcg
   https://us.battle.net/login/en/?app=wtcg
   https://tw.battle.net/login/zh/?app=wtcg
   https://kr.battle.net/login/zh/?app=wtcg
   ...
   ```

7. Create a `client.config` file with the following content, instead of `token` - insert the token obtained in the previous step. E.g

   ```
   [Config]
   Version = 3
   [Aurora]
   VerifyWebCredentials = "token"
   ClientCheck = 0
   Env.Override = 1
   Env = eu.actual.battle.net
   ```

8. Download the HsMod [Releases](https://github.com/Pik-4/HsMod/releases) and unzip to `Hearthstone\BepInEx\plugins`

9. Download the [BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases) and unzip to `Hearthstone\BepInEx`, After entering the game, press `F5` to control HsMod.

Now the game needs to be launched only through `./run_bepinex.sh`

If the token becomes obsolete and the game stops opening, then you just need to update it in the `client.config`.

### **补充说明**

1. 本插件可能与基于`Assembly-CSharp.dll`的修改冲突，修改`Assembly-CSharp.dll`可能导致IL指令定位异常，进而造成相关Patch无法生效；还可能与其他BepInEx插件（例如佣兵、MixMod）冲突，原因是同一个方法可能在两个插件中都存在Patch，当有多个Patch时，运行结果可能会异常，本插件没有检测原方法是否被修改。
2. 皮肤的配置文件在`Hearthstone\BepInEx\config\HsSkins.cfg`。若无，则在运行游戏后创建。
3. `F4`为固定快捷键，用于获取游戏内部分信息（相关信息存放在`Hearthstone\BepInEx\`目录下），及更新皮肤配置使用。其余快捷键均可自定义配置。
4. 插件在默认状态下，几乎全部的功能均需要手动开启；插件大部分功能能在配置中找到说明，少部分功能只在Patch中提及（如最小化限制）。
5. 出现问题时先尝试删除相关`.cfg`配置文件，进行重新配置；如果依然存在问题，请带上`HsMod.cfg`提交Issues，但不保证及时解答。

### client.config

用于绕过战网启动炉石，该文件位于Hearthstone.exe所在文件夹，内容如下

```config
[Config]
Version = 3
[Aurora]
VerifyWebCredentials = "VerifyWebCredentials"
ClientCheck = 0
Env.Override = 1
Env = cn.actual.battle.net
```

一些token获取链接

```url
https://www.battlenet.com.cn/login/zh-cn/?app=wtcg
https://tw.battle.net/login/zh/?app=wtcg
https://kr.battle.net/login/zh/?app=wtcg
https://us.battle.net/login/en/?app=wtcg
https://eu.battle.net/login/en/?app=wtcg
```

TODO

1. 整理ReadMe
2. 游戏内一键更换英雄皮肤，目前只能通过模拟掉线更新。
3. 多语言支持

### 参考

1. [MixMod](https://4pda.to/forum/index.php?showtopic=870696&st=4780#entry114865283)
2. [Hearthstone Advanced Mod](https://hearthmod.com/)
3. [从0开始教你使用BepInEx为unity游戏制作插件Mod](https://mod.3dmgame.com/read/3)
4. [BepInEx Docs](https://docs.bepinex.dev/)
5. [Harmony](https://harmony.pardeike.net/articles/intro.html)
6. [List of CIL instructions](https://en.wikipedia.org/wiki/List_of_CIL_instructions)

