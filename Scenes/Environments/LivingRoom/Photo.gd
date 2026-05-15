extends StaticBody3D

func interact():
	if Global.current_memory == 1:
		Global.current_memory = 2
		get_tree().change_scene_to_file("res://Scenes/Memories/Memory_1.tscn")
	else:
		print("Complete the previous memory first!")
