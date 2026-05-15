extends StaticBody3D

@export var dialogue_label: Label
@export var confused_model: Node3D
@export var joy_model: Node3D

var intro_dialogue = [
	"Child: Mom, what kind of plant is this? It looks thirsty.\n[Press Space]",
	"You: That's a forget-me-not, sweetie. You're right, it needs a drink.\n[Press Space]",
    "You: Let me go find the little green sprinkler.\n[Find the Sprinkler]"
]
var completion_dialogue = [
	"Child: Yay! You found it! Can I water it?\n[Press Space]",
    "You: Of course. Go ahead, give it some life.\n[Press Space to finish memory]"
]

var quest_stage = 0
var dialogue_index = 0
var music_player: AudioStreamPlayer

func _ready():
	# Start memory music
	Global.is_in_memory = true
	MusicManager.stop_music()
	music_player = AudioStreamPlayer.new()
	add_child(music_player)
	music_player.stream = load("res://Assets/Music/music3.mp3")
	music_player.play()

	if dialogue_label:
		dialogue_label.text = intro_dialogue[0]
	if confused_model and joy_model:
		confused_model.visible = true
		joy_model.visible = false

func _process(delta):
	if Input.is_action_just_pressed("ui_accept"):
		advance_dialogue()

func advance_dialogue():
	if quest_stage == 0:
		dialogue_index += 1
		if dialogue_index < intro_dialogue.size():
			dialogue_label.text = intro_dialogue[dialogue_index]
		else:
			quest_stage = 1
			dialogue_label.text = ""
	elif quest_stage == 2:
		dialogue_index += 1
		if dialogue_index < completion_dialogue.size():
			dialogue_label.text = completion_dialogue[dialogue_index]
		else:
			finish_memory()

func interact():
	if quest_stage == 1:
		if Global.is_carrying_sprinkler == true:
			quest_stage = 2
			dialogue_index = 0
			dialogue_label.text = completion_dialogue[0]
			if confused_model and joy_model:
				confused_model.visible = false
				joy_model.visible = true
		else:
			dialogue_label.text = "You: I should look for the sprinkler first before we water it."

func finish_memory():
	print("Memory complete. Returning to roof...")
	Global.is_in_memory = false
	MusicManager.play_track("res://Assets/Music/music1.mp3")
	Global.is_carrying_sprinkler = false
	Global.is_carrying_flowerpot = true
	Global.target_spawn_point = "Spawn_AfterMemory"
	get_tree().change_scene_to_file("res://Scenes/Environments/RoofTop/rooftop.tscn")
