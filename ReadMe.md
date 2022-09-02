# HsMod

**H**earth**s**tone **Mod**ify Based on BepInEx 基于BepInEx的炉石修改，插件源代码位于[github.com/Pik-4/HsMod](https://github.com/Pik-4/HsMod)，插件不会收集您的任何信息；项目遵循`AGPL-3.0`，仅用作学习研究。

### 已实现的功能

1. 支持齿轮快慢8倍速
2. 允许使用VerifyWebCredentials登录（亦支持命令行启动，不需要启动战网）。
3. 屏蔽错误报告，当发生异常时，不会向暴雪报告错误信息。
4. 禁用掉线，允许长时间无操作
5. 允许报错自动退出
6. 允许移除窗口焦点
7. 解除窗口大小化限制
8. 拦截弹窗（如无法匹配等）提示。
9. 移除中国特色提示
10. 支持移除削弱补丁提示，移除广告推销，移除天梯结算奖励等弹窗
11. 允许屏蔽对局结束的升级提示、结算提示
12. 允许屏蔽战令、成就等奖励领取提示
13. 允许快速开包，空格一次开5张
14. 允许在开包时自动分解全额分解的卡牌
15. 允许显示游戏帧率信息
16. 允许修改游戏帧率
17. 支持在收藏、英雄、卡背、打击特效、酒馆面板等场景，右键选中卡牌时显示Dbid
18. 支持收藏显示9+卡牌实际数量
19. 允许在0-0（可以不组卡牌）时放弃对决
20. 允许自动领取竞技场、对决等奖励（结束时点包裹）
21. 允许进入炉石开发者模式
22. 好友观战自动旋转卡牌、自动观战双方
23. 支持炉边聚会模拟定位
24. 允许自动屏蔽对手表情或设置对方表情上限；支持屏蔽思考表情；支持屏蔽鲍勃语音；支持对战跳过英雄介绍
25. 支持表情无冷却（表情发送最小间隔1.5秒）
26. 支持表情快捷键
27. 支持酒馆快速战斗（跳过部分动画，比齿轮更丝滑，开启时屏蔽终结特效）
28. 支持炉石自动金卡、钻石卡
29. 允许单独屏蔽对手卡牌特效
30. 允许显示对手完整战网昵称
31. 允许点击头像获取酒馆玩家昵称
32. 允许对战中添加对手
33. 允许在传说前显对手示天梯等级
34. 支持标记对手已知卡牌
35. 允许使用快捷键静音炉石
36. 允许自动举报对手；当自动举报对手启用时，可以自动生成对局记录
37. 支持模拟拔线（需要开启快捷键）
38. 支持一键自动分解全额分解的卡牌（需要开启快捷键）
39. 支持一键移除`新！`（需要开启快捷键，可能需要重新进入收藏，佣兵可能重启后失效）
40. 支持修改对战英雄皮肤、酒馆英雄皮肤、终结特效、对战面板、酒馆面板、幸运币等皮肤信息。（需要配置`HsSkins.cfg`，或在设置中修改，对局中更新需要在按下`F4`保存后，模拟拔线）
41. 支持修改卡背（对局中自动生效）
42. 支持佣兵随机皮肤，强制钻石皮肤等
43. 支持屏蔽佣兵宝箱、天梯奖励等弹窗
44. 支持屏蔽佣兵对战界面缩放
45. 支持模拟开包（支持结果随机，支持自定义卡包类型、数量、稀有度、品质等信息；支持模拟固定结果）
46. 支持设备模拟（允许领取iOS、Android等设备的卡包卡背，可能需要一局对战）
47. 支持金币购买纳克萨玛斯、黑石山、探险者协会等冒险（也支持卡拉赞，但无法打序章）
48. 允许强开卡拉赞（不能打序章，未通关前不能跳关）
49. 支持信息展示（showinfo，需要启用插件，默认HTTP，端口58744）；支持显示佣兵养成进度、开包历史信息等。

### 安装说明

#### Windows

1. 编译`HsMod`或从`Releases`下载`HsMod.dll`。

2. 配置`BepInEx`。

   2.1. 下载[BepInEx_x86](https://github.com/BepInEx/BepInEx/releases)，并将其解压到炉石根目录`Hearthstone\`下。

   2.2. 创建一个目录`Hearthstone\BepInEx\unstripped_corlib\`；下载[Mono](https://unity.bepinex.dev/corlibs/2019.4.37.zip)和[Unity](https://unity.bepinex.dev/libraries/2019.4.37.zip)，并将其解压在该目录。 

   2.3. 修改`Hearthstone\doorstop_config.ini`，将`dllSearchPathOverride=` 替换成`dllSearchPathOverride=BepInEx\unstripped_corlib`

3. 将`HsMod.dll`存放在`Hearthstone\BepInEx\plugins`。

4. 安装BepInEx配置管理[BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases)，解压到`Hearthstone\BepInEx\`即可；进入游戏后`F5`进行相关控制。

#### Mac (尚未测试)

1. Download the latest version of [BepInEx_unix](https://github.com/BepInEx/BepInEx/releases) and extract it to `Hearthstone\`

2. Download original [Mono](https://unity.bepinex.dev/corlibs/2019.4.37.zip) and [Unity](https://unity.bepinex.dev/libraries/2019.4.37.zip) libraries and unpack to `Hearthstone\BepInEx\unstripped_corlib\`. ( PS. Mono and Unity version must same as Hearthstone )

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
   ...
   ```

7. Create a `client.config` file with the following content, instead of `token` - insert the token obtained in the previous step. Env value `xx.actual.battle.net`; `xx` same as the token first two characters. E.g

   ```
   [Config]
   Version = 3
   [Aurora]
   VerifyWebCredentials = "token"
   ClientCheck = 0
   Env.Override = 1
   Env = cn.actual.battle.net
   ```

8. Download the HsMod [Releases](https://github.com/Pik-4/HsMod/releases) and unzip to `Hearthstone\BepInEx\plugins`

9. Download the [BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases) and unzip to `Hearthstone\BepInEx`, After entering the game, press `F5` to control HsMod.

Now the game needs to be launched only through `./run_bepinex.sh`

If the token becomes obsolete and the game stops opening, then you just need to update it in the `client.config`.

### **补充说明**

1. 本插件可能与基于`Assembly-CSharp.dll`的修改冲突，修改`Assembly-CSharp.dll`可能导致IL指令定位异常，进而造成相关Patch无法生效；还可能与其他BepInEx插件（例如佣兵、MixMod）冲突，原因是同一个方法可能在两个插件中都存在Patch，当有多个Patch时，运行结果可能会异常，本插件没有检测原方法是否被修改。
2. 皮肤的配置文件在`Hearthstone\BepInEx\config\HsSkins.cfg`。若无，则在运行游戏后自动创建。
3. `F4`为固定快捷键，用于获取游戏内部分信息（相关信息存放在`Hearthstone\BepInEx\`目录下）、**更新皮肤配置**、重启Web服务等。其余快捷键均可自定义配置。
4. 本插件在默认状态下，几乎全部的功能均需要手动开启；插件大部分功能能在配置中找到说明，少部分功能只在Patch中提及（如最小化限制）。
5. 本插件Web Server（即Showinfo）的默认端口为58744，一般情况下，监听本地所有IP，使用云服务器时，请注意防火墙配置。
6. 对局统计所使用的log文件是`BepInEx\HsMatch.log`，可在设置中修改。此外对局统计页面适配的佣兵插件为[MercenariesHelper](https://github.com/HearthstonePlugin/Mercenaries)（当MercenariesHelper配合HsMod编译时，其Build4Public值应为false，以忽略与HsMod的相同Patch）。其他佣兵插件暂未做适配。
7. 出现问题时先尝试删除相关`.cfg`配置文件（一般位于`BepInEx\config\`），进行重新配置；如果依然存在问题，请带上`HsMod.cfg`提交Issues，但不保证及时解答。
8. `GetHsLib.py`用于更新炉石自有运行库，`install.bat`用于将编译好的`HsMod.dll`复制到默认炉石目录（前提是BepInEx已经配置好）。此外，push或提交PR到Github后，Actions会自动生成相关pre-release。

### client.config

`client.config`用于绕过战网启动炉石，该文件位于Hearthstone.exe所在文件夹，内容如下

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

在启用插件后，支持`./Hearthstone.exe VerifyWebCredentials` 命令启动炉石（但需要有`client.config`文件）。

TODO

1. 整理ReadMe，更新Wiki等；整理配置与Patch之间关系；多语言支持
2. 游戏内一键更换英雄皮肤，目前只能通过模拟掉线更新

### 参考

1. [MixMod](https://4pda.to/forum/index.php?showtopic=870696&st=4780#entry114865283)
2. [Hearthstone Advanced Mod](https://hearthmod.com/)
3. [从0开始教你使用BepInEx为unity游戏制作插件Mod](https://mod.3dmgame.com/read/3)
4. [BepInEx Docs](https://docs.bepinex.dev/)
5. [Harmony](https://harmony.pardeike.net/articles/intro.html)
6. [List of CIL instructions](https://en.wikipedia.org/wiki/List_of_CIL_instructions)

