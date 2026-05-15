extends StaticBody3D

@export_file("*.tscn") var memory_scene_path: String

func _ready():
	# If we already did this memory, maybe we hide the medicines or just disable them
	if Global.medicine_memory_done == true:
		# Disables the collision box so you can't click it again
		$CollisionShape3D.disabled = true 

func interact():
	print("Entering Medicine Memory...")
	if memory_scene_path != "":
		get_tree().change_scene_to_file(memory_scene_path)
