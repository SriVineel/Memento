extends StaticBody3D

func interact():
	if Global.current_memory == 0:
		Global.completed_objectives.append("Interact with the teddy bear on the shelf")
		Global.current_objective_index = 2
		Global.current_memory = 1
		get_tree().change_scene_to_file("res://Scenes/Memories/Memory_0.tscn")
