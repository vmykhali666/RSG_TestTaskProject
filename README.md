Here is a refactored version of your `README.md` file with improved formatting and structure:

```markdown
# Project Tasks

## Task 1: Limit Inventory Capacity
Currently, the player can collect an unlimited number of items in the inventory. Implement a system where each item has a weight, and the player has a maximum weight capacity. If an item exceeds the capacity, it cannot be picked up.

### Tips:
- Consider modifying the `IStorage` logic to support weight and check if an item can fit.
- Look into `LootInteractable` and `GatherLootEntityBehaviour` for item collection logic.

---

## Task 2: Bug - Inventory Items Disappear When Transitioning from Dungeon to Surface
When transitioning from the dungeon to the surface, all collected items disappear. This prevents the player from selling them to the trader.

### Tips:
- Ensure the player's item data is stored in `GlobalSceneContext`.
- Create a dedicated model (similar to `PlayerCameraModel`) to store the player's item data.

---

## Task 3: Display Inventory Item Count on UI
Currently, there is no way to know if the player has picked up items. Add a UI element to display the number of items the player is carrying.

### Tips:
- Use an MVx design pattern (e.g., MVP) for UI implementation.

---

## Task 4: Display Player Health on UI
The player's health is not visible, making it unclear if enemies are dealing damage. Add a health bar to the UI. Ensure health does not reset when transitioning to the surface.

### Tips:
- Player health is stored in `IDamageable`.

---

## Task 5: Add Player Currency Balance
Currently, coins earned by selling items to the trader are not tracked. Implement a system to manage the player's currency balance and display it on the UI.

### Tips:
- The `Trader` returns the total value of items in the inventory.

---

## Task 6: Add HealPotion Usage
Players collect potions from defeated enemies but cannot use them. Add functionality to use HealPotions for healing.

### Tips:
- Implement both a UI button and a keyboard shortcut (using `NewInputSystem`) for potion usage.
- Display the remaining number of HealPotions on the UI.

---

## Task 7: Randomize Enemy Loot Drops
Currently, enemies always drop one book and one HealPotion. Add randomness to loot drops, with a chance for specific items from a predefined list.

---

## Task 8: Add HealPotion Purchase from Trader
Allow players to spend their earned currency by purchasing HealPotions from a trader. Add a stall near the trader for potion purchases.

---

## Task 9: Add Breakable Boxes
On the surface scene, there are boxes that cannot be interacted with. Allow players to attack and break these boxes, causing loot to drop upon destruction.

### Tips:
- Similar logic already exists for enemies.

---

## Bonus Task: Project Review
The project contains several issues, and some module implementations are questionable. Review the project, identify problems, and suggest improvements in a Pull Request.

---
``` 

This version organizes the tasks into sections with clear headings, descriptions, and tips. It uses Markdown features like headings, lists, and horizontal rules for better readability.