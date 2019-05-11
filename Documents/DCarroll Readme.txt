The following changes have been made to the game since you corrected it
in class

-- General -- 
+ The Drone and Player now have 3D Models and are no longer represented by
  cubes.

+ The Player can now be killed by the enemy once they are spotted

+ The Player can now crouch using <c>

-- Maths --
+ The Drone now turns based on a quaternion rather than just floating in place

-- Animation --
+ The player now has several animations corresponding to different actions in the game.
	- Running
	- Jumping
	- Shooting
	- Crouching
	- Crouch-running

-- Artificial Intelligence --
+ The enemy now has a simple, partially unfinished AI
	- In it's idle state it will look around itself for the player by spinning in place
	- If the Player is spotted, it will move to the player's z-value and engage.
	- The enemy can now be killed when it's health reaches 0

-- GUI --
+ There is now an on-screen counter displaying how many coins you have collected 

-- Instanceation --
+ The Player now creates an instance of the Drone at the start of the game instead of the drone being placed within Unity