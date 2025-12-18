# Endless Runner

A block-based Endless Runner prototype built in Unity, focusing on clean architecture, reliable core systems, and polished UI flow.

---

## Unity Version Used

* **Unity 6000.0.58f2 LTS**

---

## Setup Instructions

1. Open **Unity Hub**
2. Click **Open Project**
3. Select the project root folder
4. Ensure **Unity 6000.0.58f2 LTS** is installed
5. Open the project
6. Open the "Game" scene
7. Press **Play**

No additional setup is required.

---

## Controls

### Keyboard

* **Left Arrow / A** → Move Left
* **Right Arrow / D** → Move Right
* **Up Arrow / W** → Jump
* **Down Arrow / S** → Slide

### Mobile (Swipe)

* **Swipe Left** → Move Left
* **Swipe Right** → Move Right
* **Swipe Up** → Jump
* **Swipe Down** → Slide

---

## 1. What trade-offs did you make and why?

* **Limited content variety**
  The focus was on building a clean, extensible core system (spawning, pooling, movement, UI flow) rather than adding many block types or power-ups.

* **File-based save system**
  A simple JSON save file using `persistentDataPath` was chosen for clarity, debuggability, and ease of extension.

---

## 2. What would you improve if given one more week?

* Add **more block variations** with controlled randomness and difficulty scaling.
* Introduce a **distance-based progression system** alongside score.
* Improve **visual feedback** (camera shake, hit feedback, coin trails).
* Add **basic audio mixing** and volume controls.
* Implement **unit tests** for core systems like spawning and save/load.
* Optimize mobile performance further by batching and LOD usage.

---

## 3. How would you scale this prototype into a full game?

To scale this into a production-ready game, I would:

* Convert block selection into a **data-driven system** (ScriptableObjects).
* Add a **difficulty manager** controlling speed, obstacle density, and block probability.
* Expand the save system to support **profiles, progression, and unlocks**.
* Prepare the architecture for **live ops** (events, analytics, balance updates).

The current architecture is modular enough to support these changes without major rewrites.

---

## 4. Which part of the project are you most satisfied with?

I am most satisfied with the **scoring system and UI flow**, particularly the use of **DOTween for UI animations**:

* A clear and reliable scoring system with persistent best score saving using file-based data storage.
* Smooth and responsive UI transitions (Play, Quit, Game Over) implemented with DOTween and unscaled time.
* Clean separation between gameplay state changes and UI presentation logic.
* UI animations that remain stable even during pause states, improving overall polish and user experience.

---

## 5. Which part would you refactor first?

The first refactor would be:

* **Player-related responsibilities**
  Currently, PlayerManager handles input reactions, collision detection, score tracking, and game-over logic. I would split this into:

  * PlayerCollisionHandler
  * PlayerScoreHandler
  * PlayerStateController

This would improve readability, testability, and long-term maintainability as features grow.

---

## Final Notes

This prototype focuses on:

* Stable core gameplay systems
* Clean, maintainable architecture
* Practical editor tooling
* Polished UI interactions

The goal was to demonstrate **problem-solving, technical decision-making, and extensibility**, rather than feature quantity.
