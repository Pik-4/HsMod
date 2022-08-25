# HsMod

基于BepInEx的炉石修改 Hearthstone Mod Based on BepInEx

### 安装说明

1. 编译`HsMod`。

2. 配置`BepInEx`。

   2.1. 下载[BepInEx_x86](https://github.com/BepInEx/BepInEx/releases)，并将其解压到炉石根目录`Hearthstone\`下。

   2.2. 创建一个目录`Hearthstone\BepInEx\unstripped_corlib\`；下载[Mono](https://unity.bepinex.dev/corlibs/2019.4.37.zip)和[Unity](https://unity.bepinex.dev/libraries/2019.4.37.zip)，并将其解压在该目录。 

   2.3. 修改`Hearthstone\doorstop_config.ini`，将`dllSearchPathOverride=` 替换成`dllSearchPathOverride=BepInEx\unstripped_corlib`

3. 安装BepInEx配置管理[BepInExConfigManager.Mono](https://github.com/sinai-dev/BepInExConfigManager/releases)，解压到`Hearthstone\BepInEx\`即可；进入游戏后`F5`进行相关控制。

4. 将编译好的`HsMod.dll`文件，存放在`Hearthstone\BepInEx\plugins`即可。

### 补充说明

1. 皮肤的配置文件在`Hearthstone\BepInEx\config\HsSkins.cfg`。若无，则在运行游戏后创建。
2. `F4`为固定快捷键，用于获取游戏内部分信息（相关信息存放在`Hearthstone\BepInEx\`目录下），及更新皮肤配置使用。其余快捷键均可自定义配置。



### client.config

位于Hearthstone.exe所在文件夹，内容如下

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

