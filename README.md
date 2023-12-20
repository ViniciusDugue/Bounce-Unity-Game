# Bounce Game
A 2d roguelike game created in the unity game engine.
* currently unfinished

Accomplished Tasks:

* Used an old game project as a base for the Bounce Unity Project.
    * This old game project includes enemies, player/enemy collision system, projectiles, inventory system, and ability management system.
* Created main menu, pause menu, and a game manager for game states.
* Implemented a portal for scene changes and a level completion menu.
* Designed a custom cursor for a better look and feel.
* Added a bouncy ball that can be shot with a mouse click and deal damage to enemies
* Included health bars for enemy units.
* Introduced obstacles for levels.
* Fixed collider bugs with enemies.
* Added a trajectory line for aiming the ball off walls. Iteratively casts rays off normal vectors of wall surfaces to draw the trajectory line 
* Enabled the player to hold and rotate the ball around them in the direction of the cursor
* Scaled ball damage with its current velocity.
* Included a camera shake on enemy collision.
* Made enemies flash white when taking damage.
* Added brief player invulnerability and flashing when hit.
* Fixed enemy ambient movement.
* Added pop-up damage text for enemies.
* Implemented a bounce combo display for successive bounces.
* Added a bounce meter to show ball power/damage.
* Introduced camera lerping for a smooth cursor alignment.
* Updated the bounce meter sprite with levels/notches.
* Moved ball dependencies from player to ball script.
* Added an innate ability to charge up the bounce meter with a right-click.
