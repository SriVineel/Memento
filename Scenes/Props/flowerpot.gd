extends StaticBody3D

# This lets you select your Memory scene in the Inspector!
@export_file("*.tscn") var memory_scene_path: String

func _ready():
	# When the roof loads, check if we already took this pot.
	# If we did, delete it instantly so it doesn't respawn!
	if Global.has_rooftop_pot_been_taken == true:
		queue_free()

func interact():
	print("Diving into Memory Scene...")
	
	# 2. Tell the game this pot is permanently gone from the roof
	Global.has_rooftop_pot_been_taken = true
	
	# 3. Teleport to the Memory Scene!
	if memory_scene_path != "":
		get_tree().change_scene_to_file(memory_scene_path)
	else:
		print("ERROR: You forgot to assign the memory scene in the Inspector!")
