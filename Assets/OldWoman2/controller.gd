extends CharacterBody3D

@onready var interact_ray = $Head/Camera3D/RayCast3D
@onready var interact_prompt = $CanvasLayer/InteractPrompt

const SPEED = 2.0
const JUMP_VELOCITY = 0
const MOUSE_SENSITIVITY = 0.0005

var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")

# We reference the Head node to look up and down
@onready var head = $Head

func _ready():
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED

func _unhandled_input(event):
	if event is InputEventMouseMotion:
		# Rotate the whole body left and right (Changes walking direction)
		rotate_y(-event.relative.x * MOUSE_SENSITIVITY)
		
		# Rotate ONLY the head up and down
		head.rotate_x(event.relative.y * MOUSE_SENSITIVITY)
		head.rotation.x = clamp(head.rotation.x, deg_to_rad(-80), deg_to_rad(80))

func _physics_process(delta):
	if not is_on_floor():
		velocity.y -= gravity * delta

	if Input.is_action_just_pressed("ui_accept") and is_on_floor():
		velocity.y = JUMP_VELOCITY

	# Now using your newly mapped WASD keys
	var input_dir = Input.get_vector("ui_right", "ui_left", "ui_down", "ui_up")
	var direction = (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	
	if direction:
		velocity.x = direction.x * SPEED
		velocity.z = direction.z * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		velocity.z = move_toward(velocity.z, 0, SPEED)

	move_and_slide()
	
	if Input.is_action_just_pressed("ui_cancel"):
		Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
		
func _process(delta):
	# 1. Reset the UI every frame just in case we look away
	interact_prompt.visible = false
	
	# 2. Check if the laser is hitting anything
	if interact_ray.is_colliding():
		var target = interact_ray.get_collider()
		
		# 3. Check if the thing we hit has our OOP "interact" function
		if target.has_method("interact"):
			
			# 4. Turn on the UI prompt
			interact_prompt.visible = true
			
			# 5. Listen for the 'E' key
			if Input.is_action_just_pressed("interact"):
				# Call the function on the teddy bear!
				target.interact()
