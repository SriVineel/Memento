extends Node

func _ready():
	Global.is_in_memory = true
	Global.current_memory = 2
	Global.completed_objectives.append("Clean the table(Get Cloth from second room in top floor)")
	Global.current_objective_index = 3
	await get_tree().create_timer(5.0).timeout
	Global.target_spawn_point = "SpawnFromLivingRoom"
	Global.is_in_memory = false
	MusicManager.play_track("res://Assets/Music/music1.mp3")
	Global.target_spawn_point = "SpawnFromLivingRoom"
	get_tree().change_scene_to_file("res://Scenes/Level_01.tscn")
