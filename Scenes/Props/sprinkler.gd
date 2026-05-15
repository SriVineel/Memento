extends StaticBody3D

@export var dialogue_label: Label # We will connect the UI text to this!

func interact():
	print("Picked up sprinkler!")
	
	# Update global memory
	Global.is_carrying_sprinkler = true
	
	# Update the UI so the player knows what to do next
	if dialogue_label:
		dialogue_label.text = "You: I found it. I should bring this back to the child."
	
	# Delete the sprinkler from the world
	queue_free()
