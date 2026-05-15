extends StaticBody3D

func interact():
	if Global.is_carrying_flowerpot == true:
		print("Placing flowerpot!")
		Global.is_carrying_flowerpot = false
		$Flowerpot.visible = true
		$CollisionShape3D.disabled = true
		
		# --- NEW: UPDATE THE OBJECTIVE TO MEDICINES ---
		Global.completed_objectives.append("Decorate with the flower pot(Get it from rooftop)")
		Global.current_objective_index = 4 # Moves to "Find your medicines"
		
	else:
		print("I need to find the flowerpot first.")
