# CSCI526-element-fighting

This document provides a detailed overview of the current work flow

### 1. **GameController (`GameController.cs`)**

- **Role**: 
  The central coordinating script responsible for the overall game flow and interactions.

- **Key Features**:
  - Maintains references to key game objects like players and spawners.
  - Listens to step spawning events to decide when and which elements should spawn on them.
  - Manages the positional relationship between steps and their associated elements, ensuring elements follow steps EVEN IF THEY ARE NOT IN THE SAME CLASS
  
- **Relationship**:
  - Interacts with `ElementSpawnerScript` to decide when and where game elements are spawned.
  - Listens to the `StepSpawnerScript` to know when new steps are created.

### 2. **StepSpawnerScript (`StepSpawnerScript.cs`)**

- **Role**: 
  Responsible for spawning the steps(platforms).

- **Key Features**:
  - Steps spawn at random horizontal locations and move upwards.
  - The width in which steps can spawn is dynamically determined based on the screen width.
  - Provides an event `OnStepSpawned` to notify other scripts when a new step is created.
  
- **Relationship**:
  - `GameController` listens to this script's events to know when a step has been spawned.

### 3. **ElementSpawnerScript (`ElementSpawnerScript.cs`)**

- **Role**: 
  Manages the spawning of elements on the steps.

- **Key Features**:
  - Given a list of available element prefabs, it can decide which element to spawn based on gameplay rules.
  - Ensures that elements which are already assigned to players don't spawn again.
  
- **Relationship**:
  - Called by the `GameController` to spawn an element on a step when required.
  
### 4. **PlayerElementScript (`PlayerElementScript.cs`)**

- **Role**: 
  Attached to player objects, it keeps track of the current element assigned to a player.

- **Key Features**:
  - Maintains a `playerElement` string indicating the current element assigned to the player.
  
- **Relationship**:
  - Interacted with by `GameController` to check or change the current element of a player.

---

**Note to Charlie**: Maybe need to change a player's element after possible game events(freeze, hit element, etc) to ensure the player's element updates correctly.

### 5. **StepBehaviorScript (`StepBehaviorScript.cs`)**

- **Role**: 
  Determines the behavior of individual steps.

- **Key Features**:
  - [Details to be added based on the script's content]
  
- **Relationship**:
  - Steps created by `StepSpawnerScript` have this behavior.

### 6. **ElementScript (`ElementScript.cs`)**

- **Role**: 
  Defines the behavior of individual game elements.

- **Key Features**:
  - [Details to be added based on the script's content]
  
- **Relationship**:
  - Elements spawned by `ElementSpawnerScript` have this behavior.

---

**Note to Danyang**: We can control the element object here

### Conclusion:

TBC
