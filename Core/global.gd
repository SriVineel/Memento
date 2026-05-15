extends Node

var is_carrying_flowerpot = false
var target_spawn_point: String = ""
var has_rooftop_pot_been_taken: bool = false
var is_carrying_sprinkler: bool = false
var medicine_memory_done: bool = false


var is_in_memory = false
var completed_objectives = []
var current_objective_index = 1
var current_memory = 0  # 0 = teddy not yet interacted
