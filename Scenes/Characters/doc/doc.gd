extends Node3D

@export var dialogue_label: Label

var memory_dialogue = [
	"Doctor: Please, sit down. I have the results from your cognitive exams.\n[Press Space]",
	"Doctor: The gaps you've been experiencing... the confusion. It isn't just normal aging or stress.\n[Press Space]",
	"Doctor: I'm so sorry. The scans show early, but definitive, signs of dementia.\n[Press Space]",
    "Doctor: We need to talk about a care plan before it progresses further.\n[Press Space to wake up]"
]

var current_line = 0
var music_player: AudioStreamPlayer

func _ready():
	# Start memory music
	Global.is_in_memory = true
	MusicManager.stop_music()
	music_player = AudioStreamPlayer.new()
	add_child(music_player)
	music_player.stream = load("res://Assets/Music/music6.mp3")
	music_player.play()

	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	if dialogue_label:
		dialogue_label.text = memory_dialogue[0]

func _process(delta):
	if Input.is_action_just_pressed("ui_accept"):
		advance_dialogue()

func advance_dialogue():
	current_line += 1
	if current_line < memory_dialogue.size():
		dialogue_label.text = memory_dialogue[current_line]
	else:
		finish_memory()

func finish_memory():
	print("Waking up from memory...")
	
	Global.medicine_memory_done = true
	Global.target_spawn_point = "Spawn_AfterMedicine"
	
	# --- NEW: UPDATE THE OBJECTIVE TO THE DOOR ---
	Global.completed_objectives.append("Find your medicines")
	Global.current_objective_index = 5 # Moves to "Answer the door"
	
	# Teleport back to the present
	get_tree().change_scene_to_file("res://Scenes/Level_01.tscn")
