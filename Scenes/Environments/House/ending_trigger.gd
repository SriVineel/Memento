extends Area3D

@export var dialogue_label: Label
@export var fade_screen: ColorRect
@export var daughter_model: Node3D
@export var daughter_animator: AnimationPlayer
@export var animation_name: String = "Idle"

var ending_triggered = false

func _ready():
	# Connect the Area3D signal via code so we don't have to do it in the UI
	body_entered.connect(_on_body_entered)

func _on_body_entered(body):
	# 1. Check if it's the player AND if we are on the final objective (Index 5)
	if body.name == "OldLady" and Global.current_objective_index == 5 and not ending_triggered:
		ending_triggered = true
		start_ending_sequence()

func start_ending_sequence():
	print("Starting final cutscene...")
	
	# OPTIONAL: Freeze the player here if you have a Global.is_cutscene variable
	
	# 2. Silence the normal house music (if you have a MusicManager, stop it here)
	# MusicManager.stop() 
	
		
	# 4. Wait for 2 seconds of pure dread
	await get_tree().create_timer(2.0).timeout
	
	# 5. Reveal the daughter and play her animation
	if daughter_model:
		daughter_model.visible = true
	if daughter_animator:
		daughter_animator.play(animation_name)
		
	# 6. Wait for 3 seconds while the player looks at her
	await get_tree().create_timer(3.0).timeout
	
	# 7. Deliver the heartbreaking line
	if dialogue_label:
		dialogue_label.text = "You: ...Who are you?"
		
	# 8. Wait exactly 2 seconds so the player reads it
	await get_tree().create_timer(2.0).timeout
	
	# 9. Clear the text and trigger the 1-second Fade to Black
	if dialogue_label:
		dialogue_label.text = ""
		
	var tween = create_tween()
	# Tweens the black square's alpha from 0 (invisible) to 1 (solid) over 1.0 seconds
	tween.tween_property(fade_screen, "modulate:a", 1.0, 1.0) 
	
	# 10. Wait for the 1-second fade to finish
	await tween.finished
	
	# 11. End the game! Load the credits scene.
	print("Game Over. Loading Credits...")
	Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE) # Give them their mouse back!
	get_tree().change_scene_to_file("res://Scenes/Credits.tscn")
