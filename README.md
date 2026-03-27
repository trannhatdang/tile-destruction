---
INSTALLATION
---

Clone to machine and use Unity 2022.3, then open the file TileObjectSO and find lines with "UNCOMMENT", uncomment that block. Afterwards, the project can be opened normally by unity.

---
MAIN SYSTEMS
---

Game Manager when the game pauses to give power-ups. UI Manager controls which components become active and not.
Tiles when colliding with Abyss gives XP. PlayerAttack is a trigger collider that becomes active when the player clicks, it's power can be customized in PlayerStatsSO.
Each level is characterized by LevelSO, which has information on which tile object drops at which times, and also levelUp requirements (how much XP for power ups, for next level).

---
TILE EDITOR
---

I have made a tile editor that helps creating big Tile Objects. Demonstration: https://www.youtube.com/watch?v=BiEhspPmgLQ

---
REGRETS
---

I had more ideas on alternative weapons, like an insta-kill hammer that breaks the tile object wide open, or a straight shot laser, so I would have tried that.
Performance becomes an issue at later rounds, so given more time I would have looked into proper objectpooling for tile objects.
