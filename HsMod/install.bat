cd "%~dp0"

if exist "%PROGRAMFILES(x86)%\Hearthstone\BepInEx\core\BepInEx.dll" (
    copy /y Release\HsMod.dll "%PROGRAMFILES(x86)%\Hearthstone\BepInEx\plugins\HsMod.dll"
) else (
    echo BepInEx is not exist!
)