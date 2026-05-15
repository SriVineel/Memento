xtends StaticBody3D

const LIVING_ROOM_SCENE = "res://Scenes/Environments/LivingRoom/LivingRoom.tscn"

func _ready() -> void:
	pass

func interact():
	get_tree().change_scene_to_file(LIVING_ROOM_SCENE)

func _process(_delta: float) -> void:
	pass
