extends Control

const MAIN_SCENE = "res://Scenes/Level_01.tscn"

@onready var title_panel = $TitlePanel
@onready var context_panel = $ContextPanel

func _ready():
	Global.is_in_memory = true
	Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
	title_panel.visible = true
	context_panel.visible = false

func _input(event):
	if event is InputEventMouseButton and event.pressed:
		if title_panel.visible:
			title_panel.visible = false
			context_panel.visible = true
		elif context_panel.visible:
			Global.is_in_memory = false
			get_tree().change_scene_to_file(MAIN_SCENE)
