extends StaticBody3D

@export_file("*.tscn") var next_scene: String = ""

func interact():
	if next_scene != "":
		get_tree().change_scene_to_file(next_scene)
