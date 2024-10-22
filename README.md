# UnityPackage

install via
learn and adapt from
[How create a Quest System in Unity | RPG Style | Including Data Persistence - YouTube](https://www.youtube.com/watch?v=UyTJLDGcT64)

```text
https://github.com/meangpu/U.MeQuest.git
```

to use

- create SOObject for quest to hold all quest step
- crate new quest by inherit `QuestStep`
- add `QuestStep` script to quest prefab

when start quest, it will

- instantiate QuestStep prefab object
- on progress will create newStep
- on no more progress will mark finish then
- spawn reward prefab
