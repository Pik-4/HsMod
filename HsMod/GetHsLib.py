import os


pyName = os.path.basename(__file__)
pyPath = os.path.realpath(__file__)
realWorkDir = pyPath[:-len(pyName)]
unity_mono = os.listdir(realWorkDir+"LibUnityMono")
HearthstoneDllPath = "C:\\Program Files (x86)\\Hearthstone\\Hearthstone_Data\\Managed\\"
for dll in os.listdir(HearthstoneDllPath):
    if dll.endswith('dll') and (dll not in unity_mono):
        os.system(f'copy /y "{HearthstoneDllPath+dll}" "{realWorkDir}LibHearthstone\\{dll}"')
