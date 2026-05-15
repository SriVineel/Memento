extends StaticBody3D

# Drag your next room scene into this variable in the Inspector later!
@export_file("*.tscn") var next_scene_path: String
@export var spawn_point_name: String = "Spawn_FromRooftop" # We will create this next!

func interact():
	print("Opening door...")
	# Tell Global memory where we want to end up
	Global.target_spawn_point = spawn_point_name 
	
	# Change the scene
	get_tree().change_scene_to_file(next_scene_path)
