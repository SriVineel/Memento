extends Node3D

@onready var world_env = $WorldEnvironment # Make sure this matches your WorldEnvironment node name!

func _ready():
	# If the Old Lady just came back from the medicine memory...
	if Global.medicine_memory_done == true:
		
		# 1. Crank the saturation to make colors uncomfortably vivid
		world_env.environment.adjustment_saturation = 3.0
		
		# 2. Slightly boost contrast to make shadows harsher and more intimidating
		world_env.environment.adjustment_contrast = 2.15
